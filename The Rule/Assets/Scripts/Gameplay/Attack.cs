using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    int mask;

    public void SetMask(int mask)
    {
        this.mask = mask;
    }

    public void MeleeAttack(float damage, Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 origin = pos + direction;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, .5f, direction * 0.75f, .01f, mask);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject obj = hits[i].collider.gameObject;
            Debug.Log("HIT (MELEE)! " + damage + " pts of damage.");
            obj.GetComponent<Movement>().Knockback(direction, damage);
            obj.GetComponent<Movement>().TakeDamage((int)damage);
        }
        LawManager.instance.CheckLaw(new Action(Action.ActionType.MELEE));
    }

    public void RangedAttack(float damage, Vector2 direction, float speed, float radius)
    {
        Vector3 position = transform.position + new Vector3((direction * radius).x, (direction*radius).y, 0f);
        GameObject obj = Instantiate(projectile, position, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(damage, direction, speed, mask);
        LawManager.instance.CheckLaw(new Action(Action.ActionType.RANGED));
    }
}
