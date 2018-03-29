using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonGameObject<TileManager>
{
    [SerializeField]
    private GameObject TileMapObject;

    [SerializeField]
    private TileObject TilePrefab;

    private Vector2Int TileMapSize;

    private List<TileObject> TilesList;

    public int num;

    public int TryCreateTileNum;

    private void Awake()
    {
        TileManager t = TileManager.Instance;
    }

    protected override void Init()
    {
        TilesList = new List<TileObject>();

        TilesList.Add(Instantiate(TilePrefab, TileMapObject.transform));
        TilesList[0].TilePosition = Vector2Int.zero;
        TilesList[0].transform.position = Vector3.zero;
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
                        Debug.Log("Mpos" + tmp + " (" + (TilesList.Count - 1) + ") ");

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

        if (FindTile(position) != null)
            return FindTile(position);

        TilesList.Add(Instantiate(TilePrefab, TileMapObject.transform));
        TilesList[TilesList.Count - 1].TilePosition = position;
        TilesList[TilesList.Count - 1].transform.position = new Vector2(1f * position.x - (0.5f * position.y), 0.87f * position.y);

        TilesList[TilesList.Count - 1].ConnectAroundTiles(2);

        return TilesList[TilesList.Count - 1];
    }

    //public void Refresh()
    //{
    //    for (int i = 0; i < TilesList.Count; i++)
    //    {
    //        if (!TilesList[i].AroundTilesIsFull)
    //            TilesList[i].ConnectAroundTiles();
    //    }
    //}

    public TileObject FindTile(Vector2Int position)
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (TilesList[i].TilePosition == position)
                return TilesList[i];
        }

        return null;
    }

    public TileObject FindTile(Vector2 position)
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (Vector2.Distance(TilesList[i].transform.position, position) < 0.5f)
            {
                Debug.DrawLine(position, TilesList[i].transform.position, Color.green);

                return TilesList[i];
            }
        }

        return null;
    }

    public TileObject FindTile(TileObject currentTile, Vector2 position)
    {
        for (int i = 0; i < currentTile.AroundTiles.Length; i++)
        {
            if (currentTile.AroundTiles[i] != null && Vector2.Distance(currentTile.AroundTiles[i].transform.position, position) < 0.5f)
            {
                return currentTile.AroundTiles[i];
            }
        }

        return null;
    }
}
