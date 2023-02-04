using UnityEngine;

public enum HeroType
{
    None = 0,
    Snake,
    Elephant,
    Rat,
    Lion,
    Base,
    Poacher,
    Hunter,
    Saw,
}

public class Hero : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public HeroType type;
    public int hp, damage;
    public bool canGoStraight, canGoCross, canSwim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(HeroType type)
    {
        this.type = type;

        switch (type)
        {
            case HeroType.None:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[0];
                break;
            case HeroType.Snake:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[1];
                hp = 1;
                damage = 1;
                canGoStraight = false;
                canGoCross = true;
                break;
            case HeroType.Elephant:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[2];
                hp = 2;
                damage = 1;
                canGoStraight = true;
                canGoCross = false;
                break;
            case HeroType.Rat:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[3];
                hp = 1;
                damage = 1;
                canGoStraight = true;
                canGoCross = true;
                break;
            case HeroType.Lion:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[4];
                hp = 1;
                damage = 1;
                canGoStraight = true;
                canGoCross = true;
                break;
            case HeroType.Base:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[5];
                hp = 1;
                damage = 0;
                break;
            case HeroType.Poacher:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[6];
                hp = 1;
                damage = 1;
                canGoStraight = true;
                canGoCross = false;
                break;
            case HeroType.Hunter:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[7];
                hp = 1;
                damage = 1;
                canGoStraight = true;
                canGoCross = true;
                break;
            case HeroType.Saw:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[8];
                hp = 2;
                damage = 1;
                canGoStraight = true;
                canGoCross = true;
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //dead ;
        }
        else
        {
            // effect tru mau
        }
    }
}