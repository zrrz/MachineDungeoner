using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileInfo {
	bool isPlaceable;
	Texture2D texture;
	ushort tileID; //Redundant?

}

public class TileMap : MonoBehaviour {

	Dictionary<ushort, TileInfo> tileMap;

	void Start () {
		tileMap = new Dictionary<ushort, TileInfo>();
	}
	
	void Update () {
	
	}

	public TileInfo GetTile(ushort id) {
		TileInfo tileInfo;
		if(tileMap.TryGetValue(id, tileInfo))
			return tileInfo;
		else
			Debug.LogError("Trying to get a tileID that doesnt exist");
	}
}
