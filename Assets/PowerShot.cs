using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : MonoBehaviour
{
    public AnimationStateChanger asc;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Boss>() != null){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Boss>().BossHurt();
            asc.ChangeAnimationState("Impact", 1, DestroyAfterAnimation, 0.2f);
        } else if (other.GetComponent<Vulture>() != null || other.GetComponent<Pigeon>() != null || other.GetComponent<BlueBird>() != null) {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            asc.ChangeAnimationState("Impact", 1, DestroyAfterAnimation, 0.2f);
        }
    }

    private void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
