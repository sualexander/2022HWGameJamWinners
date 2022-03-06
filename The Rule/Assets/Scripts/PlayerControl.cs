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

        atkMask = PhysicsHelper.GetLayerMask(new int[] { 7 });
        atk.SetMask(atkMask);
    }

    void Update()
    {
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        move.SetMovement(movement);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - pos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            atk.RangedAttack(1f, direction, 35f, 1f);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(1f, direction);
        }
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
