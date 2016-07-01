using UnityEngine;
using System.Collections;

public class FakeTileInfo : MonoBehaviour {

    public Sprite dirtTexture, rockTexture;

	void Start () {
        TileMap.Instance.AddData(1, new TileInfo(1, "Dirt", dirtTexture, 0, new ushort[]{ 0 }, 1, 1, CollisionType.FullCollision, LayerType.Back));
        TileMap.Instance.AddData(2, new TileInfo(2, "Rock", rockTexture, 0, new ushort[]{ 0 }, 1, 1, CollisionType.FullCollision, LayerType.Back));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
