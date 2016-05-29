using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlockType
{
    public enum Layers {
        Background, Middle
    }

    public enum BackgroundTypes {
        Air, Water, Dirt, Rock
    }
    public enum MiddleTypes {
        None, Water, Dirt, Rock
    }
}

public class BackgroundBlock {
    public BackgroundBlock(BlockType.BackgroundTypes _blockType, SpriteRenderer _blockObject) {
        blockType = _blockType;
        blockObject = _blockObject;
    }
    public BlockType.BackgroundTypes blockType;
    public SpriteRenderer blockObject;
}

public class MiddleBlock {
    public MiddleBlock(BlockType.MiddleTypes _blockType, SpriteRenderer _blockObject) {
        blockType = _blockType;
        blockObject = _blockObject;
    }
    public BlockType.MiddleTypes blockType;
    public SpriteRenderer blockObject;
}

public class WorldBuilder : MonoBehaviour {

    public static WorldBuilder Instance;

    public int worldWidth, worldHeight;
    public BackgroundBlock[,] backgroundLayer;
    public MiddleBlock[,] middleLayer;

    public GameObject blockPrefab;

    Dictionary<BlockType.BackgroundTypes, Color> backgroundBlockColors;
    Dictionary<BlockType.MiddleTypes, Color> middleBlockColors;

    void Awake() {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
        backgroundBlockColors = new Dictionary<BlockType.BackgroundTypes, Color>();
        backgroundBlockColors.Add(BlockType.BackgroundTypes.Air, new Color(0.6f, 0.8f, 0.95f));
        backgroundBlockColors.Add(BlockType.BackgroundTypes.Water, Color.blue);
        backgroundBlockColors.Add(BlockType.BackgroundTypes.Dirt, new Color(139f/255f,69f/255f,19f/255f));
        backgroundBlockColors.Add(BlockType.BackgroundTypes.Rock, new Color(0.7f, 0.7f, 0.7f));

        middleBlockColors = new Dictionary<BlockType.MiddleTypes, Color>();
        middleBlockColors.Add(BlockType.MiddleTypes.None, new Color(0f, 0f, 0f, 0f));
        middleBlockColors.Add(BlockType.MiddleTypes.Water, Color.blue);
        middleBlockColors.Add(BlockType.MiddleTypes.Dirt, new Color(120f/255f,49f/255f,10f/255f));
        middleBlockColors.Add(BlockType.MiddleTypes.Rock, new Color(0.6f, 0.6f, 0.6f));

        BuildWorld();
	}
	
//	void Update () {
//	
//	}

