using UnityEngine;
using System.Collections;

public class didact_Enemy : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void TurnUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown("up") && !Physics.Raycast(transform.position, fwd, 1))
        {
            transform.Translate(Vector3.forward);
            Map_didact.instance.change_state(0);
        }
        if (Input.GetKeyDown("left") && !Physics.Raycast(transform.position, lef, 1))
        {
            transform.Translate(Vector3.left);
            Map_didact.instance.change_state(0);
        }
        if (Input.GetKeyDown("down") && !Physics.Raycast(transform.position, bac, 1))
        {
            transform.Translate(Vector3.back);
            Map_didact.instance.change_state(0);
        }
        if (Input.GetKeyDown("right") && !Physics.Raycast(transform.position, rig, 1))
        {
            transform.Translate(Vector3.right);
            Map_didact.instance.change_state(0);
        }
    }
}
