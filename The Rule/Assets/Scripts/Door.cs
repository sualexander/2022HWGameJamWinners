using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Open()
    {
        isOpen = true;
        Debug.Log("Open");
    }
}
