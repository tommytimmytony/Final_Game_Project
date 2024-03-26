using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Creature>() != null){
            Creature player = other.GetComponent<Creature>();
            player.GetComponent<ProjectileThrower>().ActivatePowerShot();
            player.ActivatePowerup();
            Destroy(this.gameObject);
        }
    }
}
