using UnityEngine;
using System.Collections;

public class persoMenu : MonoBehaviour {

    public static persoMenu instance;
    public bool move = false;
    public Renderer rend1;
    public GameObject vol1;
    public GameObject vol2;
    public GameObject vol3;
    public GameObject vol4;
    public GameObject vol5;
    public GameObject voll1;
    public GameObject voll2;
    public GameObject voll3;
    public GameObject voll4;
    public GameObject voll5;
    public Material mat1;
    public Material mat2;
    int compt;
    int compt2;
    public AudioClip pas;
    public AudioClip choc;
    AudioSource audio;
    AudioSource audio2;

    public Transform light;

	// Use this for initialization
	void Start () 
    {
        audio = GetComponent<AudioSource>();
        audio2 = GetComponent<AudioSource>();
        instance = this;
        compt = 5;
        compt2 = 5;
        light.transform.Translate(0, 0, 0);
	}
    private void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 lef = transform.TransformDirection(Vector3.left);
        Vector3 bac = transform.TransformDirection(Vector3.back);
        Vector3 rig = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown("w"))// && !Physics.Raycast(transform.position, fwd, 1))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.forward);
                //rotate_model.instance.turn_back();
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Camera_mvt.instance.Cam_mvt_up();
                //Light_mvt.instance.light_mvt_up();
                

                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "cubeplay")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Map");
            }
            else if (hit.collider.tag == "cubetuto")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Didactitiel");
            }
            else if (hit.collider.tag == "cubemulti")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Multi_build");
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc, 0.8F);
                NextLevel();
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.Quit();
            }
            else if (hit.collider.tag == "plusvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                if(compt == 0)
                {
                    vol1.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 1)
                {
                    vol2.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 2)
                {
                    vol3.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 3)
                {
                    vol4.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 4)
                {
                    vol5.GetComponent<Renderer>().material = mat1;
                    compt++;
                }      
            }
            else if (hit.collider.tag == "moinsvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                if (compt == 5)
                {
                    vol5.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 4)
                {
                    vol4.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 3)
                {
                    vol3.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 2)
                {
                    vol2.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 1)
                {
                    vol1.GetComponent<Renderer>().material = mat2;
                    compt--;
                }

            }
            else if (hit.collider.tag == "pluseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                if (compt2 == 0)
                {
                    voll1.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 1)
                {
                    voll2.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 2)
                {
                    voll3.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 3)
                {
                    voll4.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 4)
                {
                    voll5.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
            }
            else if (hit.collider.tag == "moinseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                if (compt2 == 5)
                {
                    voll5.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 4)
                {
                    voll4.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 3)
                {
                    voll3.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 2)
                {
                    voll2.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 1)
                {
                    voll1.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }

            }
        }
        if (Input.GetKeyDown("a"))// && !Physics.Raycast(transform.position, lef, 1))
        {
            //transform.Translate(Vector3.left);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            RaycastHit hit;
            Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Camera_mvt.instance.Cam_mvt_left();
                //Light_mvt.instance.light_mvt_left();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "cubeplay")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Map");
            }
            else if (hit.collider.tag == "cubetuto")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Didactitiel");
            }
            else if (hit.collider.tag == "cubemulti")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Multi_build");
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc, 0.8F);
                NextLevel();
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.Quit();
            }
            else if (hit.collider.tag == "plusvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                if (compt == 0)
                {
                    vol1.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 1)
                {
                    vol2.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 2)
                {
                    vol3.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 3)
                {
                    vol4.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 4)
                {
                    vol5.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
            }
            else if (hit.collider.tag == "moinsvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                if (compt == 5)
                {
                    vol5.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 4)
                {
                    vol4.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 3)
                {
                    vol3.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 2)
                {
                    vol2.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 1)
                {
                    vol1.GetComponent<Renderer>().material = mat2;
                    compt--;
                }

            }
            else if (hit.collider.tag == "pluseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                if (compt2 == 0)
                {
                    voll1.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 1)
                {
                    voll2.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 2)
                {
                    voll3.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 3)
                {
                    voll4.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 4)
                {
                    voll5.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
            }
            else if (hit.collider.tag == "moinseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                if (compt2 == 5)
                {
                    voll5.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 4)
                {
                    voll4.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 3)
                {
                    voll3.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 2)
                {
                    voll2.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 1)
                {
                    voll1.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }

            }
        }
        if (Input.GetKeyDown("s"))// && !Physics.Raycast(transform.position, bac, 1))
        {
            //transform.Translate(Vector3.back);
            //Map_didact.instance.change_state(M_didact.instance.game_state + 1);
            RaycastHit hit;
            Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Camera_mvt.instance.Cam_mvt_down();
                //Light_mvt.instance.light_mvt_down();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "cubeplay")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Map");
            }
            else if (hit.collider.tag == "cubetuto")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Didactitiel");
            }
            else if (hit.collider.tag == "cubemulti")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Multi_build");
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc, 0.8F);
                NextLevel();
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.Quit();
            }
            else if (hit.collider.tag == "plusvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                if (compt == 0)
                {
                    vol1.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 1)
                {
                    vol2.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 2)
                {
                    vol3.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 3)
                {
                    vol4.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 4)
                {
                    vol5.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
            }
            else if (hit.collider.tag == "moinsvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                if (compt == 5)
                {
                    vol5.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 4)
                {
                    vol4.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 3)
                {
                    vol3.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 2)
                {
                    vol2.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 1)
                {
                    vol1.GetComponent<Renderer>().material = mat2;
                    compt--;
                }

            }
            else if (hit.collider.tag == "pluseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                if (compt2 == 0)
                {
                    voll1.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 1)
                {
                    voll2.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 2)
                {
                    voll3.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 3)
                {
                    voll4.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 4)
                {
                    voll5.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
            }
            else if (hit.collider.tag == "moinseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                if (compt2 == 5)
                {
                    voll5.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 4)
                {
                    voll4.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 3)
                {
                    voll3.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 2)
                {
                    voll2.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 1)
                {
                    voll1.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }

            }
        }
        if (Input.GetKeyDown("d"))// && !Physics.Raycast(transform.position, rig, 1))
        {
            //transform.Translate(Vector3.right);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            RaycastHit hit;
            Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                audio.PlayOneShot(pas, 0.8F);
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Camera_mvt.instance.Cam_mvt_right();
                //Light_mvt.instance.light_mvt_right();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "cubeplay")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Map");
            }
            else if (hit.collider.tag == "cubetuto")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Didactitiel");
            }
            else if (hit.collider.tag == "cubemulti")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.LoadLevel("Multi_build");
            }
            else if (hit.collider.tag == "Exit")
            {
                audio2.PlayOneShot(choc, 0.8F);
                NextLevel();
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc, 0.8F);
                Application.Quit();
            }
            else if (hit.collider.tag == "plusvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                if (compt == 0)
                {
                    vol1.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 1)
                {
                    vol2.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 2)
                {
                    vol3.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 3)
                {
                    vol4.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
                else if (compt == 4)
                {
                    vol5.GetComponent<Renderer>().material = mat1;
                    compt++;
                }
            }
            else if (hit.collider.tag == "moinsvol")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                if (compt == 5)
                {
                    vol5.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 4)
                {
                    vol4.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 3)
                {
                    vol3.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 2)
                {
                    vol2.GetComponent<Renderer>().material = mat2;
                    compt--;
                }
                else if (compt == 1)
                {
                    vol1.GetComponent<Renderer>().material = mat2;
                    compt--;
                }

            }
            else if (hit.collider.tag == "pluseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                if (compt2 == 0)
                {
                    voll1.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 1)
                {
                    voll2.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 2)
                {
                    voll3.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 3)
                {
                    voll4.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
                else if (compt2 == 4)
                {
                    voll5.GetComponent<Renderer>().material = mat1;
                    compt2++;
                }
            }
            else if (hit.collider.tag == "moinseff")
            {
                audio2.PlayOneShot(choc, 0.8F);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                if (compt2 == 5)
                {
                    voll5.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 4)
                {
                    voll4.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 3)
                {
                    voll3.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 2)
                {
                    voll2.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }
                else if (compt2 == 1)
                {
                    voll1.GetComponent<Renderer>().material = mat2;
                    compt2--;
                }

            }
        }
	}
}
