using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : LawAbider
{
    const float speed = 30f;
    Movement move;
    float timer = 0f;
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetSpeed(speed);
    }

    void Update()
    {
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        move.SetMovement(movement);
        if (LawManager.instance.CheckLaw(this))
        {
            //Debug.Log("Following the law");
        }
        else
        {
            //Debug.Log("Breaking the law");
        }
    }

    public Movement GetMovement()
    {
        return move;
    }

    
    
}
