using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : MonoBehaviour
{
    const float speed = 10f;
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
    }
}
