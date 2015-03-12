using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Awake();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        GameObject gameMusic = GameObject.Find("GameMusic");
        GameObject gamemusic = GameObject.Find("Music");
        if (gameMusic)
        {
            Destroy(gameMusic);
            Destroy(gamemusic);
        }
        DontDestroyOnLoad(gameObject);
    }
}
