using UnityEngine;
using System.Collections;

public class Temp_Perso_mvt : MonoBehaviour {

    public static Temp_Perso_mvt instance;
    public bool move = false;
	

	void Start () 
    {
        instance = this;
	}
    private void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
	
	
	void Update () 
    {
        
	}

    public void player_turn()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown("w"))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Enemy")
            {
                //didact_Enemy.instance.LoseLife(dmg);
                Generation_map.instance.turn++;
            }
        }
        if (Input.GetKeyDown("a"))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Enemy")
            {
                //didact_Enemy.instance.LoseLife(dmg);
                Generation_map.instance.turn++;
            }
        }
        if (Input.GetKeyDown("s"))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Enemy")
            {
                //didact_Enemy.instance.LoseLife(dmg);
                Generation_map.instance.turn++;
            }
        }
        if (Input.GetKeyDown("d"))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Enemy")
            {
                //didact_Enemy.instance.LoseLife(dmg);
                Generation_map.instance.turn++;
            }
        }
    }

    public void have_move(bool b)
    {
        move = b;
    }
}
