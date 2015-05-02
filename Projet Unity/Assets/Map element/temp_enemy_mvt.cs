using UnityEngine;
using System.Collections;

public class temp_enemy_mvt : MonoBehaviour 
{

    public static temp_enemy_mvt instance;
	void Start () 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}



    public void enemy_Turn(char[,]board , Transform target)
    {
        char[,] s = Generation_map.instance.resolution(board, target.transform, transform);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);

        if (s[(int)transform.position.x+1, (int)transform.position.z ] == 'c')
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                //Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Player")
            {
                //didact_Player.instance.LoseLife(dmg);
                //Generation_map.instance.turn++;
            }
        }
        else if (s[(int)transform.position.x - 1, (int)transform.position.z] == 'c')
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                //Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Player")
            {
                //didact_Player.instance.LoseLife(dmg);
                //Generation_map.instance.turn++;
            }
        }
        else if (s[(int)transform.position.x, (int)transform.position.z +1] == 'c')
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                //Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Player")
            {
                //didact_Player.instance.LoseLife(dmg);
                //Generation_map.instance.turn++;
            }
        }
        else
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                //Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Player")
            {
                //didact_Player.instance.LoseLife(dmg);
                //Generation_map.instance.turn++;
            }
        }
        Generation_map.instance.turn++;
    }
}
