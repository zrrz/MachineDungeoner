using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataMap<T> : MonoBehaviour {

    Dictionary<ushort, T> dataMap;

    public virtual void Awake () {
        dataMap = new Dictionary<ushort, T>();
    }

//    public void AddTile(ushort id, bool isPlaceable, Texture2D texture, string name, byte width, byte height, byte collisionType, byte allowedLayers) {
//        AddTile(id, new TileInfo(id, isPlaceable, texture, name, width, height, collisionType, allowedLayers));
//    }

    public void AddData(ushort id, T data) {
        if (!dataMap.ContainsKey(id))
        {
            dataMap.Add(id, data);
        }
        else
        {
            Debug.LogError("tile ID already registered to :" + dataMap[id].ToString()); //TODO probably log this nicer
        }
    }

    public T GetData(ushort id) {
        T tileInfo;
        if(dataMap.TryGetValue(id, out tileInfo))
            return tileInfo;
        else
            Debug.LogError("Trying to get a ID from " + this.ToString() + " that doesnt exist");
        return default(T);
    }
}
