using System;
//using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BaseInfo {

	public string name;
    public Sprite texture; //SHOULD be a pointer to the loaded texture and not replicate

//	// Unity only
//	public Texture2D GetTexture() {
//		Texture2D tex = new Texture2D(2, 2); //Size doesn't matter
//		tex.LoadImage(texture);
//		return tex;
//	}

	/// <summary>
	/// Deserialize the data byte array into a TileInfo.
	/// </summary>
	/// <param name="data">Data.</param>
	public T Deserialize<T>(byte[] data) {
		T info;

		using(MemoryStream stream = new MemoryStream(data)) {
			BinaryFormatter formatter = new BinaryFormatter();
			info = (T)formatter.Deserialize(stream);
			if(null == info) {
				//				Debug.LogError("Can't deserialize into ItemInfo"); //You want to use the generic C# version of console logging instead of the Unity way
			}
			return info; //Might be null
		}
	}

	public byte[] Serialize<T>(T info) {
		using(MemoryStream stream = new MemoryStream()) {
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, info);
			return stream.ToArray();
		}
	}
}

