using UnityEngine;
using DG.Tweening;
public class Board : MonoBehaviour
{
    public static Board Instance;

    public void Awake()
    {
        Instance = this;
    }

    int[,] terrainMatrix = { { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 3, 2, 2, 3, 2, 2, 3 },
                            { 3, 2, 2, 3, 2, 2, 3 },
                            { 3, 2, 2, 3, 2, 2, 3 },
                            { 1, 1, 1, 1, 1, 1, 1 },
                            { 1, 1, 1, 1, 1, 1, 1 },
                            { 1, 1, 1, 1, 1, 1, 1 } };
    public Tile[,] map;
    public int ROW, COL;
    public GameObject tilePrefab;

    public Tile tileSelecting;
    public bool isSelecting;

    private void Start()
    {
        map = new Tile[ROW, COL];
        GenerateMap();
        GenerateHero();
    }

    public void GenerateMap()
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                map[i, j] = Instantiate(tilePrefab, transform).GetComponent<Tile>();
                map[i, j].gameObject.name = "Tile " + i + " " + j;
                map[i, j].Init(i, j);
                map[i, j].transform.localPosition = Vector3.right * (j - (int)(COL / 2)) + Vector3.up * ((int)(ROW / 2) - i);
                map[i, j].GetComponent<SpriteRenderer>().sprite = SpriteHolder.Instance.GetMapSpriteById(terrainMatrix[i, j]);
                if ((i + j) % 2 == 1 && terrainMatrix[i, j] < 2)
                {
                    map[i, j].GetComponent<SpriteRenderer>().sprite = SpriteHolder.Instance.offsetMap[terrainMatrix[i, j]];
                }
            }
        }
        transform.localScale = Vector3.one * .67f;
    }

    public void GenerateHero()
    {
        int[,] heroInit = { { 0, 4, 0, 5, 0, 4, 0 },
                            { 2, 0, 3, 0, 3, 0, 2 },
                            { 0, 1, 0, 0, 0, 1, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 }, // song
                            { 0, 6, 0, 0, 0, 6, 0 },
                            { 6, 0, 6, 0, 6, 0, 6 },
                            { 0, 7, 0, 8, 0, 7, 0 } };
        //int[,] heroInit = { { 0, 4, 0, 5, 0, 4, 0 },
        //                    { 2, 0, 3, 0, 3, 0, 2 },
        //                    { 0, 1, 0, 0, 0, 1, 0 },
        //                    { 0, 0, 0, 0, 0, 0, 0 },
        //                    { 0, 0, 0, 0, 0, 0, 0 },
        //                    { 0, 0, 0, 0, 0, 0, 0 },
        //                    { 0, 0, 0, 0, 0, 0, 0 }, // song
        //                    { 0, 6, 0, 0, 0, 0, 0 },
        //                    { 0, 0, 0, 0, 0, 0, 0 },
        //                    { 0, 7, 0, 0, 0, 7, 0 } };

        for (int i = 0; i < ROW; i++)
            for (int j = 0; j < COL; j++)
                map[i, j].hero.Init((HeroType)heroInit[i, j]);
    }

    public void UnSelect()
    {
        for (int i = 0; i < ROW; i++)
            for (int j = 0; j < COL; j++)
                map[i, j].GetComponent<Tile>().UnHighlight();
        isSelecting = false;
    }

    public void SelectTile(Tile tile)
    {
        UnSelect();
        tile.selecting.SetActive(true);
        if (map[tile.row, tile.col].hero.canGoStraight)
            HighlightTilesCanGoStraight(tile.row, tile.col);

        if (map[tile.row, tile.col].hero.canGoCross)
            HighlightTilesCanGoCross(tile.row, tile.col);

        tileSelecting = tile;
        isSelecting = true;
    }

    public bool CheckTerrain(int row, int collumn, HeroType type)
    {
        // 0 co 1 dat 2 nuoc 3 cau
        if (terrainMatrix[row, collumn] == 2) // is water
        {
            if (type == HeroType.Snake || type == HeroType.Poacher)
            {
                return true;
            }
            else return false;
        }
        else
            return true;
    }

    public void HighlightTilesCanGoStraight(int tileRow, int tileCol)
    {
        int[] dr = { -1, 0, 1, 0 };
        int[] dc = { 0, 1, 0, -1 };

        for (int k = 0; k < 4; k++)
        {
            int r = dr[k] + tileRow;
            int c = dc[k] + tileCol;
            if (r >= 0 && r < ROW && c >= 0 && c < COL)
            {
                int mode = -1;
                if (CheckCanAttack(map[tileRow, tileCol].hero.type, map[r, c].hero.type))
                    mode = 1;
                else if (map[r, c].hero.type == HeroType.None && CheckTerrain(r, c, map[tileRow, tileCol].hero.type))
                    mode = 2;

                map[r, c].Highlight(mode);
            }
        }
    }
    public void HighlightTilesCanGoCross(int tileRow, int tileCol)
    {
        int[] dr = { -1, -1, 1, 1 };
        int[] dc = { -1, 1, 1, -1 };

        for (int k = 0; k < 4; k++)
        {
            int r = dr[k] + tileRow;
            int c = dc[k] + tileCol;
            if (r >= 0 && r < ROW && c >= 0 && c < COL)
            {
                int mode = -1;
                if (CheckCanAttack(map[tileRow, tileCol].hero.type, map[r, c].hero.type))
                    mode = 1;
                else if (map[r, c].hero.type == HeroType.None && CheckTerrain(r, c, map[tileRow, tileCol].hero.type))
                    mode = 2;

                map[r, c].Highlight(mode);
            }
        }
    }

    public bool CheckCanAttack(HeroType atk, HeroType def)
    {
        if (atk == HeroType.Rat && def == HeroType.Saw)
            return true;
        else if (atk == HeroType.Snake && def == HeroType.Hunter)
            return true;
        else if (atk == HeroType.Elephant && def == HeroType.Poacher)
            return true;
        else if (atk == HeroType.Lion && def == HeroType.Poacher)
            return true;
        else if (atk == HeroType.Hunter && def == HeroType.Elephant)
            return true;
        else if (atk == HeroType.Hunter && def == HeroType.Lion)
            return true;
        else if (atk == HeroType.Hunter && def == HeroType.Rat)
            return true;
        else if (atk == HeroType.Poacher && def == HeroType.Snake)
            return true;
        else if (atk == HeroType.Saw && def == HeroType.Base)
            return true;
        else if (atk == HeroType.Poacher && def == HeroType.Base)
            return true;
        else if (atk == HeroType.Snake && def == HeroType.Poacher)
            return true;
        return false;
    }

    public void Swap(Tile a, Tile b)
    {
        a.hero.transform.parent = b.transform;
        a.hero.transform.localPosition = Vector3.zero;

        b.hero.transform.parent = a.transform;
        b.hero.transform.localPosition = Vector3.zero;

        a.hero = a.GetComponentInChildren<Hero>();
        b.hero = b.GetComponentInChildren<Hero>();
    }

    public void MoveHero(Tile targetTile)
    {
        UnSelect();
        PlayMoveSound();
        tileSelecting.hero.transform.DOMove(targetTile.transform.position, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            tileSelecting.hero.transform.localPosition = Vector3.zero;
            Swap(tileSelecting, targetTile);
            GameController.Instance.SwitchTurn();
        });
    }

    public void AttackHero(Tile targetTile)
    {
        UnSelect();
        // move xong thi set lai vi tri + backend
        Vector3 halfDistance = (targetTile.transform.position - tileSelecting.transform.position) / 2;
        tileSelecting.hero.transform.DOMove(targetTile.transform.position + Vector3.back * 5 - halfDistance, 0.25f).SetEase(Ease.InSine).OnComplete(() =>
        {
            targetTile.hero.TakeDamage(tileSelecting.hero.damage);
            PlayAttackSound();
            targetTile.hero.transform.DOShakePosition(0.2f, 0.1f, 1, 0).SetLoops(2, LoopType.Yoyo);
            if (targetTile.hero.hp == 0) //dam chet luon
            {
                targetTile.hero.Disappear();
                tileSelecting.hero.transform.DOMove(targetTile.transform.position + Vector3.back * 5, 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
                    {
                        targetTile.hero.Init(HeroType.None);
                        Swap(tileSelecting, targetTile);
                        tileSelecting.hero.transform.localPosition = Vector3.zero;
                        GameController.Instance.SwitchTurn();
                    });
            }
            else
            {
                tileSelecting.hero.transform.DOMove(tileSelecting.transform.position, 0.25f).SetEase(Ease.InSine);
                GameController.Instance.SwitchTurn();
            }
        });
    }
    public void PlayMoveSound()
    {
        AudioManager.Instance.Play("MoveSound");
    }
    public void PlayAttackSound()
    {
        switch (tileSelecting.hero.type)
        {
            case HeroType.Snake:
                AudioManager.Instance.Play("SnakeSound");
                break;
            case HeroType.Elephant:
                AudioManager.Instance.Play("ElephantSound");
                break;
            case HeroType.Rat:
                AudioManager.Instance.Play("RatSound");
                break;
            case HeroType.Lion:
                AudioManager.Instance.Play("LionRoar");
                break;
            case HeroType.Saw:
                AudioManager.Instance.Play("ChainSaw");
                break;
            default:
                break;
        }
    }
    public bool IsHumanAlive()
    {
        int saw = 0, poacher = 0;
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                if (map[i, j].hero.type == HeroType.Saw) saw++;
                if (map[i, j].hero.type == HeroType.Poacher) poacher++;
            }
        }
        if (poacher == 0 && saw == 0)
            return false;
        return true;
    }
}