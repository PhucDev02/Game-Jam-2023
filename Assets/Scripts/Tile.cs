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
    public GameObject selecting;
    public void Init(int row, int col)
    {
        this.row = row;
        this.col = col;
        UnHighlight();
    }

    private void OnMouseDown()
    {
        if (Board.Instance.isSelecting)
        {
            if (status == TileStatus.CanMoveTo)
                Board.Instance.MoveHero(this);
            else if (status == TileStatus.CanAttack)
                Board.Instance.AttackHero(this);
            else SelectTile();
        }
        else
        {
            SelectTile();
        }
    }
    public void SelectTile()
    {
        if (hero.type != HeroType.None)
        {
            if (GameController.Instance.isHumanTurn)
            {
                if ((int)hero.type >= 6 && (int)hero.type <= 8)
                    Board.Instance.SelectTile(this);
            }
            else if ((int)hero.type >= 1 && (int)hero.type <= 5)
                Board.Instance.SelectTile(this);
        }
        else
            Board.Instance.UnSelect();
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
        selecting.SetActive(false);
        highlight.sprite = null;
        status = TileStatus.None;
    }
}