using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    public AnimationStateChanger asc;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<CreatureShot>() != null || other.GetComponent<PowerShot>() != null){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            asc.ChangeAnimationState("Hurt", 1, DestroyAfterAnimation, 0.2f);
        } else if (other.GetComponent<Creature>() != null){
           GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Creature>().PlayerHurt();
            asc.ChangeAnimationState("Death", 2, DestroyAfterAnimation, 0.2f);
        }
    }

    private void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
