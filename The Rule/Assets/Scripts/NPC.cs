using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : LawAbider
{
    Movement move;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Law law = GetLaw();
    }
}
