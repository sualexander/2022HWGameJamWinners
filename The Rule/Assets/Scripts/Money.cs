using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int value;

    public Sprite[] sprites;

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (value <= 10)
            {
                sr.sprite = sprites[0];
            } else if (value <= 20)
            {
                sr.sprite = sprites[1];
            } else
            {
                sr.sprite = sprites[2];
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().CollectMoney(this);
            Destroy(gameObject);
            Debug.Log("money was picked up!");
        }
    }
}
