using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonGameObject<TileManager>
{
    [SerializeField]
    private GameObject TileMapObject;

    [SerializeField]
    private TileObject TilePrefab;

    private List<List<TileObject>>[] TileLists;

    public int num;

    public int TryCreateTileNum;

    private void Awake()
    {
        TileManager t = TileManager.Instance;
    }

    protected override void Init()
    {
        TileLists = new List<List<TileObject>>[4];

        for (int i = 0; i < TileLists.Length; i++)
        {
            TileLists[i] = new List<List<TileObject>>();

            for (int j = 0; j < TileLists[i].Count; j++)
            {
                TileLists[i][j] = new List<TileObject>();
            }
        }

        CreateTile(Vector2Int.zero);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKey(KeyCode.J))
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2Int pos = Vector2Int.zero;

                while (true)
                {
                    Vector2Int tmp = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));

                    if (tmp.x * tmp.y < 0)
                        continue;

                    pos += tmp;

                    TileObject tile = FindTile(pos);

                    if (tile == null)
                    {
                        CreateTile(pos);
                        break;
                    }
                }
            }
        }
    }

    public TileObject CreateTile(Vector2Int position)
    {
        TryCreateTileNum += 1;

        List<List<TileObject>> tilelist = GetTileList(position);
        Vector2Int index = new Vector2Int(Mathf.Abs(position.x), Mathf.Abs(position.y));


        if (tilelist.Count - 1 < index.x || tilelist[index.x].Count - 1 < index.y)
        {
            while (tilelist.Count - 1 < index.x)
            {
                tilelist.Add(new List<TileObject>());
            }

            while (tilelist[index.x].Count - 1 < index.y)
            {
                tilelist[index.x].Add(null);
            }
        }

        if (tilelist[index.x][index.y] != null)
            return tilelist[index.x][index.y];

        tilelist[index.x][index.y] = Instantiate(TilePrefab, TileMapObject.transform);
        tilelist[index.x][index.y].TilePosition = position;
        tilelist[index.x][index.y].transform.position = new Vector2(1f * position.x - (0.5f * position.y), 0.87f * position.y);

        tilelist[index.x][index.y].ConnectAroundTiles(2);

        if (index.x == 0 && index.y == 0)
        {
            for (int i = 0; i < TileLists.Length; i++)
            {
                while (TileLists[i].Count - 1 < index.x)
                {
                    TileLists[i].Add(new List<TileObject>());
                }

                while (TileLists[i][index.x].Count - 1 < index.y)
                {
                    TileLists[i][index.x].Add(null);
                }


                TileLists[i][index.x][index.y] = tilelist[index.x][index.y];
            }
        }

        if (index.x == 0 || index.y == 0)
        {
            if (index.x == 0 && 0 < index.y)
            {
                for (int i = 0; i == 2; i = 1)
                {
                    while (TileLists[i].Count - 1 < index.x)
                    {
                        TileLists[i].Add(new List<TileObject>());
                    }

                    while (TileLists[i][index.x].Count - 1 < index.y)
                    {
                        TileLists[i][index.x].Add(null);
                    }


                    TileLists[i][index.x][index.y] = tilelist[index.x][index.y];

                    i++;
                }
            }
            if (index.x == 0 && index.y < 0)
            {
                for (int i = 2; i == 4; i = 3)
                {
                    while (TileLists[i].Count - 1 < index.x)
                    {
                        TileLists[i].Add(new List<TileObject>());
                    }

                    while (TileLists[i][index.x].Count - 1 < index.y)
                    {
                        TileLists[i][index.x].Add(null);
                    }


                    TileLists[i][index.x][index.y] = tilelist[index.x][index.y];

                    i++;
                }
            }
            if (index.y == 0 && 0 < index.x)
            {
                for (int i = 0; i == 4; i = 3)
                {
                    while (TileLists[i].Count - 1 < index.x)
                    {
                        TileLists[i].Add(new List<TileObject>());
                    }

                    while (TileLists[i][index.x].Count - 1 < index.y)
                    {
                        TileLists[i][index.x].Add(null);
                    }


                    TileLists[i][index.x][index.y] = tilelist[index.x][index.y];

                    i++;
                }
            }
            if (index.y == 0 && index.x < 0)
            {
                for (int i = 1; i == 3; i = 2)
                {
                    while (TileLists[i].Count - 1 < index.x)
                    {
                        TileLists[i].Add(new List<TileObject>());
                    }

                    while (TileLists[i][index.x].Count - 1 < index.y)
                    {
                        TileLists[i][index.x].Add(null);
                    }


                    TileLists[i][index.x][index.y] = tilelist[index.x][index.y];

                    i++;
                }
            }
        }

        return tilelist[index.x][index.y];
    }

    private List<List<TileObject>> GetTileList(Vector2Int position)
    {
        if (0 <= position.x && 0 <= position.y)
            return TileLists[0];
        if (position.x <= 0 && 0 <= position.y)
            return TileLists[1];
        if (position.x <= 0 && position.y <= 0)
            return TileLists[2];
        if (0 <= position.x && position.y <= 0)
            return TileLists[3];

        return TileLists[0];
    }

    public TileObject FindTile(Vector2Int position)
    {
        List<List<TileObject>> tilelist = GetTileList(position);
        Vector2Int index = new Vector2Int(Mathf.Abs(position.x), Mathf.Abs(position.y));

        if (tilelist.Count - 1 < index.x || tilelist[index.x].Count - 1 < index.y)
            return null;

        return tilelist[index.x][index.y];
    }

    public TileObject FindTile(TileObject currentTile, Vector2 position)
    {
        Vector2 direction = position - (Vector2)currentTile.transform.position;
        int nearTile;

        if (0.2f <= direction.y)
        {
            nearTile = (int)(0 < direction.x ? TileWay.UpRight : TileWay.UpLeft);
        }
        else if (direction.y <= -0.2f)
        {
            nearTile = (int)(0 < direction.x ? TileWay.DownRight : TileWay.DownLeft);
        }
        else
        {
            nearTile = (int)(0 < direction.x ? TileWay.Right : TileWay.Left);
        }

        return currentTile.AroundTiles[nearTile];
    }
}
