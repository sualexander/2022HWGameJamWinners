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

    Animator anim;

    float straightWalkingTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (transform.childCount > 0)
        {

            GameObject obj = transform.GetChild(0).gameObject;
            anim = obj.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (anim != null)
        {
            anim.SetFloat("dirX", direction.x);
            anim.SetFloat("dirY", direction.y);
            anim.SetFloat("speed", movement.magnitude);
        }
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

        straightWalkingTime += Time.fixedDeltaTime;
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement.normalized;
        if (movement != Vector2.zero)
        {
            direction = movement;
        }
        if ((movement.x != 0 && movement.y != 0) || movement == Vector2.zero)
        {
            straightWalkingTime = 0f;
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

    public bool IsZigZagging()
    {
        return !(straightWalkingTime > 0.25) ;
    }
}
