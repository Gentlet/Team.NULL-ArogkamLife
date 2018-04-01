using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileWay { Right, UpRight, DownRight, Left, UpLeft, DownLeft };

public class TileObject : MonoBehaviour
{
    [SerializeField]
    private Vector2Int tilePosition;
    [SerializeField]
    private TileObject[] aroundTiles;


    private void Awake()
    {
        aroundTiles = new TileObject[6];

        transform.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    public void CreateAroundTile(Vector2Int startpos, int num)
    {
        if (num == 0)
            return;

        Vector2Int[] pos =
        {
            (tilePosition + Vector2Int.right),
            (tilePosition + Vector2Int.one),
            (tilePosition + Vector2Int.down),
            (tilePosition + Vector2Int.left),
            (tilePosition + Vector2Int.up),
            (tilePosition - Vector2Int.one),
        };

        for (int i = 0; i < AroundTiles.Length; i++)
        {
            if (Vector2Int.Distance(startpos, pos[i]) >= (TileManager.Instance.num - num - 1))
            {
                TileObject tile = TileManager.Instance.CreateTile(pos[i]);


                aroundTiles[i].CreateAroundTile(startpos, (num - 1));
            }
        }
    }

    public void ConnectAroundTiles(int num)
    {
        if (num == 0)
            return;

        for (int i = 0; i < aroundTiles.Length; i++)
        {
            ConnectTile(i, num);
        }
    }

    private void ConnectTile(int index, int num)
    {
        if (aroundTiles[index] != null)
            return;

        Vector2Int[] pos =
        {
            (tilePosition + Vector2Int.right),
            (tilePosition + Vector2Int.one),
            (tilePosition + Vector2Int.down),
            (tilePosition + Vector2Int.left),
            (tilePosition + Vector2Int.up),
            (tilePosition - Vector2Int.one),
        };

        aroundTiles[index] = TileManager.Instance.FindTile(pos[index]);

        if (aroundTiles[index] != null)
            aroundTiles[index].ConnectAroundTiles(num - 1);
    }

    #region Properties
    public Vector2Int TilePosition
    {
        get
        {
            return tilePosition;
        }
        set
        {
            tilePosition = value;
        }
    }

    public TileObject[] AroundTiles
    {
        get
        {
            return aroundTiles;
        }
    }

    public bool AroundTilesIsFull
    {
        get
        {
            for (int i = 0; i < aroundTiles.Length; i++)
            {
                if (aroundTiles[i] == null)
                    return false;
            }

            return true;
        }
    }
    #endregion
}
