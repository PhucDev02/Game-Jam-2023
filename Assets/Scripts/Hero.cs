using UnityEngine;
using DG.Tweening;
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
    public SpriteRenderer spriteRenderer, health;
    public HeroType type;
    public int hp, damage;
    public bool canGoStraight, canGoCross, canSwim;
    public SpriteRenderer bottom;

    private void Awake()
    {
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
                canGoStraight = true;
                canGoCross = false;
                canSwim = true;
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
                canGoCross = false;
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
                canSwim = true;
                break;
            case HeroType.Hunter:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[7];
                hp = 1;
                damage = 1;
                canGoStraight = true;
                canGoCross = false;
                break;
            case HeroType.Saw:
                spriteRenderer.sprite = SpriteHolder.Instance.HeroSprites[8];
                hp = 2;
                damage = 1;
                canGoStraight = true;
                canGoCross = true;
                break;
        }
        Appear();
        if (type == HeroType.None || type == HeroType.Base)
            health.sprite = null;
        else
            health.sprite = SpriteHolder.Instance.fullHealth;
        UpdateBottom();
    }
    public void UpdateBottom()
    {
        if ((int)type >= 6 && (int)type <= 8)
        {
            bottom.sprite = SpriteHolder.Instance.humanBottom;
        }
        else if ((int)type >= 1 && (int)type <= 5)
        {
            bottom.sprite = SpriteHolder.Instance.animalBottom;
        }
        else bottom.sprite = null;
    }
    public void Disappear()
    {
        spriteRenderer.DOFade(0, 0.5f);
    }
    public void Appear()
    {
        transform.DOLocalMoveY(0.4f, 0);
        transform.DOLocalMoveY(0f, 0.6f).SetEase(Ease.OutCubic);
        spriteRenderer.DOFade(0, 0f);
        spriteRenderer.DOFade(1, 1f);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            health.sprite = null;
            if (type == HeroType.Base)
                GameController.Instance.GameOver(true);
        }
        else
        {
            health.sprite = SpriteHolder.Instance.halfHealth;
        }
    }
}