using UnityEngine;
using System.Collections;

public class Camera_mvt : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown("w"))
        {
            transform.Translate(Vector3.up);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.Translate(Vector3.down);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.Translate(Vector3.right);
        }
	}
}
