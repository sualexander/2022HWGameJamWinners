using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Projectile : MonoBehaviour
{
    const float life = 20f;
    float lifetime = 0f;
    float damage;
    Movement move;

    public void Setup(float damage, Vector2 dir, float speed)
    {
        this.damage = damage;
        move = GetComponent<Movement>();
        move.SetSpeed(speed);
        move.SetMovement(dir);
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > life)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT (RANGED)! " + damage + " pts of damage.");
        Destroy(gameObject);
    }
}