    void BuildWorld() {
        //Background
        backgroundLayer = new BackgroundBlock[worldWidth,worldHeight];
        for (int i = 0; i < worldWidth; i++)
        {
            for (int j = 0; j < worldHeight; j++)
            {
                SpriteRenderer blockObject = ((GameObject)Instantiate(blockPrefab, new Vector2(-worldWidth/2 + i, -worldHeight/2 + (worldHeight - j)), Quaternion.identity)).GetComponent<SpriteRenderer>();
                if (j < worldHeight / 2)
                {
                    backgroundLayer[i, j] = new BackgroundBlock(BlockType.BackgroundTypes.Air, blockObject);
                    backgroundLayer[i,j].blockObject.color = backgroundBlockColors[BlockType.BackgroundTypes.Air];
                }
                else if (j < (worldHeight * 3f / 4f))
                {
                    backgroundLayer[i, j] = new BackgroundBlock(BlockType.BackgroundTypes.Dirt, blockObject);
                    backgroundLayer[i,j].blockObject.color = backgroundBlockColors[BlockType.BackgroundTypes.Dirt];
                }
                else
                {
                    backgroundLayer[i, j] = new BackgroundBlock(BlockType.BackgroundTypes.Rock, blockObject);
                    backgroundLayer[i,j].blockObject.color = backgroundBlockColors[BlockType.BackgroundTypes.Rock];
                }
                backgroundLayer[i,j].blockObject.transform.parent = transform.GetChild(0);
                backgroundLayer[i,j].blockObject.sortingOrder = -2;
                backgroundLayer[i,j].blockObject.GetComponent<Block>().x = i;
                backgroundLayer[i,j].blockObject.GetComponent<Block>().y = j;
                backgroundLayer[i, j].blockObject.GetComponent<Block>().layer = BlockType.Layers.Background;
                backgroundLayer[i, j].blockObject.gameObject.layer = LayerMask.NameToLayer("Background");
            }
        }
        //Middle layer
        middleLayer = new MiddleBlock[worldWidth,worldHeight];
        for (int i = 0; i < worldWidth; i++)
        {
            for (int j = 0; j < worldHeight; j++)
            {
//                Debug.Log("i j: " + i + " " + j);
                SpriteRenderer blockObject = ((GameObject)Instantiate(blockPrefab, new Vector2(-worldWidth/2 + i, -worldHeight/2 + (worldHeight - j)), Quaternion.identity)).GetComponent<SpriteRenderer>();
                if(j < worldHeight / 2)
                {
                    middleLayer[i,j] = new MiddleBlock(BlockType.MiddleTypes.None, blockObject);
                    middleLayer[i,j].blockObject.color = middleBlockColors[BlockType.MiddleTypes.None];
                }
                else if (j < (worldHeight * 3f / 4f))
                {
                    middleLayer[i, j] = new MiddleBlock(BlockType.MiddleTypes.Dirt, blockObject);
                    middleLayer[i,j].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Dirt];
                }
                else
                {
                    middleLayer[i,j] = new MiddleBlock(BlockType.MiddleTypes.Rock, blockObject);
                    middleLayer[i,j].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Rock];
                }

                middleLayer[i,j].blockObject.transform.parent = transform.GetChild(1);
                middleLayer[i,j].blockObject.sortingOrder = -1;
                middleLayer[i,j].blockObject.GetComponent<Block>().x = i;
                middleLayer[i,j].blockObject.GetComponent<Block>().y = j;
                middleLayer[i, j].blockObject.GetComponent<Block>().layer = BlockType.Layers.Middle;

                middleLayer[i, j].blockObject.GetComponent<Collider2D>().enabled = middleLayer[i, j].blockType != BlockType.MiddleTypes.None;
            }
        }
        for (int i = 0; i < worldWidth; i++)
        {
            for (int j = 0; j < worldHeight; j++)
            {
                ResizeMiddleLayerBlock(i,j);
//                CalculateLighting(i, j);
            }
        }
        UpdateLighting();
    }

    //TODO put this somewhere nicer
    const int lightAffectRadius = 2;

    void UpdateLighting() {
//        return;
        for (int i = 0; i < worldWidth; i++)
        {
            for (int j = 0; j < worldHeight; j++)
            {
                CalculateLighting(i, j);
            }
        }
    }

    /// <summary>
    /// Calculates the light for block at x,y using lightAffectRadius
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    void CalculateLighting(int x, int y) {
        int left = Mathf.Min(lightAffectRadius, x);  
        int width = Mathf.Min(x + lightAffectRadius, worldWidth) - x;
        int top = Mathf.Min(lightAffectRadius, y); 
        int height = Mathf.Min(y + lightAffectRadius, worldHeight) - y;
//        Debug.Log("x: " + x + " l: " + left + " w: " + width + " t: " + top + " h: " + height);

//        Debug.Log("i is: " + (x - left) + " x is: " + x + " until " + (x + width));
//        return;

        bool unlit = true;
        for (int i = x - left; i <= x + width && i < worldWidth; i++)
        {
            for(int j = y - top; j <= y + height && j < worldHeight; j++) {
                if (middleLayer[i, j].blockType == BlockType.MiddleTypes.None && backgroundLayer[i, j].blockType == BlockType.BackgroundTypes.Air)
                {
                    unlit = false;
                }
            }
        }

        if (unlit)
        {
            if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Dirt)
            {
                
                middleLayer[x, y].blockObject.color = Color.Lerp(middleBlockColors[BlockType.MiddleTypes.Dirt], new Color(0.2f, 0.2f, 0.2f), 0.4f);
            }
            else if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Rock)
            {
                middleLayer[x, y].blockObject.color = Color.Lerp(middleBlockColors[BlockType.MiddleTypes.Rock], new Color(0.2f, 0.2f, 0.2f), 0.4f);
            }
