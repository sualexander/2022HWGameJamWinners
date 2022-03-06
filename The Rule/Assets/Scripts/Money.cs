using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int value;

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
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
