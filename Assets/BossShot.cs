using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public AnimationStateChanger asc;

    void Start()
    {
        asc.ChangeAnimationState("Idle");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Creature>() != null){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Creature>().PlayerHurt();
            asc.ChangeAnimationState("Impact", 2, DestroyAfterAnimation, 0.2f);
        }
    }

    private void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
