using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Damage : UnityEvent<int>
{
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Movement : MonoBehaviour
{
    float speed;
    Vector2 movement;
    Vector2 direction = Vector2.down;
    Rigidbody2D rb;

    public Damage takeDamage;

    Vector2 knockback;
    float knockbackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        Vector3 translation = movement * speed * Time.fixedDeltaTime;
        if (Time.time - knockbackTime < 0.1f)
        {
            translation += (Vector3)knockback * Time.fixedDeltaTime;
        }
        if (translation.sqrMagnitude > 0)
        {
            rb.MovePosition(translation + transform.position);
        }
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

    public void Knockback(Vector2 dir, float force)
    {
        knockback = dir * force * 12f;
        knockbackTime = Time.time;
    }

    public void TakeDamage(int dmg)
    {
        takeDamage.Invoke(dmg);
    }
}
