using UnityEngine;
using System.Collections;

public class _didactEnemy : _Enemy 
{
    //public static _didactEnemy instance;
	
	void Start () 
    {
        life = 10;
        dmg = 2;
        //instance = this;
	}

    public override void turnUpdate(Transform Target)
    {
        if (Target.position.x > transform.position.x)
        {
            move_right();
        }
        else if (Target.position.x < transform.position.x)
        {
            move_left();
        }
        else if (Target.position.z > transform.position.z)
        {
            move_forward();
        }
        else
        {
            move_back();
        }
    }

    private void move_forward()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Physics.Raycast(transform.position, fwd, out hit, 1);
        if (!Physics.Raycast(transform.position, fwd, 1))
        {
            transform.Translate(Vector3.forward);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            _Player p = hit.collider.gameObject.GetComponent<_Player>();
            p.LoseLife(dmg);
            
        }
    }
    private void move_left()
    {
        Vector3 lef = transform.TransformDirection(Vector3.left);

        RaycastHit hit;
        Physics.Raycast(transform.position, lef, out hit, 1);
        if (!Physics.Raycast(transform.position, lef, 1))
        {
            transform.Translate(Vector3.left);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            
        }
        else if (hit.collider.tag == "Player")
        {
            _Player p = hit.collider.gameObject.GetComponent<_Player>();
            p.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            
        }
    }
    private void move_back()
    {
        Vector3 bac = transform.TransformDirection(Vector3.back);

        RaycastHit hit;
        Physics.Raycast(transform.position, bac, out hit, 1);
        if (!Physics.Raycast(transform.position, bac, 1))
        {
            transform.Translate(Vector3.back);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            _Player p = hit.collider.gameObject.GetComponent<_Player>();
            p.LoseLife(dmg);
            
        }
    }
    private void move_right()
    {
        Vector3 rig = transform.TransformDirection(Vector3.right);

        RaycastHit hit;
        Physics.Raycast(transform.position, rig, out hit, 1);
        if (!Physics.Raycast(transform.position, rig, 1))
        {
            transform.Translate(Vector3.right);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            _Player p = hit.collider.gameObject.GetComponent<_Player>();
            p.LoseLife(dmg);
            
        }
    }
}
