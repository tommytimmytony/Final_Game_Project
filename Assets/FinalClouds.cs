using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalClouds : MonoBehaviour
{
    Rigidbody2D cloudRB;
    bool moveFinal = false;
    public void InitFinalClouds(){
        cloudRB = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveFinalClouds());
    }

    void Update()
    {
        if (cloudRB.position.x <= -2 && !moveFinal)
        {
            StopCloud(); 
        }
    }

    void StopCloud()
    {
        cloudRB.velocity = Vector2.zero;
    }

    IEnumerator MoveFinalClouds()
    {
        yield return new WaitForSeconds(60);
        MoveCloud();
    }

    void MoveCloud()
    {
        cloudRB.velocity = new Vector2(-3, 0); 
    }

    public void MoveFinal() {
        moveFinal = true; 
        cloudRB.velocity = new Vector2(-1, 0); 
    }
}
