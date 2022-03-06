using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class NPC : LawAbider
{
    public Movement move;
    float fov = 90f;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
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
