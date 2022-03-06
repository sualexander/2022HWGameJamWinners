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
    Vector2 dir;

    public void Setup(float damage, Vector2 dir, float speed, int mask)
    {
        this.damage = damage;
        this.mask = mask;
        this.dir = dir;
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
            obj.GetComponent<Movement>().Knockback(dir, damage * 0.6f);
            obj.GetComponent<Movement>().TakeDamage((int)damage);
        }
        Destroy(gameObject);
    }
}
