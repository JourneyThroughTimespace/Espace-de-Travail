using UnityEngine;
using System.Collections;

public class Temp_Perso_mvt : MonoBehaviour {

    public static Temp_Perso_mvt instance;
    public bool move = false;
	// Use this for initialization
	void Start () 
    {
        instance = this;
	}

	
	// Update is called once per frame
	void Update () 
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown("w") && !Physics.Raycast(transform.position, fwd, 1))
        {
            transform.Translate(Vector3.forward);
            have_move(true);
            Camera_mvt.instance.Cam_mvt_up();
            Light_mvt.instance.light_mvt_up();
        }
        if (Input.GetKeyDown("a") && !Physics.Raycast(transform.position, lef, 1))
        {
            transform.Translate(Vector3.left); 
            have_move(true);
            Camera_mvt.instance.Cam_mvt_left();
            Light_mvt.instance.light_mvt_left();
        }
        if (Input.GetKeyDown("s") && !Physics.Raycast(transform.position, bac, 1))
        {
            transform.Translate(Vector3.back);
            have_move(true);
            Camera_mvt.instance.Cam_mvt_down();
            Light_mvt.instance.light_mvt_down();
        }
        if (Input.GetKeyDown("d") && !Physics.Raycast(transform.position, rig, 1))
        {
            transform.Translate(Vector3.right);
            have_move(true);
            Camera_mvt.instance.Cam_mvt_right();
            Light_mvt.instance.light_mvt_right();
        }
	}

    public void have_move(bool b)
    {
        move = b;
    }
}
