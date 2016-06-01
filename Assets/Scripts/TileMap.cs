using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Helper class from storing all data about tiles
/// </summary>
public class TileInfo {
    /// <summary>
    /// Initializes a new instance of the <see cref="TileInfo"/> class.
    /// </summary>
    /// <param name="tileID">ID of tile.</param>
    /// <param name="isPlaceable">If set to <c>true</c> is placeable.</param>
    /// <param name="texture">Texture of tile</param>
    /// <param name="name">Tile Name.</param>
    /// <param name="width">Width in tiles.</param>
    /// <param name="height">Height in tiles.</param>
    public TileInfo(ushort tileID, bool isPlaceable, Texture2D texture, string name, byte width, byte height, byte collisionType, byte allowedLayers) {
        this.tileID = tileID;
        this.isPlaceable = isPlaceable;
        this.texture = texture;
        this.name = name;
        this.width = width;
        this.height = height;
        this.collisionType = collisionType;
        this.allowedLayers = allowedLayers;
    }
    public ushort tileID; //Redundant?
    public bool isPlaceable;
    public Texture2D texture; //This SHOULD be a pointer to the loaded texture and not replicate
    public string name; //Maybe unnecesary?
    byte width, height;
    byte collisionType;
    byte allowedLayers; // bit packed bools for what layers it can be placed on
}

/// <summary>
/// Defines collision types by 
/// </summary>
public enum CollisionType {
    NoCollision = 0,
    FullCollision = 1,
    NoBottomCollision = 2,
    NoTopCollision = 3,
    NoVerticalCollision = 4,
    OnlyBottomCollision = 5,
    OnlyTopCollision = 6,
    NoLeftCollision = 7,
    NoRightCollision = 8,
    NoHorizontalCollision = 9,
    OnlyLeftCollision = 10,
    OnlyRightCollision = 11
}

/// <summary>
/// Tile map singleton for setting and getting all tiles types by ID
/// </summary>
public class TileMap : MonoBehaviour {
    
    static TileMap s_instance;

    public TileMap Instance {
        get {
            if (s_instance != null)
            {
                return s_instance;
            }
            else
            {
                Debug.LogError("TileMap instance not set");
                return null;
            }
        }
    }

	Dictionary<ushort, TileInfo> tileMap;

	void Awake () {
        if (s_instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            s_instance = this;
        }
		tileMap = new Dictionary<ushort, TileInfo>();
	}

    public void AddTile(ushort id, bool isPlaceable, Texture2D texture, string name, byte width, byte height, byte collisionType, byte allowedLayers) {
        AddTile(id, new TileInfo(id, isPlaceable, texture, name, width, height, collisionType, allowedLayers));
    }
	
    public void AddTile(ushort id, TileInfo tileInfo) {
        if (!tileMap.ContainsKey(id))
        {
            tileMap.Add(id, tileInfo);
        }
        else
        {
            Debug.LogError("tile ID already registered to :" + tileMap[id].name);
        }
    }

	public TileInfo GetTile(ushort id) {
		TileInfo tileInfo;
		if(tileMap.TryGetValue(id, out tileInfo))
			return tileInfo;
		else
			Debug.LogError("Trying to get a tileID that doesnt exist");
        return null;
	}
}
