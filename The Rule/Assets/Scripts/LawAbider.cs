using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawAbider : MonoBehaviour
{
    protected enum Law
    {
        NO_MOVEMENT,
        NO_MELEE,
        NO_RANGED,
    };
    static Law currentLaw = Law.NO_MOVEMENT;
    protected Law GetLaw()
    {
        return currentLaw;
    }
}
