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

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, Vector3.zero, OnPathLoad);
        move.SetSpeed(speed);
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
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Debug.Log(direction);
        move.SetMovement(direction);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void OnPathLoad(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
