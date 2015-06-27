using UnityEngine;
using System.Collections;

public class _MapEnemy : _Enemy
{
    public override void turnUpdate(Transform target)
    {
        string s = _BoardGame.instance.solve(_BoardGame.instance.gameBoard, target.transform, transform, range);
        if (s != "null")
        {
            if (s == "right")
            {
                move_right();
            }
            else if (s == "left")
            {
                move_left();
            }
            else if (s == "forward")
            {
                move_forward();
            }
            else
            {
                move_back();
            }
        }
        
    }
    private void move_forward()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Physics.Raycast(transform.position, fwd, out hit, range);
        if (!Physics.Raycast(transform.position, fwd, range))
        {
            transform.Translate(Vector3.forward);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            _MapPlayer p = hit.collider.gameObject.GetComponent<_MapPlayer>();
            p.LoseLife(dmg);
        }
        else if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy" || hit.collider.tag == "blocking")
        {
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            }
        }
    }
    private void move_left()
    {
        Vector3 lef = transform.TransformDirection(Vector3.left);

        RaycastHit hit;
        Physics.Raycast(transform.position, lef, out hit, range);
        if (!Physics.Raycast(transform.position, lef, range))
        {
            transform.Translate(Vector3.left);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
        }
        else if (hit.collider.tag == "Player")
        {
            _MapPlayer p = hit.collider.gameObject.GetComponent<_MapPlayer>();
            p.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
        }
        else if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy" || hit.collider.tag == "blocking")
        {
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            }
        }
    }
    private void move_back()
    {
        Vector3 bac = transform.TransformDirection(Vector3.back);

        RaycastHit hit;
        Physics.Raycast(transform.position, bac, out hit, range);
        if (!Physics.Raycast(transform.position, bac, range))
        {
            transform.Translate(Vector3.back);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            _MapPlayer p = hit.collider.gameObject.GetComponent<_MapPlayer>();
            p.LoseLife(dmg);
        }
        else if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy" || hit.collider.tag == "blocking")
        {
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            }
        }
    }
    private void move_right()
    {
        Vector3 rig = transform.TransformDirection(Vector3.right);

        RaycastHit hit;
        Physics.Raycast(transform.position, rig, out hit, range);
        if (!Physics.Raycast(transform.position, rig, range))
        {
            transform.Translate(Vector3.right);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
        }
        else if (hit.collider.tag == "Player")
        {
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            _MapPlayer p = hit.collider.gameObject.GetComponent<_MapPlayer>();
            p.LoseLife(dmg);
        }
        else if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy" || hit.collider.tag == "blocking")
        {
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            }
        }
    }

    public int l;
    public int d;
    public int r;

	// Use this for initialization
	void Start () 
    {
        life = l;
        dmg = d;
        range = r;
	}
	
	// Update is called once per frame
	void Update () 
    {
        l = life;
	}
}
