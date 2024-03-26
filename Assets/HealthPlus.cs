using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlus : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Creature>() != null){
            other.GetComponent<Creature>().ActivateHealthPlus();
            Destroy(this.gameObject);
        }
    }
}
