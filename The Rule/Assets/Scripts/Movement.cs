using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Movement : MonoBehaviour
{
    float speed;
    Vector2 movement;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Vector3 translation = movement * speed * Time.deltaTime;
        rb.MovePosition(translation + transform.position);
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement.normalized;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public bool IsMoving()
    {
        return (movement.magnitude) > 0f;
    }
}
