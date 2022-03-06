using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Movement))]
public class PlayerControl : LawAbider
{
    const float speed = 5f;
    Movement move;
    float timer = 0f;
    Attack atk;
    int atkMask;
    int money = 100;

    const int maxHealth = 4;
    int health;

    SpriteRenderer blindness;

    [SerializeField] float maxDistanceFromNPC = 20f;

    void Start()
    {
        move = GetComponent<Movement>();
        move.SetSpeed(speed);

        atk = GetComponent<Attack>();
        atkMask = PhysicsHelper.GetLayerMask(new int[] { 7 });
        atk.SetMask(atkMask);

        health = maxHealth;
        move.takeDamage.AddListener(Damaged);
        if (UIManager.instance != null) UIManager.instance.SetHealth(health);
        if (UIManager.instance != null) UIManager.instance.SetGold(money);
        blindness = transform.Find("Blindness").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (LawManager.instance.GetLaw() == LawManager.Law.WALK_REVERSE) movement *= -1;
        if (LawManager.instance.GetLaw() == LawManager.Law.CLOSE_EYES) blindness.color = Color.white;
        else blindness.color = Color.clear;
        move.SetMovement(movement);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - pos).normalized;
        if (LawManager.instance.GetLaw() == LawManager.Law.FACE_REVERSE) direction *= -1;

        if (Input.GetMouseButtonDown(0))
        {
            if (money >= 5)
            {
                money -= 5;
                atk.RangedAttack(1f, direction, 30f, 1f);
                if (UIManager.instance) UIManager.instance.SetGold(money);
                LawManager.instance.CheckLaw(new Action(Action.ActionType.RANGED));
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(3f, direction);
            LawManager.instance.CheckLaw(new Action(Action.ActionType.MELEE));
        }

        if(Input.GetKeyDown("m"))
        {
            LawManager.instance.Thrown();
            money = 0;
            if (UIManager.instance) UIManager.instance.SetGold(money);
        }

        LawManager.instance.CheckLaw(this);
        // NPC interaction
        Debug.DrawRay(transform.position, maxDistanceFromNPC * direction);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, direction, maxDistanceFromNPC,
                LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<NPC>().Speak();
                move.SetFixedPosition(true);
                timer = 0;
            }
        }
        timer += Time.deltaTime;
        if (timer > 0.5f) move.SetFixedPosition(false);
    }

    public Movement GetMovement()
    {
        return move;
    }

    // Called by collectables when the player enters their trigger.
    public void CollectMoney(Money m)
    {
        int value = m.Value;
        money += value;
        health += value / 10;
        if (health > 4) health = 4;
        Debug.Log(health);
        if (UIManager.instance) UIManager.instance.SetHealth(health);
        if (UIManager.instance) UIManager.instance.SetGold(money);
    }

    void Damaged(int dmg)
    {
        health -= dmg;
        if (UIManager.instance) UIManager.instance.SetHealth(health);
        if (health < 0)
        {
            Death();
        }
    }

    void Death()
    {
        UIManager.instance.FadeOut();
        Invoke("LoadJail", 1.5f);
    }

    void LoadJail()
    {
        StartCoroutine(LoadScene("Jail"));
    }
    IEnumerator LoadScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
