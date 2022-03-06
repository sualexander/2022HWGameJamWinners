using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawAbider : MonoBehaviour
{
    static LawManager.Law currentLaw = LawManager.Law.DONT_STOP;
    protected LawManager.Law GetLaw()
    {
        return currentLaw;
    }

    protected static void ChangeLaw()
    {
        // Change the max here to be the length of Law
        currentLaw = (LawManager.Law)Random.Range(0, 3);
    }
}
