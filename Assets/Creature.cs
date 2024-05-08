using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Creature : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitPointText;

    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] float jumpForce = 1;

    public enum CreatureMovementType {tf, physics};
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.physics;
    public enum CreaturePerspective { topDown, sideScroll };
    [SerializeField] CreaturePerspective perspectiveType = CreaturePerspective.sideScroll;

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -1f;
    [SerializeField] float jumpRadius = .5f;

    [Header("Flavor")]
    [SerializeField] string creatureName = "Bob";
    public GameObject body;
    [SerializeField] public List<AnimationStateChanger> animationStateChangers;


    [Header("Tracked Data")]
    [SerializeField] Vector3 homePos;

    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        Debug.Log("Start");
        hitPointText.text = "HP." + health.ToString();
    }

    void Update() {
        if (transform.position.y < -10) {
            BackToMainMenu();
        } 
    }

    public void MoveCreature(Vector3 direction) {
        if (movementType == CreatureMovementType.tf) {
            MoveCreatureTransform(direction);
        } else if (movementType == CreatureMovementType.physics) {
            MoveCreatureRb(direction);
        }

        //set animation
        if(direction != Vector3.zero){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Walk", speed);
            }
        }else{
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Idle");
            }
        }
    }

    public void MoveCreatureRb(Vector3 direction)
    {
        Vector3 currentVelocity = Vector3.zero;
        if(perspectiveType == CreaturePerspective.sideScroll){
            currentVelocity = new Vector3(0, rb.velocity.y, 0);
            direction.y = 0;
        }

        rb.velocity = (currentVelocity) + (direction * speed);
        float cS = body.transform.localScale.z;
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-cS,cS,cS);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(cS,cS,cS);
        }
    }

    public void MoveCreatureTransform(Vector3 direction) {
        transform.position += direction * Time.deltaTime * speed;
    }

    public void Jump(){
        if(Physics2D.OverlapCircleAll(transform.position + new Vector3(0,jumpOffset,0),jumpRadius,groundMask).Length > 0){
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Jump", speed);
        }
       
    }

    public void PlayerHurt() {
        foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Hurt");
        }
        health -= 1;
        GetComponent<AudioSource>().Play();
        if (health <= 0) {
             foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Death", 10, BackToMainMenu, 1);
            }
        } 
        if (health >= 0) {
            hitPointText.text = "HP." + health.ToString();
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivatePowerup()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
        StartCoroutine(RevertColorAfterDelay(8f));
    }

    IEnumerator RevertColorAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Change the color back to the original color
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f);
    }

    public void ActivateHealthPlus(){
        health += 1;
        hitPointText.text = "HP." + health.ToString();
    }

    public void ForceUp(){
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}