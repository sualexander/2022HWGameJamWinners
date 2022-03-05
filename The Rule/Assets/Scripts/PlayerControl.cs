using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : LawAbider
{
    const float speed = 30f;
    Movement move;

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
        if (CheckLaw())
        {
            Debug.Log("Following the law");
        }
        else
        {
            Debug.Log("Breaking the law");
        }
    }

    public Movement GetMovement()
    {
        return move;
    }

    bool CheckLaw()
    {
        bool ret = true;
        Law law = GetLaw();
        switch (law)
        {
            case Law.NO_MOVEMENT:
                return !move.IsMoving();
            default:
                break;
        }
        return ret;
    }

    bool CheckLaw(Action a)
    {
        bool ret = true;
        Law law = GetLaw();
        switch (law)
        {
            case Law.NO_MELEE:
                ret = a.action != Action.ActionType.MELEE;
                break;
            case Law.NO_RANGED:
                ret = a.action != Action.ActionType.RANGED;
                break;
            default:
                break;
        }
        return ret;
    }
    
}
