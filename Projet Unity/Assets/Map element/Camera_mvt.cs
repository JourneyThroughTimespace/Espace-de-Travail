using UnityEngine;
using System.Collections;

public class Camera_mvt : MonoBehaviour 
{
    public static Camera_mvt instance;

	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        Vector3 pos = transform.position - new Vector3(0, 10, 0);
        if (Input.GetKeyDown("w") && !Physics.Raycast(pos, fwd, 1))
        {
            transform.Translate(Vector3.up);
           // Camera_mvt.instance.Cam_mvt_up();
        }
        if (Input.GetKeyDown("a") && !Physics.Raycast(pos, lef, 1))
        {
            transform.Translate(Vector3.left);
            //Camera_mvt.instance.Cam_mvt_left();
        }
        if (Input.GetKeyDown("s") && !Physics.Raycast(pos, bac, 1))
        {
            transform.Translate(Vector3.down);
            //Camera_mvt.instance.Cam_mvt_down();
        }
        if (Input.GetKeyDown("d") && !Physics.Raycast(pos, rig, 1))
        {
            transform.Translate(Vector3.right);
           // Camera_mvt.instance.Cam_mvt_right();
        }*/
	}
    public void Cam_mvt_up()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.up);
            //Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void Cam_mvt_left()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.left);
            //Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void Cam_mvt_down()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.down);
            //Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void Cam_mvt_right()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.right);
            //Temp_Perso_mvt.instance.have_move(false);
        }
    }
}
