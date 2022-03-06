using UnityEngine;

public class Sentinel : AI
{
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
        target = standardPosition;
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
            target = pPos;
        }
        if (pursuit && !PlayerWithinRange())
        {
            pursuit = false;
            target = standardPosition;
            return;
        }
        if (pursuit)
        {
            Debug.Log(target);
            if (Time.time - lastAtkTime > atkCooldown && Vector2.Distance(pPos, pos) < 2f)
            {
                atk.MeleeAttack(dmg, (pPos - pos).normalized);
                lastAtkTime = Time.time;
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
        RaycastHit2D hit = Physics2D.Raycast(pos, (pPos-pos).normalized, Vector2.Distance(pos, pPos), PhysicsHelper.GetLayerMask(new int[] { 8 }));
        if (hit)
        {
            return false;
        } else
        {
            return true;
        }
    }
}
