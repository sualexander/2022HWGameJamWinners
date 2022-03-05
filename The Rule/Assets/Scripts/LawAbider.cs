using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawAbider : MonoBehaviour
{
    protected enum Law
    {
        NO_MOVEMENT,
        NO_MELEE,
        NO_RANGE,
    };
    static Law currentLaw = Law.NO_MELEE;
    protected Law GetLaw()
    {
        return currentLaw;
    }
}
