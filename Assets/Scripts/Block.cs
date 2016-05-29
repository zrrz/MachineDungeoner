using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public int x, y;
    public BlockType.Layers layer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        if (layer == BlockType.Layers.Background)
        {
            WorldBuilder.Instance.BreakBackgroundBlock(x, y);
        }
        else
        {
            WorldBuilder.Instance.BreakMiddleBlock(x, y);
        }
    }
}
