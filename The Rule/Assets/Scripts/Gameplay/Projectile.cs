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
    int mask;

    public void Setup(float damage, Vector2 dir, float speed, int mask)
    {
        this.damage = damage;
        this.mask = mask;
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
        GameObject obj = collision.gameObject;
        if (PhysicsHelper.MaskContainsLayer(mask, obj.layer))
        {
            Debug.Log("HIT (RANGED)! " + damage + " pts of damage.");
        }
        if (obj.layer != 6)
        {
            Destroy(gameObject);
        }
    }
}
