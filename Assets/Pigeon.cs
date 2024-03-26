using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float speed = -10;

    [Header("Attack")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Creature player;
    [SerializeField] float timeAttack = 2.5f;

    [Header("Flavor")]
    [SerializeField] string creatureName = "PigeonBob";

    Rigidbody2D rb;
    [SerializeField] float offset;
    [SerializeField] float offsetx;

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
