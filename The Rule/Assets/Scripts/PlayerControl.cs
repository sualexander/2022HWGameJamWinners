using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Attack))]
public class PlayerControl : LawAbider
{
    const float speed = 15f;

    Vector2 direction = Vector2.down;

    Movement move;
    Attack atk;

    int atkMask = 0;
    float timer = 0f;
    
    void Start()
    {
        atk = GetComponent<Attack>();
        move = GetComponent<Movement>();
        move.SetSpeed(speed);

        atkMask = PhysicsHelper.GetLayerMask(new int[] { 7 });
    }

    void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        move.SetMovement(movement);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - pos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            atk.RangedAttack(1f, direction, 35f, 1f);
        } else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(1f, direction, atkMask);
        if (CheckLaw())
        {
            //Debug.Log("Following the law");
        }
        else
        {
            //Debug.Log("Breaking the law");
        }
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            timer = 0f;
            ChangeLaw();
            Debug.Log(GetLaw());
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
