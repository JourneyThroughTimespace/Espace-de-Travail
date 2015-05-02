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
        transform.Translate(Vector3.up);
    }
    public void light_mvt_left()
    {
        transform.Translate(Vector3.left);
    }
    public void light_mvt_down()
    {
        transform.Translate(Vector3.down);
    }
    public void light_mvt_right()
    {
        transform.Translate(Vector3.right);
    }
}
