using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    // Start is called before the first frame update
    public enum ActionType
    {
        MELEE,
        RANGED
    }
    public ActionType action;
    public Action(ActionType at)
    {
        action = at;
    }
}
