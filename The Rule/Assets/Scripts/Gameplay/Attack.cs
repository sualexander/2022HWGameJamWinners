using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;

    void Start()
    {

    }

    void Update()
    {

    }

    public void MeleeAttack(float damage, Vector2 direction, int mask)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 origin = pos + direction;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 2f, direction, .1f, mask);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("HIT (MELEE)! " + damage + " pts of damage.");
        }
    }

    public void RangedAttack(float damage, Vector2 direction, float speed, float radius)
    {
        Vector3 position = transform.position + new Vector3((direction * radius).x, (direction*radius).y, 0f);
        GameObject obj = Instantiate(projectile, position, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(damage, direction, speed);
    }
}
