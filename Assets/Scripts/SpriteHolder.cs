using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public static SpriteHolder Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite[] HeroSprites;
    public Sprite canMoveSpr, canAtkSpr;
    public Sprite[] offsetMap; // 0 is grass 1 is ground
    public Sprite[] mapSprites; // 0 co 1 dat 2 nuoc 3 cau

    public Sprite GetMapSpriteById(int index)
    {
        return mapSprites[index];
    }

}