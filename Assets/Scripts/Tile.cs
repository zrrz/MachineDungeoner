using UnityEngine;
using System.Collections;

public class Tile {

	public Texture2D texture;

	public Vector2 position;
	public Vector2 size;

	public Material mat;

	void Start () {
		
	}
	
	void Update () {
	
	}

	public void Draw() {
		Vector2 pixelPos = Camera.main.WorldToScreenPoint(position);
//		Debug.Log(position + "=" + (Vector2)screenPos);
		Vector2 screenPos = new Vector2(Screen.width/pixelPos.x, Screen.height/pixelPos.y);
		Rect screenRect = new Rect(screenPos, size);
//		Debug.Log(screenRect);
//		Graphics.DrawTexture(screenRect, texture);
//		Graphics.DrawTexture(position, texture);
	}
}
