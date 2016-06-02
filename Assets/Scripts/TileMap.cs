using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Tile map singleton for setting and getting all tiles types by ID
/// </summary>
public class TileMap : DataMap<TileInfo> {
    
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

	public override void Awake () {
        if (s_instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            s_instance = this;
        }
        base.Awake();
	}


    public void AddData(
        ushort tileID, 
        string name, 
        Texture2D texture, 
        byte numDroppedItems, 
        ushort[] itemIDs, 
        byte width, 
        byte height, 
        byte collisionType, 
        byte layer
    ) {
        AddData(tileID, name, texture, numDroppedItems, itemIDs, width, height, collisionType, layer);
    }
}
