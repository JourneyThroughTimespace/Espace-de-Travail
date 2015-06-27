using UnityEngine;
using System.Collections;
using System;

public class Camera_mvt : MonoBehaviour 
{
    public static Camera_mvt instance;
	// Use this for initialization
   
	void Start () 
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void Cam_mvt_up()
    {
        transform.Rotate(30, 0, 0);
        transform.Translate(Vector3.up);
        transform.Rotate(-30, 0, 0);
    }
    public void Cam_mvt_left()
    {
        transform.Translate(Vector3.left);
    }
    public void Cam_mvt_down()
    {
        transform.Rotate(30, 0, 0);
        transform.Translate(Vector3.down);
        transform.Rotate(-30, 0, 0);
    }
    public void Cam_mvt_right()
    {
        transform.Translate(Vector3.right);
    }



    /*
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }*/
}
