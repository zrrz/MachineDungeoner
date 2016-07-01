using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

//TODO MOVE THIS SOMEWHERE CLEANER
public enum LayerType {
	Back = 0, Middle = 1, Front = 2
}

/// <summary>
/// Helper class from storing all data about a tile
/// </summary>
[System.Serializable]
public class TileInfo : BaseInfo {
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
        Sprite texture, 
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
