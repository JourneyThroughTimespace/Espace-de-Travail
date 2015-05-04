using UnityEngine;
using System.Collections;

public class PlayerC : MonoBehaviour {
    public int life = 100;
    public int dmg = 20;
    public int range = 1;
    public static didact_PlayerMulti instance;
    private Transform _transform;
    private NetworkView NTV;
    public void LoseLife(int loss)
    {        
            life -= loss;
            if (life <= 0)
            {
                Map_didact.instance.GameOver();
            }        
    }
    private void ReturnMenu()
    {        
            Application.LoadLevel("Menu0.1");
    }
	void Start () {
        _transform = GetComponent<Transform>();
        NTV = GetComponent<NetworkView>();
	}

    public void TurnUpdate()
    {
        if (NTV.isMine)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 lef = transform.TransformDirection(Vector3.left);
            Vector3 bac = transform.TransformDirection(Vector3.back);
            Vector3 rig = transform.TransformDirection(Vector3.right);
            if (Input.GetKeyDown("z"))// && !Physics.Raycast(transform.position, fwd, 1))
            {
                RaycastHit hit;
                bool b = Physics.Raycast(transform.position, fwd, out hit, 1);
                if (!Physics.Raycast(transform.position, fwd, 1))
                {
                    transform.Translate(Vector3.forward);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                    _transform.TransformDirection(fwd);
                }
                else if (hit.collider.tag == "Exit")
                {
                    ReturnMenu();
                }
                else if (hit.collider.tag == "Enemy")
                {
                    didact_EnemyMulti.instance.LoseLife(dmg);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                }
            }
            if (Input.GetKeyDown("q"))// && !Physics.Raycast(transform.position, lef, 1))
            {
                //transform.Translate(Vector3.left);
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                RaycastHit hit;
                bool b = Physics.Raycast(transform.position, lef, out hit, 1);
                if (!Physics.Raycast(transform.position, lef, 1))
                {
                    transform.Translate(Vector3.left);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                    _transform.TransformDirection(lef);
                }
                else if (hit.collider.tag == "Exit")
                {
                    ReturnMenu();
                }
                else if (hit.collider.tag == "Enemy")
                {
                    didact_EnemyMulti.instance.LoseLife(dmg);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                }
            }
            if (Input.GetKeyDown("s"))// && !Physics.Raycast(transform.position, bac, 1))
            {
                //transform.Translate(Vector3.back);
                //Map_didact.instance.change_state(M_didact.instance.game_state + 1);
                RaycastHit hit;
                bool b = Physics.Raycast(transform.position, bac, out hit, 1);
                if (!Physics.Raycast(transform.position, bac, 1))
                {
                    transform.Translate(Vector3.back);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                    _transform.TransformDirection(bac);
                }
                else if (hit.collider.tag == "Exit")
                {
                    ReturnMenu();
                }
                else if (hit.collider.tag == "Enemy")
                {
                    didact_EnemyMulti.instance.LoseLife(dmg);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                }
            }
            if (Input.GetKeyDown("d") && !Physics.Raycast(transform.position, rig, 1))
            {
                //transform.Translate(Vector3.right);
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                RaycastHit hit;
                bool b = Physics.Raycast(transform.position, rig, out hit, 1);
                if (!Physics.Raycast(transform.position, rig, 1))
                {
                    transform.Translate(Vector3.right);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                    _transform.TransformDirection(rig);
                }
                else if (hit.collider.tag == "Exit")
                {
                    ReturnMenu();
                }
                else if (hit.collider.tag == "Enemy")
                {
                    didact_EnemyMulti.instance.LoseLife(dmg);
                    Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
                }
            }
        }
    }
	
}
