using UnityEngine;
using System.Collections;

public class rotate_model : MonoBehaviour 
{
    public static rotate_model instance;
	void Start () 
    {
        instance = this;
	}
	

	void Update () 
    {
	
	}

    public void turn_forward()
    {
        transform.Rotate(0, - transform.eulerAngles.y , 0);
    }
    public void turn_left()
    {
        transform.Rotate(0,  - transform.eulerAngles.y - 90, 0);      
    }
    public void turn_right()
    {
        transform.Rotate(0, - transform.eulerAngles.y + 90, 0);
    }
    public void turn_back()
    {
        transform.Rotate(0, - transform.eulerAngles.y + 180, 0);
    }
}
