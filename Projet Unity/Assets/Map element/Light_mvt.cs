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
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.up);
            Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void light_mvt_left()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.left);
            Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void light_mvt_down()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.down);
            Temp_Perso_mvt.instance.have_move(false);
        }
    }
    public void light_mvt_right()
    {
        if (Temp_Perso_mvt.instance.move)
        {
            transform.Translate(Vector3.right);
            Temp_Perso_mvt.instance.have_move(false);
        }
    }
}
