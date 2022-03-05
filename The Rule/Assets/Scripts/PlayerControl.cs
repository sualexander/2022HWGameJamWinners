using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Attack))]
public class PlayerControl : MonoBehaviour
{
    const float speed = 15f;

    Vector2 direction = Vector2.down;

    Movement move;
    Attack atk;

    int atkMask = 0;

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
        }
    }

    public Movement GetMovement()
    {
        return move;
    }
}
