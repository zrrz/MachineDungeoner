using UnityEngine;
using System.Collections;

//TODO rename this shit
public class WorldDrawer : MonoBehaviour {

    int rows = 10, columns = 12;

    SpriteRenderer[,] backTiles;
    SpriteRenderer[,] midTiles;
    SpriteRenderer[,] frontTiles;

    bool dirty = false;

    public Transform player;

	void Start () {
        GameObject backHolder = new GameObject("BackSprites");
        backHolder.transform.parent = transform;
        GameObject midHolder = new GameObject("MidSprites");
        midHolder.transform.parent = transform;
        GameObject frontHolder = new GameObject("FrontSprites");
        frontHolder.transform.parent = transform;

        backTiles = new SpriteRenderer[columns, rows];
        midTiles = new SpriteRenderer[columns, rows];
        frontTiles = new SpriteRenderer[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject backObj = new GameObject(i + " " + j, typeof(SpriteRenderer));
                backTiles[i, j] = backObj.GetComponent<SpriteRenderer>();
                backObj.transform.parent = backHolder.transform;

                GameObject midObj = new GameObject(i + " " + j, typeof(SpriteRenderer));
                midTiles[i, j] = midObj.GetComponent<SpriteRenderer>();
                midObj.transform.parent = midHolder.transform;

                GameObject frontObj = new GameObject(i + " " + j, typeof(SpriteRenderer));
                frontTiles[i, j] = frontObj.GetComponent<SpriteRenderer>();
                frontObj.transform.parent = frontHolder.transform;
            }
        }
	}
	
	void Update () {
        Vector2 tileIndices = WorldManager.Instance.GetTileIndices(player.position);
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int x = (int)tileIndices.x - (rows / 2) + i;
                int y = (int)tileIndices.y - (columns / 2) + j;
                if (x >= 0 && x < WorldManager.worldWidth && y >= 0 && y < WorldManager.worldHeight)
                {
                    UpdateTile(i, j, x, y);
                }
            }
        }
	}

    void UpdateTile(int i, int j, int x, int y) {
        ushort tileID = WorldManager.Instance.backWorldData[x, y];
        TileInfo info = TileMap.Instance.GetData(tileID);
        if (info == null)
        {
            return;
        }
//        Texture2D tex = new Texture2D(2, 2);
//        tex.LoadImage(info.texture);
//        Debug.Log(x + " " + y);
//        backTiles[i, j].sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.one / 2f, 16f);
        backTiles[i,j].sprite = info.texture;
        backTiles[i, j].transform.position = new Vector2(x, y);
    }

    void Swap() {

    }
}
