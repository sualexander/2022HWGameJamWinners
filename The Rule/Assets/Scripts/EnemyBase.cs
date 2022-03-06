using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Seeker))]
public class EnemyBase : NPC
{
    public float speed = 100f;
    public float nextWaypointDistance = 3f;

    Vector3 target = Vector3.zero;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        move.SetSpeed(speed);
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, Vector3.zero, OnPathLoad);
        InvokeRepeating("UpdatePath", .5f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, Vector3.zero, OnPathLoad);
    }
    void OnPathLoad(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            move.SetMovement(Vector2.zero);
            reachedEndOfPath = true;
            return;
        } else if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            move.SetMovement(Vector2.zero);
            reachedEndOfPath = true;
            return;
        }
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        move.SetMovement(direction);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
