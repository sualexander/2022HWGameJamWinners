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
    int money;

    const int maxHealth = 4;
    int health;

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
    }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        move.SetMovement(movement);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - pos).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            if (money >= 5)
            {
                money -= 5;
                atk.RangedAttack(1f, direction, 30f, 1f);
                if (UIManager.instance) UIManager.instance.SetGold(money);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(3f, direction);
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
        money += m.Value;
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
        SceneManager.LoadScene("Jail");
    }

}
