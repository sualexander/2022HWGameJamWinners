using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    public GameObject swipe;
    int mask;

    [SerializeField] AudioClip[] rangedAttackSounds;
    [SerializeField] AudioClip[] meleeAttackSounds;
    AudioSource audioSource;
    public void SetMask(int mask)
    {
        this.mask = mask;
        audioSource = GetComponent<AudioSource>();
    }

    public void MeleeAttack(float damage, Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 origin = pos + direction;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, .5f, direction * 0.75f, .01f, mask);

        Vector3 position = transform.position + new Vector3((direction * 1.2f).x, (direction * 1.2f).y, 0f);
        GameObject j = Instantiate(swipe, position, Quaternion.identity);
        float rot = Mathf.Atan(direction.y / direction.x) + Mathf.PI / 2;
        if (direction.x >= 0) rot += Mathf.PI;
        j.transform.Rotate(new Vector3(0f, 0f, rot * Mathf.Rad2Deg));

        for (int i = 0; i < hits.Length; i++)
        {
            GameObject obj = hits[i].collider.gameObject;
            Debug.Log("HIT (MELEE)! " + damage + " pts of damage.");
            obj.GetComponent<Movement>().Knockback(direction, damage);
            obj.GetComponent<Movement>().TakeDamage((int)damage);
            //AudioManager.instance.PlayAudio(meleeAttackSounds[Random.Range(0, meleeAttackSounds.Length - 1)]);
            Debug.Log("Meleed");
        }
        LawManager.instance.CheckLaw(new Action(Action.ActionType.MELEE));
    }

    public void RangedAttack(float damage, Vector2 direction, float speed, float radius)
    {
        Vector3 position = transform.position + new Vector3((direction * radius).x, (direction*radius).y, 0f);
        GameObject obj = Instantiate(projectile, position, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(damage, direction, speed, mask);
        LawManager.instance.CheckLaw(new Action(Action.ActionType.RANGED));
        //AudioManager.instance.PlayAudio(rangedAttackSounds[Random.Range(0, rangedAttackSounds.Length - 1)]);
        Debug.Log("Ranged");
    }
}
