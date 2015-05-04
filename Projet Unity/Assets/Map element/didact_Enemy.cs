using UnityEngine;
using System.Collections;

public class didact_Enemy : MonoBehaviour
{
    public int life = 10;
    public int dmg = 2;

    public static didact_Enemy instance;
    //private Transform Target;
    void Start()
    {
        instance = this;
        //Target = GameObject.FindGameObjectWithTag("perso").transform;
    }

    public void LoseLife(int loss)
    {
        life -= loss;
        if (life <= 0)
        {
            dmg = 0;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    void Update()
    {

    }
    public void TurnUpdate(Transform Target)
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);

        if (Target.position.x > transform.position.x)
        {
            //transform.Translate(Vector3.right);
            //Map_didact.instance.change_state(0);
            RaycastHit hit;
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Map_didact.instance.change_state(0);
            }
            else if (hit.collider.tag == "Player")
            {
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                didact_Player.instance.LoseLife(dmg);
                Map_didact.instance.change_state(0);
            }
        }
        else if (Target.position.x < transform.position.x)
        {
            //transform.Translate(Vector3.left);
            //Map_didact.instance.change_state(0);
            RaycastHit hit;
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Map_didact.instance.change_state(0);
            }
            else if (hit.collider.tag == "Player")
            {
                didact_Player.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Map_didact.instance.change_state(0);
            }
        }
        else if (Target.position.z > transform.position.z)
        {
            //transform.Translate(Vector3.forward);
            //Map_didact.instance.change_state(0);
            RaycastHit hit;
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Map_didact.instance.change_state(0);
            }
            else if (hit.collider.tag == "Player")
            {
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                didact_Player.instance.LoseLife(dmg);
                Map_didact.instance.change_state(0);
            }
        }
        else
        {
            //transform.Translate(Vector3.back);
            //Map_didact.instance.change_state(0);
            RaycastHit hit;
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Map_didact.instance.change_state(0);
            }
            else if (hit.collider.tag == "Player")
            {
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                didact_Player.instance.LoseLife(dmg);
                Map_didact.instance.change_state(0);
            }
        }
    }
}