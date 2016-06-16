using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    static WorldManager instance;
    public static WorldManager Instance { get { return instance; } }

    static int worldHeight = -1, worldWidth = -1;

    ushort[,] worldData;

	void Awake () {
        if (instance != null)
        {
            Destroy(this);
            Debug.LogError("WorldManager already defined");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
	}
	
    void Start() {
        GenerateWorld(8400, 2400);
    }

	void Update () {
	
	}

    void GenerateWorld(int height, int width) {
        worldHeight = height; worldWidth = width;

        worldData = new ushort[worldWidth, worldHeight];

        for (int i = 0; i < worldWidth; i++)
        {
            for (int j = 0; j < worldHeight; worldHeight++)
            {

            }
        }
    }
}
