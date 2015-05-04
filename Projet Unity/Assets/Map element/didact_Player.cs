using UnityEngine;
using System.Collections;

public class didact_Player : MonoBehaviour
{
    public int life = 10;
    public int dmg = 20;
    public int range = 1;
    public static didact_Player instance;
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
        Application.LoadLevel("Menu0.2");
    }

    void Start()
    {
        instance = this;
    }
    
    public void TurnUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown("w"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y , 0);
                transform.Translate(Vector3.forward);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                ReturnMenu();
            }
            else if (hit.collider.tag == "Enemy")
            {
                didact_Enemy.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
        }
        if (Input.GetKeyDown("a"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                ReturnMenu();
            }
            else if (hit.collider.tag == "Enemy")
            {
                didact_Enemy.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
        }
        if (Input.GetKeyDown("s"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                ReturnMenu();
            }
            else if (hit.collider.tag == "Enemy")
            {
                didact_Enemy.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
        }
        if (Input.GetKeyDown("d") && !Physics.Raycast(transform.position, rig, 1))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                ReturnMenu();
            }
            else if (hit.collider.tag == "Enemy")
            {
                didact_Enemy.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
        }
    }
}
