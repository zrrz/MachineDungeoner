using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class WorldManager : MonoBehaviour {

    static WorldManager instance;
    public static WorldManager Instance { get { return instance; } }

    public static int worldHeight = -1, worldWidth = -1;

    public ushort[,] frontWorldData;
    public ushort[,] midWorldData;
    public ushort[,] backWorldData;

//    [HideInInspector]
    public List<Anchor> anchors;

    const int ANCHOR_EFFECT_WIDTH = 15, ANCHOR_EFFECT_HEIGHT = 10;

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
        StartCoroutine(GenerateWorld(8400, 2400));
    }

    //TODO call this like 10x/frame somewhere
	void SlowUpdate () {
        foreach (Anchor anchor in anchors)
        {
            Vector2 anchorPos = new Vector2(0, 0); //TODO convert world to tile coords
            for(int x = (int)anchorPos.x - ANCHOR_EFFECT_WIDTH; x < (int)anchorPos.x + ANCHOR_EFFECT_WIDTH; x++) { //TODO not calc max each iteration
                for(int y = (int)anchorPos.y - ANCHOR_EFFECT_HEIGHT; y < (int)anchorPos.y + ANCHOR_EFFECT_HEIGHT; y++) { //Might be upside down?
                    //TODO update WorldData[x,y]
                }
            }
        }

        //TODO spawn monsters for anchor

        
	}

    class ThreadAllocData : object {
        public ThreadAllocData(int _x, int _width, int _y, int _height) {_x = _x; width = _width; y = _y; height = _height;}
        public int x, y, width, height;
    }

    public static float worldBuildingProgress = 0f;

    IEnumerator GenerateWorld(int height, int width) {
        worldHeight = height; worldWidth = width;

        Debug.Log("Allocating memory");
        UIManager.Instance.ShowLoadingBar(true);
        UIManager.Instance.ChangeLoadingBar("Allocating world data", 0f);
        frontWorldData = new ushort[worldWidth, worldHeight];
        midWorldData = new ushort[worldWidth, worldHeight];
        backWorldData = new ushort[worldWidth, worldHeight];

        yield return null;

        Debug.Log("Building world");
        UIManager.Instance.ChangeLoadingBar("Allocating world data", 0.2f);

//        int rows = 10;
//        Thread[] worldBuildThreads = new Thread[rows];
//        for (int i = 0; i < rows; i++)
//        {
//            worldBuildThreads[i] = new Thread(WorldManager.AllocateWorld);
//            worldBuildThreads[i].Start(new ThreadAllocData(0, worldWidth, worldHeight/rows * i, worldHeight/rows));
//        }

        WorldManager.AllocateWorld(new ThreadAllocData(0, worldWidth, 0, worldHeight));

//        Thread thread = new Thread(WorldManager.AllocateWorld);
//        thread.Start(new ThreadAllocData(0, worldWidth, 0, worldHeight/10));
//        thread.Start((0, worldWidth, 0, 200));
//        for (int i = 0; i < worldWidth; i++)
//        {
//            for (int j = 0; j < worldHeight; j++)
//            {
//                frontWorldData[i, j] = 1;
//                midWorldData[i, j] = 1;
//                backWorldData[i, j] = 1;
//            }
//            if(i > 0) //Dumb division by 0
//                UIManager.Instance.ChangeLoadingBar("Allocating world data", 0.2f + ((float)i/worldWidth)/2f);
//            yield return null;
//        }

//        while (worldBuildingProgress < 1f)
//        {
//            UIManager.Instance.ChangeLoadingBar("Allocating world data", 0.2f + (worldBuildingProgress/2f));
//            yield return null;
//        }

        UIManager.Instance.ChangeLoadingBar("Done building world", 1f);
        Debug.Log("Done building world. Took " + Time.realtimeSinceStartup);
        yield return new WaitForSeconds(0.5f); 
        UIManager.Instance.ShowLoadingBar(false);
    }

    public static void AllocateWorld(object _allocData) {
        ThreadAllocData allocData = (ThreadAllocData)_allocData;
//        Debug.Log("Thread running at x:" + allocData.x + " width:" + allocData.width + " - y:" + allocData.y + " height:" + allocData.width);
        for (int i = allocData.x, n = allocData.x + allocData.width; i < n; i++)
        {
            for (int j = allocData.y, n2 = allocData.y + allocData.height; j < n2; j++)
            {
                WorldManager.Instance.frontWorldData[i, j] = 1;
                WorldManager.Instance.midWorldData[i, j] = 1;
                WorldManager.Instance.backWorldData[i, j] = (ushort)Random.Range(1, 3);
            }
        }
        worldBuildingProgress += 0.1f;
    }

    public Vector2 GetTileIndices(Vector3 pos) {
//        throw System.NotImplementedException();
        return anchors[0].transform.position;
    }
}
