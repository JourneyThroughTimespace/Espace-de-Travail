using UnityEngine;
using System.Collections;

public class Light_mvt : MonoBehaviour
{

    public static Light_mvt instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void light_mvt_up()
    {
        transform.Rotate(5, 0, 0);
        transform.Translate(Vector3.up);
        transform.Rotate(-5, 0, 0);
    }
    public void light_mvt_left()
    {
        transform.Rotate(5, 0, 0);
        transform.Translate(Vector3.left);
        transform.Rotate(-5, 0, 0);
    }
    public void light_mvt_down()
    {
        transform.Rotate(5, 0, 0);
        transform.Translate(Vector3.down);
        transform.Rotate(-5, 0, 0);
    }
    public void light_mvt_right()
    {
        transform.Rotate(5, 0, 0);
        transform.Translate(Vector3.right);
        transform.Rotate(-5, 0, 0);
    }
}
