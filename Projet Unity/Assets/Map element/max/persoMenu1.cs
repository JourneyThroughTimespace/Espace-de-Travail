using UnityEngine;
using System.Collections;

public class persoMenu1 : MonoBehaviour
{

    public static persoMenu1 instance;
    public bool move = false;
    public Material mat1;
    public Material mat2;
    public GameObject CubeJoin;
    public AudioClip pas;
    public AudioClip choc;
    AudioSource audio;
    AudioSource audio2;

    //Network
    string TypeName = "JTT4LIFE";
    string GameName = "Room 1";
    HostData[] hostData;
    public string levelToLoad;
    public static HostData GameToJoin = null;

    public Transform light;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio2 = GetComponent<AudioSource>();
        instance = this;
        light.transform.Translate(0, 0, 0);
        //audio = persoMenu.audio;
        //audio2 = persoMenu.audio2;
    }
    private void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
    void Update()
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.forward);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                Camera_mvt.instance.Cam_mvt_up();


            }
            else if (hit.collider.tag == "LaunchServer")
            {
                audio2.PlayOneShot(choc);
                StartServer();
            }
            else if (hit.collider.tag == "RefreshServer")
            {
                audio2.PlayOneShot(choc);
                MasterServer.RequestHostList(TypeName);
                if (hostData != null)
                {
                    CubeJoin.GetComponent<Renderer>().material = mat1;
                }
            }
            else if (hit.collider.tag == "JoinServer")
            {
                audio2.PlayOneShot(choc);
                if (CubeJoin.GetComponent<Renderer>().material == mat1)
                {
                    for (int i = 0, l = hostData.Length; i < l; i++)
                    {
                        JoinServer(hostData[i]);
                    }
                }
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc);
                Application.LoadLevel("Menu0.2");
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.left);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                Camera_mvt.instance.Cam_mvt_left();
                //Light_mvt.instance.light_mvt_left();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "LaunchServer")
            {
                audio2.PlayOneShot(choc);
                StartServer();
            }
            else if (hit.collider.tag == "RefreshServer")
            {
                audio2.PlayOneShot(choc);
                MasterServer.RequestHostList(TypeName);
                if (hostData != null)
                {
                    CubeJoin.GetComponent<Renderer>().material = mat1;
                }
            }
            else if (hit.collider.tag == "JoinServer")
            {
                audio2.PlayOneShot(choc);
                if (CubeJoin.GetComponent<Renderer>().material == mat1)
                {
                    for (int i = 0, l = hostData.Length; i < l; i++)
                    {
                        JoinServer(hostData[i]);
                    }
                }
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc);
                Application.LoadLevel("Menu0.2");
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.back);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                Camera_mvt.instance.Cam_mvt_down();
                //Light_mvt.instance.light_mvt_down();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "LaunchServer")
            {
                audio2.PlayOneShot(choc);
                StartServer();
            }
            else if (hit.collider.tag == "RefreshServer")
            {
                audio2.PlayOneShot(choc);
                MasterServer.RequestHostList(TypeName);
                if (hostData != null)
                {
                    CubeJoin.GetComponent<Renderer>().material = mat1;
                }
            }
            else if (hit.collider.tag == "JoinServer")
            {
                audio2.PlayOneShot(choc);
                if (CubeJoin.GetComponent<Renderer>().material == mat1)
                {
                    for (int i = 0, l = hostData.Length; i < l; i++)
                    {
                        JoinServer(hostData[i]);
                    }
                }
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc);
                Application.LoadLevel("Menu0.2");
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
                audio.PlayOneShot(pas);
                transform.Translate(Vector3.right);
                transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                Camera_mvt.instance.Cam_mvt_right();
                //Light_mvt.instance.light_mvt_right();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "LaunchServer")
            {
                audio2.PlayOneShot(choc);
                StartServer();
            }
            else if (hit.collider.tag == "RefreshServer")
            {
                audio2.PlayOneShot(choc);
                MasterServer.RequestHostList(TypeName);
                if (hostData != null)
                {
                    CubeJoin.GetComponent<Renderer>().material = mat1;
                }
            }
            else if (hit.collider.tag == "JoinServer")
            {
                audio2.PlayOneShot(choc);
                if (hostData != null)
                {
                    for (int i = 0, l = hostData.Length; i < l; i++)
                    {
                        JoinServer(hostData[i]);
                    }

                }
            }
            else if (hit.collider.tag == "Exit2")
            {
                audio2.PlayOneShot(choc);
                Application.LoadLevel("Menu0.2");
            }
        }
    }

    void OnServerInitialized()
    {
        Debug.Log("server lance");
        Application.LoadLevel(levelToLoad);
    }

    void OnMasterServerEvent(MasterServerEvent Event)
    {
        if (Event == MasterServerEvent.HostListReceived)
        {
            Debug.Log("Liste des parties mise à jour");
            hostData = MasterServer.PollHostList();
        }
    }

    public void StartServer()
    {
        if (!Network.isClient && !Network.isServer)
        {
            // Initialisation du serveur pour 4 joueurs max. sur le port 2500.
            Network.InitializeServer(4, 2500, !Network.HavePublicAddress());

            // Enregistrement du serveur avec l'identifiant du jeu et le nom de la partie.
            MasterServer.RegisterHost(TypeName, GameName);
            Debug.Log("Server créé");

        }
    }

    private void JoinServer(HostData gameToJoint)
    {

        // Identificateur unique de la partie
        GameToJoin = gameToJoint;
        Debug.LogError("joinserver ok " + gameToJoint);


        // Chargement du niveau, on se connect au serveur juste après.
        Application.LoadLevel(levelToLoad);
    }
}


