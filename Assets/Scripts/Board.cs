using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public void Awake()
    {
        Instance = this;
    }

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
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                map[i, j] = Instantiate(tilePrefab, transform).GetComponent<Tile>();
                map[i, j].gameObject.name = "Tile " + i + " " + j;
                map[i, j].Init(i, j);
                map[i, j].transform.localPosition = Vector3.right * (j - (int)(COL / 2)) + Vector3.up * ((int)(ROW / 2) - i);
                map[i, j].GetComponent<SpriteRenderer>().sprite = SpriteHolder.Instance.GetMapSpriteById(terrainMatrix[i, j]);
                if ((i + j) % 2 == 1 && terrainMatrix[i,j] < 2)
                {
                    map[i, j].GetComponent<SpriteRenderer>().sprite = SpriteHolder.Instance.offsetMap[terrainMatrix[i, j]];
                }
            }
        }
        transform.localScale = Vector3.one * .65f;
    }

    public void GenerateHero()
    {
        int[,] heroInit = { { 2, 3, 4, 5, 4, 3, 2 },
                            { 1, 0, 7, 8, 6, 0, 1 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0 },
                            { 6, 0, 0, 0, 0, 0, 6 },
                            { 6, 6, 7, 8, 7, 6, 6 } };

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

        if (map[tile.row, tile.col].hero.canGoStraight)
            HighlightTilesCanGoStraight(tile.row, tile.col);

        if (map[tile.row, tile.col].hero.canGoCross)
            HighlightTilesCanGoCross(tile.row, tile.col);

        tileSelecting = tile;
        isSelecting = true;
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
                else if (map[r, c].hero.type == HeroType.None)
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
                else if (map[r, c].hero.type == HeroType.None)
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
        Swap(tileSelecting, targetTile);
    }

    public void AttackHero(Tile targetTile)
    {
        targetTile.hero.Init(HeroType.None);
        MoveHero(targetTile);
    }
}