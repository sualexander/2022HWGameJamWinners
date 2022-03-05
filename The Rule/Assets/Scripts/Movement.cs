using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Movement : MonoBehaviour
{
    float speed;
    Vector2 movement;
    Vector2 direction = Vector2.down;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        Vector3 translation = movement * speed * Time.fixedDeltaTime;
        if (translation.sqrMagnitude > 0)
            rb.MovePosition(translation + transform.position);
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement.normalized;
        if (movement != movement.normalized)
        {
            direction = movement;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public bool IsMoving()
    {
        return (movement.magnitude) > 0f;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetFixedPosition(bool fix)
    {
        if (fix)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
