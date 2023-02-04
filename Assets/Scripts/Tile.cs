using UnityEngine;
public enum TileStatus
{
    None,
    CanMoveTo,
    CanAttack
}

public class Tile : MonoBehaviour
{
    public int row, col;
    public Hero hero;
    public SpriteRenderer highlight;
    public TileStatus status;

    public void Init(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void OnMouseDown()
    {
        if (Board.Instance.isSelecting)
        {
            if (status == TileStatus.CanMoveTo)
                Board.Instance.MoveHero(this);
            else if (status == TileStatus.CanAttack)
                Board.Instance.AttackHero(this);

            Board.Instance.UnSelect();
        }
        else
        {
            if (hero.type != HeroType.None)
                Board.Instance.SelectTile(this);
            else
                Board.Instance.UnSelect();
        }
    }

    public void Highlight(int mode) // 1 : can attack, 2 : can move
    {
        if (mode == 1)
        {
            highlight.sprite = SpriteHolder.Instance.canAtkSpr;
            status = TileStatus.CanAttack;
        }
        else if (mode == 2)
        {
            highlight.sprite = SpriteHolder.Instance.canMoveSpr;
            status = TileStatus.CanMoveTo;
        }
        else
        {
            highlight.sprite = null;
            status = TileStatus.None;
        }
    }

    public void UnHighlight()
    {
        highlight.sprite = null;
        status = TileStatus.None;
    }
}