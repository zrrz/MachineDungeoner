using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject loadingBar;

    public static UIManager Instance { get { return instance;} }
    static UIManager instance;

    void Awake() {
        if (instance != null)
        {
            DestroyImmediate(this);
            Debug.Log("UIManager already in scene");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void ChangeLoadingBar(string text, float percentage) {
        Vector3 barScale = loadingBar.transform.GetChild(1).localScale;
        barScale.x = percentage;
        loadingBar.transform.GetChild(1).localScale = barScale;
        loadingBar.transform.GetChild(2).GetComponent<Text>().text = text;
    }

    public void ShowLoadingBar(bool show) {
        loadingBar.SetActive(show);
    }
}
