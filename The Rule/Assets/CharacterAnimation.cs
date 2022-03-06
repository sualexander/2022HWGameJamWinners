using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{
    Up,
    Down,
    Left,
    Right
}

public class CharacterAnimation : MonoBehaviour
{
    public CharacterAsset assets;

    private Dir _direction;
    public Dir direction;
    private int _frame;
    public int frame;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        frame = 0;
        direction = Dir.Down;
    }

    private void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        Sprite[] sprites;
        switch(direction)
        {
            case Dir.Up:
                sprites = assets.upCycle;
                break;
            case Dir.Down:
                sprites = assets.downCycle;
                break;
            case Dir.Left:
                sprites = assets.leftCycle;
                break;
            case Dir.Right:
                sprites = assets.rightCycle;
                break;
            default:
                sprites = assets.rightCycle;
                break;
        }
        sprite.sprite = sprites[frame];
    }
}
