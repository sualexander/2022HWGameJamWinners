using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class NPC : LawAbider
{
    [HideInInspector]
    public Movement move;
    float fov = 90f;

    void Start()
    {
        move = GetComponent<Movement>();
    }

    void Update()
    {
        Law law = GetLaw();
        switch (law)
        {
            case Law.NO_MOVEMENT:
                break;
            case Law.NO_MELEE:
                break;
            case Law.NO_RANGED:
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 direction = collision.transform.position - transform.position;
            float angle = Vector2.Angle(direction, transform.forward);
            if (angle < fov / 2)
            {

            }
        }
    }
}
