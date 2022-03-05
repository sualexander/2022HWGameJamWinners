using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Movement : MonoBehaviour
{
    float speed;
    Vector2 movement;

    void Start()
    {

    }

    void Update()
    {
        Vector3 translation = new Vector3(movement.x, movement.y, 0f) * speed * Time.deltaTime;
        transform.Translate(translation);
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement.normalized;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
