using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class NPC : LawAbider
{
    [HideInInspector]
    public Movement move;
    float fov = 90f;
    [SerializeField] string[] dialog;
    int currentDialog = 0;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetFixedPosition(true);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = collision.transform.position - transform.position;
            float angle = Vector2.Angle(direction, transform.forward);
            if (angle < fov / 2)
            {

            }
        }
    }

    public void Speak()
    {
        Debug.Log(dialog[currentDialog]);
        UIManager.instance.SetDialog(dialog[currentDialog]);
        if (currentDialog < dialog.Length - 1)
        {
            currentDialog++;
        }
    }
}
