using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : LawAbider
{
    const float speed = 10f;
    Movement move;
    Attack atk;
    int atkMask;
    //float timer = 0f;
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetSpeed(speed);

        atk = GetComponent<Attack>();
        atkMask = PhysicsHelper.GetLayerMask(new int[] { 7 });
        atk.SetMask(atkMask);
    }

    void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        move.SetMovement(movement);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - pos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            atk.RangedAttack(1f, direction, 30f, 1f);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(1f, direction);
        }
        //if (LawManager.instance.CheckLaw(this))
        //{
            //Debug.Log("Following the law");
        //}
        //else
        //{
            //Debug.Log("Breaking the law");
        //}
    }

    public Movement GetMovement()
    {
        return move;
    }

    
    
}
