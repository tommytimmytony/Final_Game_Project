using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed = -10;

    [Header("Attack")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Creature player;
    [SerializeField] float timeAttack = 2.5f;

    [Header("Flavor")]
    [SerializeField] string creatureName = "BigBob";
    public GameObject body;

    [SerializeField] float offset;
    [SerializeField] float offsetx;

    public float circleRadius = 1f; // Radius of the circular motion
    public float circleSpeed = 1f; // Speed of the circular motion
    private Vector3 initialPosition;
    private float angle = 0f;
    private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise

    void Start()
    {
        AttackPlayer();
        health = 50;
        initialPosition = transform.position;
    }


     void Update()
    {
        // Circular motion
        float x = initialPosition.x + Mathf.Cos(angle) * circleRadius;
        float y = initialPosition.y + Mathf.Sin(angle) * circleRadius;
        transform.position = new Vector3(x, y, initialPosition.z);
        angle += circleSpeed * rotationDirection * Time.deltaTime;
    }


     void AttackPlayer(){
        StartCoroutine(AttackPlayerRoutine());
        IEnumerator AttackPlayerRoutine(){
            while(true){
                yield return new WaitForSeconds(timeAttack);
                AttackPlayerRandom();
            }
        }
    }

    void AttackPlayerRandom(){
        Vector3 initPos = transform.position + new Vector3(-0.8f,2.5f,0);
        GameObject newProjectile = Instantiate(projectilePrefab, initPos, Quaternion.identity);
        newProjectile.transform.rotation = Quaternion.LookRotation(transform.forward, player.transform.position - initPos);
        newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * speed;
        Destroy(newProjectile,15);
    }

    public void BossHurt() {
        StartCoroutine(FlashBoss());
        health -= 1;
        if (health < 0) {
            Debug.Log("Boss Down");
        }
    }

    private IEnumerator FlashBoss()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,0f);  
        yield return new WaitForSeconds(0.1f);      
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
    }

}
