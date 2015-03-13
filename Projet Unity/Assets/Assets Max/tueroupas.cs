using UnityEngine;
using System.Collections;

public class tueroupas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        detruitobjet();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void detruitobjet ()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
