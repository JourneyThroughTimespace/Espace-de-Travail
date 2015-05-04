using UnityEngine;
using System.Collections;

public class Menu_camera : MonoBehaviour 
{
    public static Menu_camera instance;

	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void Cam_mvt_up()
    {
        transform.Translate(Vector3.up);
    }
    public void Cam_mvt_left()
    {
        transform.Translate(Vector3.left);
    }
    public void Cam_mvt_down()
    {
        transform.Translate(Vector3.down);
    }
    public void Cam_mvt_right()
    {
        transform.Translate(Vector3.right);
    }
}
