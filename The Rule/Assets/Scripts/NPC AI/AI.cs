using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Seeker))]
public class AI : NPC
{
    public float speed = 100f;
    public float dmg = 1f;
    public int maxHealth = 5;

    public bool canKill = true;

    public int health;

    public float nextWaypointDistance = .5f;


    protected Vector3 target = Vector3.zero;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;
    
    protected Attack atk;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        move = GetComponent<Movement>();
        atk = GetComponent<Attack>();
        atk.SetMask(PhysicsHelper.GetLayerMask(new int[] { 6 }));
        move.SetSpeed(speed);

        target = Vector3.zero;

        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target, OnPathLoad);
        InvokeRepeating("UpdatePath", .5f, .5f);

        move.takeDamage.AddListener(Damaged);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target, OnPathLoad);
    }
    void OnPathLoad(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void Pathfind()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            move.SetMovement(Vector2.zero);
            reachedEndOfPath = true;
            return;
        }
        else if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            move.SetMovement(Vector2.zero);
            reachedEndOfPath = true;
            return;
        }
        reachedEndOfPath = false;
        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        move.SetMovement(direction);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    protected virtual void Update()
    {
        Pathfind();
    }

    void Damaged(int dmg)
    {
        health -= dmg;
        Debug.Log(health + " HEALTH REMAINING");

        if (health <= 0 && canKill)
        {
            Debug.Log("DEAD!");
            Destroy(gameObject);
        }
    }
}
