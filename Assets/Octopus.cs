using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
     public AnimationStateChanger asc;
    [SerializeField] float forceApplied;
    [SerializeField] int health = 5;
    void Awake(){
        forceApplied = 30f;
    }

    void Update(){
        if (health <= 0) {
            Death();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<CreatureShot>() != null || other.GetComponent<PowerShot>() != null){
           health -= 1;
           asc.ChangeAnimationState("Hurt", 1, null, 0.2f);
        } else if (other.GetComponent<Creature>() != null){
            Creature creature = other.GetComponent<Creature>();
            Rigidbody2D rb = creature.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector3.up * forceApplied, ForceMode2D.Impulse);
        }
    }

    private void Death() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        asc.ChangeAnimationState("Death", 1, DestroyAfterAnimation, 0.2f);
    }

    private void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}

