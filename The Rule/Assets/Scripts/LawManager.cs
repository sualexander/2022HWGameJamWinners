using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawManager : MonoBehaviour
{
    public enum Law
    {
        THROW_MONEY,
        NO_MOVEMENT,
        NO_MELEE,
        NO_RANGED,
    };
    static Law currentLaw = Law.NO_MOVEMENT;

    public static LawManager instance;
    public GameObject meleeGuard;
    bool hasSpawnedGuards = false;
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
            Law newLaw = (Law)Random.Range(0, 4);
            UIManager.instance.PlayBugleSlide();
            Debug.Log(newLaw);
            StartCoroutine(ChangeLaw(newLaw));
            hasSpawnedGuards = false;
        }
    }

    
    public Law GetLaw()
    {
        return currentLaw;
    }

    private IEnumerator ChangeLaw(Law law)
    {
        // Change the second argument to Random to be the length of Law
        yield return new WaitForSeconds(2);
        currentLaw = law;
        UIManager.instance.PlayBugleSlide();
    }

    public bool CheckLaw(PlayerControl plr)
    {
        Movement move = plr.GetMovement();
        bool ret = true;
        switch (currentLaw)
        {
            case Law.NO_MOVEMENT:
                ret = !move.IsMoving();
                break;
            default:
                break;
        }
        if (!ret && !hasSpawnedGuards)
        {
            Debug.Log("You broke the law...");
            Vector2 posToSpawn = plr.gameObject.transform.position;
            Instantiate(meleeGuard, new Vector3(posToSpawn.x - 10, posToSpawn.y, 0), Quaternion.identity);
            Instantiate(meleeGuard, new Vector3(posToSpawn.x + 10, posToSpawn.y, 0), Quaternion.identity);
            hasSpawnedGuards = true;
        }
        return ret;
    }

    public bool CheckLaw(Action a)
    {
        bool ret = true;
        Law law = GetLaw();
        switch (currentLaw)
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
        if (!ret)
        {
            Debug.Log("You broke the law...");
            UIManager.instance.FadeOut();
        }
        return ret;
    }
}
