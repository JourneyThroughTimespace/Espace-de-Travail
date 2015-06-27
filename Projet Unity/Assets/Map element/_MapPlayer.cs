using UnityEngine;
using System.Collections;

public class _MapPlayer : _Entity
{

    public int l;
    public int d;
    public int r;
    public static _MapPlayer instance;

    public override void LoseLife(int loss)
    {
        life -= loss;
        if (life <= 0)
        {
            ReturnMenu();
        }
        l = life;
    }

    void Start()
    {
        life = l;
        dmg = d;
        range = r;
        //l = life;
        //d = dmg;
        //r = range;
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }




    public void turnUpdate()
    {
        if (Input.GetKeyDown("w"))
        {
            move_forward();
        }
        else if (Input.GetKeyDown("a"))
        {
            move_left();
        }
        else if (Input.GetKeyDown("s"))
        {
            move_back();
        }
        else if (Input.GetKeyDown("d"))
        {
            move_right();
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
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_up();
            /*_BoardGame.instance.camera.transform.Rotate(30, 0, 0);
            _BoardGame.instance.camera.transform.Translate(Vector3.up);
            _BoardGame.instance.camera.transform.Rotate(-30, 0, 0);*/
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            _BoardGame.instance.change_State();
        }
        else
        {
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                transform.Translate(Vector3.forward);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_up();
                /*_BoardGame.instance.camera.transform.Rotate(30, 0, 0);
                _BoardGame.instance.camera.transform.Translate(Vector3.up);
                _BoardGame.instance.camera.transform.Rotate(-30, 0, 0);*/
            }
            else if (hit.collider.tag == "Exit")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                _GameManager.instance.player.set_life(life);
                _GameManager.instance.player.set_dmg(dmg);
                _GameManager.instance.player.set_range(range);
                gameObject.SetActive(false);
                //_MapGameManager.instance.cam.gameObject.SetActive(false);
                Destroy(_MapGameManager.instance.gameObject);
                //_BoardGame.instance.camera.SetActive(false);
                _BoardGame.instance.next_level();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                _BoardGame.instance.change_State();
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                _BoardGame.instance.change_State();
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
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_left();
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            _BoardGame.instance.change_State();
        }
        else
        {
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_left();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
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
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_down();
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            _BoardGame.instance.change_State();
        }
        else
        {
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_down();
            }
            else if (hit.collider.tag == "blocking")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
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
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_right();
        }
        else if (hit.collider.tag == "Enemy")
        {
            //audio2.PlayOneShot(choc, 0.8F);
            _Enemy e = hit.collider.gameObject.GetComponent<_Enemy>();
            e.LoseLife(dmg);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            _BoardGame.instance.change_State();
        }
        else
        {
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                //audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_right();
            }
            else if (hit.collider.tag == "blocking")
            {
                // audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
            }
            else if (hit.collider.tag == "Weapon")
            {
                //audio2.PlayOneShot(choc, 0.8F);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
            }
        }
    }


    private void ReturnMenu()
    {
        l = 10;
        Application.LoadLevel("Menu0.2");
    }
}
