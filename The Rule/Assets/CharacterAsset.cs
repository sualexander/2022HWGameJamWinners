using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum animState
{
    Up,
    Down,
    Left,
    Right
}

[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/CharacterAsset")]
public class CharacterAsset : ScriptableObject
{
    public Sprite[] upCycle;
    public Sprite[] downCycle;
    public Sprite[] leftCycle;
    public Sprite[] rightCycle;
}
