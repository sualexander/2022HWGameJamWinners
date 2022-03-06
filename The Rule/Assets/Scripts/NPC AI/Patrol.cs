using UnityEngine;

public class Patrol : AI
{
    public Transform[] waypoints;
    int currentPatrolPoint = 0;

    Vector3 standardPosition;
    float playerRange = 20f * 20f; // make it the value you want squared, for performance reasons.
    Transform player;
    bool pursuit = false;

    float atkCooldown = .75f;
    float lastAtkTime = 0f;

    protected override void Start()
    {
        base.Start();
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            player = obj.transform;
        standardPosition = transform.position;
        target = waypoints[currentPatrolPoint].position;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 pos = transform.position;
        Vector3 pPos = player.position;

        if (!pursuit && PlayerWithinRange() && PlayerInSight())
        {
            pursuit = true;
        }
        if (pursuit)
        {
            if (!PlayerWithinRange())
            {
                pursuit = false;
                target = waypoints[currentPatrolPoint].position;
                return;
            }
            target = pPos;
            if (Time.time - lastAtkTime > atkCooldown && Vector2.Distance(pPos, pos) < 2f)
            {
                atk.MeleeAttack(dmg, (pPos - pos).normalized);
                lastAtkTime = Time.time;
            }
        } else
        {
            if (reachedEndOfPath)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint > waypoints.Length - 1) currentPatrolPoint = 0;
                target = waypoints[currentPatrolPoint].position;
                UpdatePath();
                reachedEndOfPath = false;
            }
        }
    }

    bool PlayerWithinRange()
    {
        if (player != null)
        {
            Vector3 pos = transform.position;
            Vector3 pPos = player.position;
            float distanceSquared = (pos.x - pPos.x) * (pos.x - pPos.x) + (pos.y - pPos.y) * (pos.y - pPos.y);
            return distanceSquared <= playerRange;
        }
        return false;
    }
    bool PlayerInSight()
    {
        Vector2 pos = transform.position;
        Vector2 pPos = player.position;
        RaycastHit2D hit = Physics2D.Raycast(pos, (pPos - pos).normalized, Vector2.Distance(pos, pPos), PhysicsHelper.GetLayerMask(new int[] { 8 }));
        if (hit)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length-1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
        Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }
}
