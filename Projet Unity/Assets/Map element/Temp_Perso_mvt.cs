using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Temp_Perso_mvt : MonoBehaviour {

    public static Temp_Perso_mvt instance;
    public bool move = false;
    public int life = 100;
    public int dmg = 50;
    public int range = 1;
    public AudioClip pas;
    public AudioClip choc;
    AudioSource audio;
    AudioSource audio2;
    public GameObject canvas;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;
    public Image damageImage;
    private Slider healthSlider;
    



    public void LoseLife(int loss)
    {
        
        life -= loss;
        GameObject slid = GameObject.Find("HealthSlider");
        slid.GetComponent<Slider>().value = life;
        //canvas.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent <Slider> ().value = life;
        if (life <= 0)
        {
            Generation_map.instance.GameOver();
        }
    }

	void Start () 
    {
        Instantiate(canvas);
        GameObject slid = GameObject.Find("HealthSlider");
        slid.GetComponent<Slider>().value = life;
        audio = GetComponent<AudioSource>();
        audio2 = GetComponent<AudioSource>();
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
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                Camera_mvt.instance.Cam_mvt_up();
                Light_mvt.instance.light_mvt_up();
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Generation_map.instance.turn++;
                audio.PlayOneShot(pas, 0.8F);


            }
            else if (hit.collider.tag == "Enemy")
            {
                audio2.PlayOneShot(choc, 0.8F);
                temp_enemy_mvt.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z + 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (Input.GetKeyDown("a"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                
                Camera_mvt.instance.Cam_mvt_left();
                transform.Translate(Vector3.left);
                Light_mvt.instance.light_mvt_left();
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Generation_map.instance.turn++;
                audio.PlayOneShot(pas, 0.8F);
            }
            else if (hit.collider.tag == "Enemy")
            {
                audio2.PlayOneShot(choc, 0.8F);
                temp_enemy_mvt.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x - 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Generation_map.instance.turn++;
            }
        }
        if (Input.GetKeyDown("s"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                Camera_mvt.instance.Cam_mvt_down();
                transform.Translate(Vector3.back);
                Light_mvt.instance.light_mvt_down();
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Generation_map.instance.turn++;
                audio.PlayOneShot(pas, 0.8F);
            }
            else if (hit.collider.tag == "Enemy")
            {
                audio2.PlayOneShot(choc, 0.8F);
                temp_enemy_mvt.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x, (int)transform.position.z - 1] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Generation_map.instance.turn++;
            }
        }
        if (Input.GetKeyDown("d"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                Camera_mvt.instance.Cam_mvt_right();
                transform.Translate(Vector3.right);
                Light_mvt.instance.light_mvt_right();
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Generation_map.instance.turn++;
                audio.PlayOneShot(pas, 0.8F);
            }
            else if (hit.collider.tag == "Enemy")
            {
                audio2.PlayOneShot(choc, 0.8F);
                temp_enemy_mvt.instance.LoseLife(dmg);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Generation_map.instance.turn++;
            }
            else if (hit.collider.tag == "blocking")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Destroy(hit.collider.gameObject);
                Generation_map.instance.gameBoard[(int)transform.position.x + 1, (int)transform.position.z] = 'e';
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Generation_map.instance.turn++;
            }
        }
    }
}
