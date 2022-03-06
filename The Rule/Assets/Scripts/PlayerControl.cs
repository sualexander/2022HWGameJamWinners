using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControl : LawAbider
{
    const float speed = 30f;
    Movement move;
    float timer = 0f;
    Attack atk;
    int atkMask;
    int money;
    [SerializeField] float maxDistanceFromNPC = 20f;
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetSpeed(speed);
        atk = GetComponent<Attack>();
        atkMask = PhysicsHelper.GetLayerMask(new int[] { 7 });
        atk.SetMask(atkMask);
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
            atk.RangedAttack(1f, direction, 35f, 1f);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atk.MeleeAttack(1f, direction);
        }
        if (LawManager.instance.CheckLaw(this))
        {
            //Debug.Log("Following the law");
        }
        else
        {
            //Debug.Log("Breaking the law");
            UIManager.instance.FadeOut();
        }

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
    }
    
}
