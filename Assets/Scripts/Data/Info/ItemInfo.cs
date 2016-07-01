using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Helper class from storing all data about an item
/// </summary>
[System.Serializable]
public class ItemInfo : BaseInfo {
	/// <summary>
	/// Initializes a new instance of the <see cref="TileInfo"/> class.
	/// </summary>
	/// <param name="tileID">ID of tile.</param>
	/// <param name="isPlaceable">If set to <c>true</c> is placeable.</param>
	/// <param name="texture">Texture of tile</param>
	/// <param name="name">Tile Name.</param>
	/// <param name="width">Width in tiles.</param>
	/// <param name="height">Height in tiles.</param>
	public ItemInfo(
		ushort itemID, 
		string name, 
        Sprite texture,
		byte rarity,
		byte allowedLayers,
		ushort backTileID,
		ushort midTileID,
		ushort frontTileID
	) {
		this.itemID = itemID;
		this.name = name;
		this.texture = texture;
		this.rarity = rarity;
		this.allowedLayers = allowedLayers;
		this.backTileID = backTileID;
		this.midTileID = midTileID;
		this.frontTileID = frontTileID;
	}
	public ushort itemID; //Redundant?
	public byte rarity;

	public byte width, height;

	public byte allowedLayers; // bit packed bools for what layers it can be placed on
	public ushort backTileID;
	public ushort midTileID;
	public ushort frontTileID;

	//TODO make sure this is correct
	public bool GetAllowedLayer(LayerType layerType) {
		if (layerType == LayerType.Back)
		{
			return (allowedLayers & 0x1) > 0;
		}
		else if (layerType == LayerType.Middle)
		{
			return (allowedLayers & 0x2) > 0;
		}
		else if (layerType == LayerType.Front)
		{
			return (allowedLayers & 0x4) > 0;
		}
		return false;
	}
}

