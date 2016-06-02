using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Helper class from storing all data about a tile
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
    public TileInfo(
        ushort tileID, 
        string name, 
        Texture2D texture, 
        byte numDroppedItems, 
        ushort[] itemIDs, 
        byte width, 
        byte height, 
        CollisionType collisionType, 
        LayerType layer
    ) {
        this.tileID = tileID;
        this.name = name;
        this.texture = texture;
        this.numDroppedItems = numDroppedItems;
        this.itemIDs = itemIDs;
        this.width = width;
        this.height = height;
        this.collisionType = collisionType;
        this.layer = layer;
    }
    public ushort tileID; //Redundant?
    public string name;
    public Texture2D texture; //These SHOULD be a pointer to the loaded texture and not replicate

    public byte numDroppedItems; //How many items are set to drop
    public ushort[] itemIDs; //ID of item to drop when broken. 0 is Nothing

    public byte width, height;

    //Only for mid layer
//    public byte collisionType;
    public CollisionType collisionType;
//    public byte layer;
    public LayerType layer;

    //TODO make sure this is correct
//    public LayerType GetLayer() {
//        if (layer & 0x1)
//        {
//            return LayerType.Back;
//        }
//        else if (layer & 0x2)
//        {
//            return LayerType.Middle;
//        }
//        else if (layer & 0x4)
//        {
//            return LayerType.Front;
//        }
//    }

    //TODO make sure this works
//    public CollisionType GetCollisionType() {
//        return (CollisionType)collisionType & 0xB;
//    }
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
