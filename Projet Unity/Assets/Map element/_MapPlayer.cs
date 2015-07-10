using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class _MapPlayer : _Entity
{

    public int l;
    public int d;
    public int r;
    public static _MapPlayer instance;
    public AudioClip pas;
    public AudioClip choc;
    AudioSource audio;
    AudioSource audio2;
    public int weaponStatusEffect;

    public Text textHUD;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public string[] playerStatus = { "Fine", "Poisoinned", "Burning", "Stunned", "Dead" };
    public string currentPlayerStatus;
    public string currentWeaponName;

    public GameObject[] dropable;

    private Random random = new Random();

    public override void LoseLife(int loss)
    {
        life -= loss;
        GameObject slid = GameObject.Find("HealthSlider");
        slid.GetComponent<Slider>().value = life;
        /*switch (ennemyStatusEffect)
        {
            case 0:
                break;
            case 1:
                PoisonEffect(2, 4);
                break;
            case 2:
                BurningEffect(4, 2);
                break;
             /*case 3:
                StunEffect(1);
                break;
              * 
        }*/
        if (life <= 0)
        {
            ReturnMenu();
        }
        l = life;
    }

    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio2 = GetComponent<AudioSource>();
        currentPlayerStatus = playerStatus[0];
        life = l;
        dmg = d;
        range = r;
        currentWeaponName = "Katana";
        GameObject canvas = GameObject.Find("HUDCanvas");
        GameObject slid = GameObject.Find("HealthSlider");
        GameObject textHUD = GameObject.Find("StatusText");
        textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
        
        //l = life;
        //d = dmg;
        //r = range;
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }




    public void turnUpdate()
    {
        //textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
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
            audio.PlayOneShot(pas);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
            transform.Translate(Vector3.forward);
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_up();
            _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_up();
            /*_BoardGame.instance.camera.transform.Rotate(30, 0, 0);
            _BoardGame.instance.camera.transform.Translate(Vector3.up);
            _BoardGame.instance.camera.transform.Rotate(-30, 0, 0);*/
        }
        else if (hit.collider.tag == "Enemy")
        {
             audio2.PlayOneShot(choc);
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
                audio.PlayOneShot(pas);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                transform.Translate(Vector3.forward);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_up();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_up();
                /*_BoardGame.instance.camera.transform.Rotate(30, 0, 0);
                _BoardGame.instance.camera.transform.Translate(Vector3.up);
                _BoardGame.instance.camera.transform.Rotate(-30, 0, 0);*/
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc);
                _GameManager.instance.player.GetComponent<_MapPlayer>().l = life;
                _GameManager.instance.player.GetComponent<_MapPlayer>().d = dmg;
                _GameManager.instance.player.GetComponent<_MapPlayer>().r = range;
                _GameManager.instance.player.GetComponent<_MapPlayer>().currentWeaponName = currentWeaponName;
                gameObject.SetActive(false);
                //_MapGameManager.instance.cam.gameObject.SetActive(false);
                Destroy(_MapGameManager.instance.gameObject);
                //_BoardGame.instance.camera.SetActive(false);
                _BoardGame.instance.next_level();
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                _BoardGame.instance.change_State();
                if(Random.Range(0,2) == 0)
                {
                    Instantiate(dropable[Random.Range(0, dropable.Length)], new Vector3((int)transform.position.x, 0, (int)transform.position.z + 1), Quaternion.identity);
                }
            }
            else if (hit.collider.tag == "food")
            {
                audio2.PlayOneShot(choc);
                transform.Translate(Vector3.forward);
                set_life(hit.collider.GetComponent<_Consomable>().soin + life);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_up();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_up();
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Weapon")
            {
                audio2.PlayOneShot(choc);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                set_status(hit.collider.GetComponent<_Weapon>().weaponStatusEffect);
                currentWeaponName = (hit.collider.GetComponent<_Weapon>().weaponName);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                textHUD.GetComponent<Text>().text = "";
                textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
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
            audio.PlayOneShot(pas);
            transform.Translate(Vector3.left);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_left();
            _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_left();
        }
        else if (hit.collider.tag == "Enemy")
        {
            audio2.PlayOneShot(choc);
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_left();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_left();
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(dropable[Random.Range(0, dropable.Length)], new Vector3((int)transform.position.x - 1, 0, (int)transform.position.z), Quaternion.identity);
                }
            }
            else if (hit.collider.tag == "food")
            {
                audio2.PlayOneShot(choc);
                transform.Translate(Vector3.left);
                set_life(hit.collider.GetComponent<_Consomable>().soin + life);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_left();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_left();
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Weapon")
            {
                audio2.PlayOneShot(choc);
                //Destroy(hit.collider.gameObject);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                set_status(hit.collider.GetComponent<_Weapon>().weaponStatusEffect);
                currentWeaponName = (hit.collider.GetComponent<_Weapon>().weaponName);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
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
            audio.PlayOneShot(pas);
            transform.Translate(Vector3.back);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_down();
            _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_down();
        }
        else if (hit.collider.tag == "Enemy")
        {
            audio2.PlayOneShot(choc);
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_down();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_down();
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(dropable[Random.Range(0, dropable.Length)], new Vector3((int)transform.position.x, 0, (int)transform.position.z - 1), Quaternion.identity);
                }
            }
            else if (hit.collider.tag == "food")
            {
                audio2.PlayOneShot(choc);
                transform.Translate(Vector3.back);
                set_life(hit.collider.GetComponent<_Consomable>().soin + life);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_down();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_down();
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Weapon")
            {
                audio2.PlayOneShot(choc);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                set_status(hit.collider.GetComponent<_Weapon>().weaponStatusEffect);
                currentWeaponName = (hit.collider.GetComponent<_Weapon>().weaponName);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
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
            audio.PlayOneShot(pas);
            transform.Translate(Vector3.right);
            transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
            _BoardGame.instance.change_State();
            Camera_mvt.instance.Cam_mvt_right();
            _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_right();
        }
        else if (hit.collider.tag == "Enemy")
        {
            audio2.PlayOneShot(choc);
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_right();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_right();
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc);
                Destroy(hit.collider.gameObject);
                _BoardGame.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(dropable[Random.Range(0, dropable.Length)], new Vector3((int)transform.position.x + 1, 0, (int)transform.position.z), Quaternion.identity);
                }
            }
            else if (hit.collider.tag == "food")
            {
                audio2.PlayOneShot(choc);
                transform.Translate(Vector3.right);
                set_life(hit.collider.GetComponent<_Consomable>().soin + life);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                _BoardGame.instance.change_State();
                Camera_mvt.instance.Cam_mvt_right();
                _BoardGame.instance.li.GetComponent<Light_mvt>().light_mvt_right();
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Weapon")
            {
                audio2.PlayOneShot(choc);
                set_dmg(hit.collider.GetComponent<_Weapon>().dmg);
                set_range(hit.collider.GetComponent<_Weapon>().range);
                set_status(hit.collider.GetComponent<_Weapon>().weaponStatusEffect);
                currentWeaponName = (hit.collider.GetComponent<_Weapon>().weaponName);
                hit.collider.gameObject.SetActive(false);
                _BoardGame.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                textHUD.GetComponent<Text>().text = "Status:" + playerStatus[0] + "\n Weapon: " + currentWeaponName;
                _BoardGame.instance.change_State();
            }
        }
    }

   /*public override void StunEffect(int Duration)
    {

        while (Duration > 0)
        {
            currentPlayerStatus = playerStatus[3];
            isStun = true;
            _BoardGame.instance.set_turn(2);
            Duration = Duration - 1;
        }
        currentPlayerStatus = playerStatus[0];
        isStun = false;
    }
     */ // je l'ai fait avec boarddiadact faut le mettre avec boardgame

    override public void BurningEffect(int burningDamage, int Duration)
    {
        while (Duration > 0)
        {
            isBurning = true;
            currentPlayerStatus = playerStatus[2];
            life -= burningDamage;
            Duration = Duration - 1;
            GameObject colorFill = GameObject.Find("Fill");
            colorFill.GetComponent<Image>().color = new Color(0f, 255f, 0f, 100f);
        }
        isBurning = false;
        currentPlayerStatus = playerStatus[0];
    }
    public override void PoisonEffect(int poisonDamage, int Duration)
    {
        /*while (Duration > 0)
        {
            currentPlayerStatus = playerStatus[1];
            isPoisoned = true;
            life -= poisonDamage;
            Duration = Duration - 1;
            GameObject colorFill = GameObject.Find("Fill");
            colorFill.GetComponent<Image>().color = new Color(200f, 180f, 0f, 100f);
        }*/

        life -= poisonDamage;
        Duration--;
        if (Duration == 0)
        {
            isPoisoned = false;
            currentPlayerStatus = playerStatus[0];
        }
    }


    private void ReturnMenu()
    {
        //l = 10;
        //d = 5;
        //r = 1;
        Application.LoadLevel("Menu0.2");
    }

    void Update()
    {
        l = life;
    }
}
