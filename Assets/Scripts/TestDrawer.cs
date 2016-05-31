using UnityEngine;
using System.Collections;

public class TestDrawer : MonoBehaviour {

	public Texture2D texture2D;
//	public Material mat;
	Tile[] tiles;

//	public GameObject tilePrefab;

	public int rows = 1000, columns = 100;

	void Start () {
		tiles = new Tile[rows*columns];
		for(int i = 0; i < rows; i++) {
			for(int j = 0; j < columns; j++) {
//				Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity);
				int index = columns * i + j;
				tiles[index] = new Tile();
				Vector2 position = new Vector2(i, j);
				tiles[index].position = position;
				tiles[index].texture = texture2D;
				tiles[index].size = new Vector2(10f, 10f);
	//			tiles[index].mat = mat;
			}
		}
	}
	
	void Update () {
		
	}

	void OnPostRender() {

		for(int i = 0; i < tiles.Length; i++) {
//			tiles[i].Draw();
			Debug.Log(new Vector2(i % columns, i/columns));
			Draw(new Vector2(i % columns, i/columns));
		}
//		Graphics.DrawTexture(new Rect(9f,1f,1f,1f), texture2D);
//		Graphics.DrawTexture(new Rect(0.1f,1f,1f,1f), texture2D);
	}

	Vector2 size = new Vector2(10f, 10f);
	public void Draw(Vector2 position) {
		Vector2 pixelPos = Camera.main.WorldToScreenPoint(position);
		//		Debug.Log(position + "=" + (Vector2)screenPos);
		Vector2 screenPos = new Vector2(Screen.width/pixelPos.x, Screen.height/pixelPos.y);
		Rect screenRect = new Rect(screenPos, size);
		//		Debug.Log(screenRect);
		//		Graphics.DrawTexture(screenRect, texture);
		//		Graphics.DrawTexture(position, texture);
	}
}
