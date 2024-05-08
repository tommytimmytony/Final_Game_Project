using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : MonoBehaviour
{
    public AnimationStateChanger asc;
    [SerializeField] float forceApplied;

    void Awake(){
        forceApplied = 25f;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<CreatureShot>() != null || other.GetComponent<PowerShot>() != null){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            asc.ChangeAnimationState("Hurt", 1, DestroyAfterAnimation, 0.2f);
        } else if (other.GetComponent<Creature>() != null){
            other.GetComponent<Creature>().GetComponent<Rigidbody2D>().AddForce(Vector3.up * forceApplied, ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }
    }

    private void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
