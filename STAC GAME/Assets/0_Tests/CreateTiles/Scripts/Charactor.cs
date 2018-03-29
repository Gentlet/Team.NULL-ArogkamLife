using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public TileObject CurrentTile;

    public int speed;

    // Use this for initialization
    void Start()
    {
        CurrentTile = TileManager.Instance.FindTile(transform.position);

        CreateMap(Vector2Int.zero);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(transform.position, CurrentTile.transform.position, Color.green);
        DebugManager.Instance.listInit("ChPos", "Pos X : " + CurrentTile.TilePosition.x + "   Pos Y : " + CurrentTile.TilePosition.y);

        Vector2 tmp = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
            tmp.x += 1;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            tmp.x -= 1;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            tmp.y += 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            tmp.y -= 1;

        if (Input.GetKey(KeyCode.LeftShift))
            tmp *= speed;

        transform.position += (Vector3)tmp * 2f * Time.deltaTime;

        posset();
    }

    void posset()
    {
        if (Vector2.Distance(transform.position, CurrentTile.transform.position) < 0.5f)
            return;

        TileObject tmp = TileManager.Instance.FindTile(CurrentTile, transform.position);

        if (tmp != null)
        {
            TileObject tile = tmp;

            if (CurrentTile != tile)
            {
                CurrentTile = tile;
                CreateMap(tile.TilePosition);
            }
        }
    }

    void CreateMap(Vector2Int position)
    {
        CurrentTile.CreateAroundTile(position, TileManager.Instance.num);
    }

}