//            else if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Water)
//            {
//                middleLayer[x, y].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Water] - new Color(0.2f, 0.2f, 0.2f);
//            }
        }
        else
        {
            if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Dirt)
            {
                middleLayer[x, y].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Dirt];
            }
            else if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Rock)
            {
                middleLayer[x, y].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Rock];
            }
            else if (middleLayer[x, y].blockType == BlockType.MiddleTypes.Water)
            {
                middleLayer[x, y].blockObject.color = middleBlockColors[BlockType.MiddleTypes.Water];
            }
        }
    }

    void ResizeMiddleLayerBlock(int x, int y) {
        middleLayer[x, y].blockObject.transform.localPosition = new Vector2(-worldWidth/2 + x, -worldHeight/2 + worldHeight - y);
        middleLayer[x, y].blockObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        // Left
        if (x > 0)
        {
            if (middleLayer[x - 1, y].blockType != BlockType.MiddleTypes.None)
            {
                middleLayer[x, y].blockObject.transform.localScale += new Vector3(0.1f, 0f, 0f);
                middleLayer[x, y].blockObject.transform.Translate(-0.05f, 0f, 0f);
            }
        }
        // Right
        if (x < worldWidth-1)
        {
            if (middleLayer[x + 1, y].blockType != BlockType.MiddleTypes.None)
            {
                middleLayer[x, y].blockObject.transform.localScale += new Vector3(0.1f, 0f, 0f);
                middleLayer[x, y].blockObject.transform.Translate(0.05f, 0f, 0f);
            }
        }
        // Up
        if (y > 0)
        {
            if (middleLayer[x, y - 1].blockType != BlockType.MiddleTypes.None)
            {
                middleLayer[x, y].blockObject.transform.localScale += new Vector3(0f, 0.1f, 0f);
                middleLayer[x, y].blockObject.transform.Translate(0f, 0.05f, 0f);
            }
        }
        // Down
        if (y < worldHeight-1)
        {
            if (middleLayer[x, y + 1].blockType != BlockType.MiddleTypes.None)
            {
                middleLayer[x, y].blockObject.transform.localScale += new Vector3(0f, 0.1f, 0f);
                middleLayer[x, y].blockObject.transform.Translate(0f, -0.05f, 0f);
            }
        }
    }

    public void BreakMiddleBlock(int x, int y) {
        CalculateLighting(x, y);
        if (middleLayer[x, y].blockType != BlockType.MiddleTypes.None)
        {
            middleLayer[x, y].blockObject.color = new Color(0f, 0f, 0f, 0f);
            middleLayer[x, y].blockType = BlockType.MiddleTypes.None;
            middleLayer[x, y].blockObject.GetComponent<Collider2D>().enabled = false;
            if (x > 0)
            {
                ResizeMiddleLayerBlock(x - 1, y);
            }
            if (x < worldWidth - 1)
            {
                ResizeMiddleLayerBlock(x + 1, y);
            }
            if (y > 0)
            {
                ResizeMiddleLayerBlock(x, y - 1);
            }
            if (y < worldHeight - 1)
            {
                ResizeMiddleLayerBlock(x, y + 1);
            }
        }
        UpdateLighting();
    }

    public void BreakBackgroundBlock(int x, int y) {
//        CalculateLighting(x, y);
        if (backgroundLayer[x, y].blockType != BlockType.BackgroundTypes.Air)
        {
            backgroundLayer[x, y].blockObject.color = backgroundBlockColors[BlockType.BackgroundTypes.Air];
            backgroundLayer[x, y].blockType = BlockType.BackgroundTypes.Air;
            backgroundLayer[x, y].blockObject.GetComponent<Collider2D>().enabled = false;
//            if (x > 0)
//            {
//                ResizeMiddleLayerBlock(x - 1, y);
//            }
//            if (x < worldWidth - 1)
//            {
//                ResizeMiddleLayerBlock(x + 1, y);
//            }
//            if (y > 0)
//            {
//                ResizeMiddleLayerBlock(x, y - 1);
//            }
//            if (y < worldHeight - 1)
//            {
//                ResizeMiddleLayerBlock(x, y + 1);
//            }
            UpdateLighting();
        }
    }
}
