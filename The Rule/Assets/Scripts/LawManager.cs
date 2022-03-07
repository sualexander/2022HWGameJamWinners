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

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
            return;
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0f;
            Law newLaw = (Law)Random.Range(0, 9);
            newLaw = Law.CLOSE_EYES;
            Debug.Log(newLaw);
            StartCoroutine(ChangeLaw(newLaw));
            string law;
            switch (newLaw)
            {
                case Law.THROW_MONEY:
                    law = "Fling your worldly goods to the ground! (Press [M])";
                    break;
                case Law.DONT_STOP:
                    law = "Dance for me!";
                    break;
                case Law.NO_MELEE:
                    law = "Shun the blade!";
                    break;
                case Law.NO_RANGED:
                    law = "Shun the bow!";
                    break;
                case Law.WALK_REVERSE:
                    law = "Walk backwards!";
                    break;
                case Law.FACE_REVERSE:
                    law = "Never meet your enemy's gaze!";
                    break;
                case Law.ZIGZAG:
                    law = "Shun the straight path!";
                    break;
                case Law.COVER_EARS:
                    law = "Cover your ears!";
                    break;
                case Law.CLOSE_EYES:
                    law = "Shield your gaze!";
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
                return move.IsZigZagging();
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
                Debug.Log("HI");
                break;
            case Law.NO_RANGED:
                Debug.Log("HI");
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
