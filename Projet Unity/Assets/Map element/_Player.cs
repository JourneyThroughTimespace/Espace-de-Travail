using UnityEngine;
using System.Collections;

public class _Player : _Entity
{
    public int l;
    public int d;
    public int r;
    public static _Player instance;

    public override void LoseLife(int loss)
    {
        life -= loss;
        if (life <= 0)
        {
            ReturnMenu();
        }
        //l = life;
    }

	void Start () 
    {
        life = l;
        dmg = d;
        range = r;
        instance = this;
	}
    



    public void turnUpdate()
    {
        if (Input.GetKeyDown("w"))
        {
            move_forward();
            _BoardDiadact.instance.change_State();
        }
        if (Input.GetKeyDown("a"))
        {
            move_left();
            _BoardDiadact.instance.change_State();
        }
        if (Input.GetKeyDown("s"))
        {
            move_back();
            _BoardDiadact.instance.change_State();
        }
        if (Input.GetKeyDown("d"))
        {
            move_right();
            _BoardDiadact.instance.change_State();
        }
        
    }

    private void move_forward()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Physics.Raycast(transform.position, fwd, out hit, range);
        if (!Physics.Raycast(transform.position, fwd, range))
        {
            //audio.PlayOneShot(pas, 0.8F);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            transform.Translate(Vector3.forward);
            
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            
        }
        else
        {
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                transform.Translate(Vector3.forward);
                
            }
            else if (hit.collider.tag == "Exit")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                _GameManager.instance.player.l = life;
                _GameManager.niveau++;
                Destroy(gameObject);
                _BoardDiadact.instance.next_level();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
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
            //audio.PlayOneShot(pas, 0.8F);
            transform.Translate(Vector3.left);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            
        }
        else
        {
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                
            }
            else if (hit.collider.tag == "Exit")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                ReturnMenu();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                Generation_map.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                
            }
        }
    }

    public void move_back()
    {
        Vector3 bac = transform.TransformDirection(Vector3.back);

        RaycastHit hit;
        Physics.Raycast(transform.position, bac, out hit, range);
        if (!Physics.Raycast(transform.position, bac, range))
        {
            //audio.PlayOneShot(pas, 0.8F);
            transform.Translate(Vector3.back);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            
        }
        else
        {
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                
            }
            else if (hit.collider.tag == "Exit")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                ReturnMenu();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
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
            //audio.PlayOneShot(pas, 0.8F);
            transform.Translate(Vector3.right);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            
        }
        else
        {
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                
            }
            else if (hit.collider.tag == "Exit")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                ReturnMenu();
            }
            else if (hit.collider.tag == "blocking")
            {
               // audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                Generation_map.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                
            }
        }
    }


    private void ReturnMenu()
    {
        l = 10;
        Application.LoadLevel("Menu0.2");
    }


    /*void Update()
    {
        l = life;
        d = dmg;
        r = range;
    }*/
}
