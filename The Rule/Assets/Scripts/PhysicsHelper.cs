using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHelper
{
    public static int GetLayerMask(int[] layers)
    {
        int mask = 0;
        for (int i = 0; i < layers.Length; i++)
        {
            mask += (int) Mathf.Pow(2f, layers[i]);
        }
        return mask;
    }

    public static bool MaskContainsLayer(int mask, int layer)
    {
        return ((mask & (1 << layer)) != 0);
    }
}
