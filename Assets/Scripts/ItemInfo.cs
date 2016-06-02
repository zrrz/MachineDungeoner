using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Helper class from storing all data about an item
/// </summary>
[System.Serializable]
public class ItemInfo {
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
		Texture2D texture,
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
	public string name;
	public Texture2D texture; //Can probably use same image for inv and ground. SHOULD be a pointer to the loaded texture and not replicate
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

	//TODO DRY please

	/// <summary>
	/// Deserialize the data byte array into a TileInfo.
	/// </summary>
	/// <param name="data">Data.</param>
	public ItemInfo Deserialize(byte[] data) {
		ItemInfo itemInfo;

		using(MemoryStream stream = new MemoryStream(data)) {
			BinaryFormatter formatter = new BinaryFormatter();
			itemInfo = formatter.Deserialize(stream) as ItemInfo;
			if(null == itemInfo) {
				//				Debug.LogError("Can't deserialize into ItemInfo"); //You want to use the generic C# version of console logging instead of the Unity way
			}
			return itemInfo; //Might be null
		}
	}

	public byte[] Serialize(ItemInfo itemInfo) {
		using(MemoryStream stream = new MemoryStream()) {
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, itemInfo);
			return stream.ToArray();
		}
	}
}

