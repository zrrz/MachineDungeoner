using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilder : MonoBehaviour {

    public static WorldBuilder Instance;

    public int worldWidth, worldHeight;

    public GameObject blockPrefab;

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
        BuildWorld();
	}
	
//	void Update () {
//	
//	}

    void BuildWorld() {
      
    }

    //TODO put this somewhere nicer
    const int lightAffectRadius = 2;

    void UpdateLighting() {

    }

    /// <summary>
    /// Calculates the light for block at x,y using lightAffectRadius
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    void CalculateLighting(int x, int y) {

    }
}
