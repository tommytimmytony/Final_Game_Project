using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigClouds : MonoBehaviour
{
    Rigidbody2D cloudRB;
    void Start (){
        cloudRB = GetComponent<Rigidbody2D>();
    }
    
     void Update()
    {
        if (cloudRB.position.x <= 8)
        {
            cloudRB.velocity = Vector2.zero; 
        }
    }

    public void InitBigClouds(float duration) {
        StartCoroutine(MoveCloud(duration));
    }
    
    IEnumerator MoveCloud(float duration)
    {
        yield return new WaitForSeconds(duration);
        Rigidbody2D cloudRB = GetComponent<Rigidbody2D>();
        cloudRB.velocity = new Vector2(-4, 0);
    }

}
