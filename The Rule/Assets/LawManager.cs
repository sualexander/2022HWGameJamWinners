using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawManager : MonoBehaviour
{
    protected enum Law
    {
        NO_MOVEMENT,
        NO_MELEE,
        NO_RANGED,
    };
    static Law currentLaw = Law.NO_MOVEMENT;

    public static LawManager instance;
    float timer = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0f;
            ChangeLaw();
            Debug.Log(GetLaw());
        }
    }

    
    protected Law GetLaw()
    {
        return currentLaw;
    }

    static void ChangeLaw()
    {
        // Change the max here to be the length of Law
        currentLaw = (Law)Random.Range(0, 3);
        UIManager.instance.PlayBugleSlide();
    }

    public bool CheckLaw(PlayerControl plr)
    {
        Movement move = plr.GetMovement();
        bool ret = true;
        Law law = GetLaw();
        switch (law)
        {
            case Law.NO_MOVEMENT:
                return !move.IsMoving();
            default:
                break;
        }
        return ret;
    }

    public bool CheckLaw(Action a)
    {
        bool ret = true;
        Law law = GetLaw();
        switch (law)
        {
            case Law.NO_MELEE:
                ret = a.action != Action.ActionType.MELEE;
                break;
            case Law.NO_RANGED:
                ret = a.action != Action.ActionType.RANGED;
                break;
            default:
                break;
        }
        return ret;
    }
}
