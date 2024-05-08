using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int health;
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
    [SerializeField] AudioClip bossAppear;
    [SerializeField] AudioClip bossHurt;
    [SerializeField] AudioClip bossDeath;
    [SerializeField] AudioClip victory;
    AudioSource src;
    Rigidbody2D rb;
    private bool isMovingCircularly = false;
    private bool bossLanded = false;
    public bool finalBoss = false;
    void Start()
    {
        health = 50;
        rb = GetComponent<Rigidbody2D>();
        src = GetComponent<AudioSource>();
        body.SetActive(false);
    }

    public void InitBoss(){
        bossLanded = false;
        body.SetActive(true);
        transform.position = new Vector3(14,7,0);
        StartCoroutine(MoveBoss());
        AttackPlayer();
    }

    void Update()
    {
        if (!bossLanded && rb.position.x <= 5.7)
        {
            StopBoss();
            ActivateCircularMotion(); 
            bossLanded = true;
        }
        if (!bossLanded && rb.position.y < 0)
        {
            body.SetActive(false);
            src.clip = victory;
            src.Play();
        }
    }

     void ActivateCircularMotion()
    {
        if (!isMovingCircularly)
        {
            StartCoroutine(MoveBossCircular());
            isMovingCircularly = true; // Avoid multiple coroutine starts
        }
    }

    IEnumerator MoveBossCircular()
    {
        initialPosition = transform.position;
        while (true)
        {
            float x = initialPosition.x + Mathf.Cos(angle) * circleRadius;
            float y = initialPosition.y + Mathf.Sin(angle) * circleRadius;
            transform.position = new Vector3(x, y, initialPosition.z);
            angle += circleSpeed * rotationDirection * Time.deltaTime;
            yield return null;
        }
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
        GetComponent<AudioSource>().Play();
        Destroy(newProjectile,15);
    }

    public void BossHurt() {
        if (!body.activeSelf) {return;}
        StartCoroutine(FlashBoss());
        health -= 1;
    }

    public void BossDown() {
        StopAllCoroutines();
        rb.velocity = new Vector2(-5f, 12f);
        src.clip = bossHurt;
        src.Play();
    }

    private IEnumerator FlashBoss()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,0f);  
        yield return new WaitForSeconds(0.1f);      
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
    }

    void StopBoss()
    {
        rb.velocity = Vector2.zero;
    }

    IEnumerator MoveBoss()
    {
        rb.velocity = new Vector2(-1.2f, -1f); 
        yield return new WaitForSeconds(60f);
        if (!bossLanded) {
        src.clip = bossAppear;
        src.Play();
        }
    }
    
    public void BossDeath(){
        src.clip = bossDeath;
        src.Play();
        rb.velocity = new Vector2(0,-4);
        StartCoroutine(FlashBoss());
        StartCoroutine(FlashBoss());
        StartCoroutine(FlashBoss());
        StartCoroutine(FlashBoss());
    }
}
