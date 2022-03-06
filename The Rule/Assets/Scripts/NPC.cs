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
    [SerializeField] GameObject money;
    int currentDialog = 0;
    bool hasThrownMoney = false;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetFixedPosition(true);
    }

    void Update()
    {
        LawManager.Law law = LawManager.instance.GetLaw();
        switch (law)
        {
            case LawManager.Law.DONT_STOP:
                hasThrownMoney = false;
                break;
            case LawManager.Law.NO_MELEE:
                hasThrownMoney = false;

                break;
            case LawManager.Law.NO_RANGED:
                hasThrownMoney = false;

                break;
            case LawManager.Law.THROW_MONEY:
                if (!hasThrownMoney)
                {
                    Instantiate(money, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                    Debug.Log("Money thrown...");
                    hasThrownMoney = true;
                }
                break;
            default:
                break;
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
