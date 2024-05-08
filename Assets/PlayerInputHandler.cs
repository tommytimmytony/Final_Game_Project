using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Creature player;
    ProjectileThrower projectileThrower;

    void Start() {
        projectileThrower = player.GetComponent<ProjectileThrower>();
    }

    void Update()
    {
        Vector3 input = Vector3.zero;
       
        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            player.Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.P)) {
            if (Input.GetKey(KeyCode.W)) {projectileThrower.LaunchUp();}
            else if (Input.GetKey(KeyCode.S)) { projectileThrower.LaunchDown();}
            else {  projectileThrower.Launch(player.body.transform.localScale.x);}
        }
        
        player.MoveCreature(input);
    }

    
}
