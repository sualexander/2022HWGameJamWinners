using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LawManager : MonoBehaviour
{

    public enum Law
    {
        THROW_MONEY,
        DONT_STOP,
        NO_MELEE,
        NO_RANGED,
        WALK_REVERSE,
        FACE_REVERSE,
        ZIGZAG,
        COVER_EARS,
        CLOSE_EYES
    };
    static Law currentLaw = Law.NO_RANGED;

    public static LawManager instance;
    public GameObject meleeGuard;
    public AudioClip bugle;
    bool hasSpawnedGuards = false;
    float timer = 0;

    bool thrown = false;

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
        if (SceneManager.GetActiveScene().name == "Hub")
            return;
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            if (currentLaw == Law.THROW_MONEY && !thrown)
            {
                Debug.Log("You broke the law...");
                UIManager.instance.Alert();
                Vector2 posToSpawn = GameObject.Find("Player").transform.position;

                Instantiate(meleeGuard, new Vector3(posToSpawn.x - 10, posToSpawn.y, 0), Quaternion.identity);
                Instantiate(meleeGuard, new Vector3(posToSpawn.x + 10, posToSpawn.y, 0), Quaternion.identity);
                hasSpawnedGuards = true;
            }
            timer = 0f;
            Law newLaw = (Law)Random.Range(0, 9);
            StartCoroutine(ChangeLaw(newLaw));
            string law;
            thrown = false;
            switch (newLaw)
            {
                case Law.THROW_MONEY:
                    law = "Press [M] to throw your money!";
                    break;
                case Law.DONT_STOP:
                    law = "You shall not stop moving!";
                    break;
                case Law.NO_MELEE:
                    law = "You shall not use melee weapons!";
                    break;
                case Law.NO_RANGED:
                    law = "You shall not use ranged weapons!";
                    break;
                case Law.WALK_REVERSE:
                    law = "You shall move in reverse!";
                    break;
                case Law.FACE_REVERSE:
                    law = "You shall always look backwards!";
                    break;
                case Law.ZIGZAG:
                    law = "You shall move in a zig zag.";
                    break;
                case Law.COVER_EARS:
                    law = "You shall cover your ears";
                    break;
                case Law.CLOSE_EYES:
                    law = "You shall close your eyes";
                    break;
                default:
                    law = "Anarchy REIGNS!";
                    break;
            }
            UIManager.instance.PlayBugleSlide(law);
            AudioManager.instance.PlayAudio(bugle);
            hasSpawnedGuards = false;
        }
    }

    public void Thrown()
    {
        thrown = true;
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
    }

    public bool CheckLaw(PlayerControl plr)
    {
        Movement move = plr.GetMovement();
        bool ret = true;
        switch (currentLaw)
        {
            case Law.DONT_STOP:
                ret = move.IsMoving();
                break;
            case Law.ZIGZAG:
                ret = move.IsZigZagging();
                break;
            default:
                break;
        }
        if (!ret && !hasSpawnedGuards)
        {
            Debug.Log("You broke the law...");
            UIManager.instance.Alert();
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
            UIManager.instance.Alert();
            Vector2 posToSpawn = GameObject.Find("Player").transform.position;

            Instantiate(meleeGuard, new Vector3(posToSpawn.x - 10, posToSpawn.y, 0), Quaternion.identity);
            Instantiate(meleeGuard, new Vector3(posToSpawn.x + 10, posToSpawn.y, 0), Quaternion.identity);
            hasSpawnedGuards = true;
        }
        return ret;
    }
}
