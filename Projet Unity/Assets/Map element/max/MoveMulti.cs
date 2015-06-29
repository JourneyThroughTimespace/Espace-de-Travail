using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveMulti : MonoBehaviour
{

    public static MoveMulti instance;
    //public bool move = false;
    //  public int life = 100;
    // public int dmg = 50;
    //public int range = 1;
    // public AudioClip pas;
    // public AudioClip choc;
    // AudioSource audio;
    // AudioSource audio2;
    // public GameObject canvas;
    // public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    //  public float flashSpeed = 5f;
    //public Image damageImage;
    //  private Slider healthSlider;
    //network
    private Transform transform;
    private NetworkView ntView;
    NetworkMessageInfo info;
    public int tour;
    private int index;



    void Start()
    {
        this.index = GameObject.Find("Level").GetComponent<LevelManager>().index;
        Debug.Log("je suis tamere");
        instance = this;
        transform = GetComponent<Transform>();
        ntView = GetComponent<NetworkView>();
        Debug.Log("je suis tamere");
        if (ntView.isMine)
            Debug.Log("New object instanted by me");
        else
            Debug.Log("New object instantiated by " + info.sender);
        Debug.LogError("index =" + index);

    }

    private void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    [RPC]
    private void ChangeTour(int vachette)
    {
        Debug.Log("changetour lance avec " + this.tour);

        this.tour = vachette;

        Debug.Log("change tour fini avec " + this.tour);
    }

    void Update()
    {
        Debug.LogError("index =" + index + "tour =" + tour);
        if (this.index == tour)
        {
            Debug.LogError("tu veux voir ma teub?");
            Debug.Log("tour client" + tour);
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
                    if (ntView.isMine)
                    {
                        transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y, 0);
                        transform.Translate(Vector3.forward);
                    }
                    if (tour == 1)
                    {
                        tour = 0;
                    }
                    else if (tour == 0)
                    {
                        tour = 1;
                    }
                    Debug.LogError("tour = " + tour);
                    ntView.RPC("ChangeTour", RPCMode.Others, tour);


                }
                Debug.Log(tour);
            }
            if (Input.GetKeyDown("a"))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, lef, out hit, 1);
                if (!Physics.Raycast(transform.position, lef, 1))
                {

                    if (ntView.isMine)
                    {
                        transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y - 90, 0);
                        transform.Translate(Vector3.left);
                    }
                    if (tour == 1)
                    {
                        tour = 0;
                    }
                    else if (tour == 0)
                    {
                        tour = 1;
                    }
                    ntView.RPC("ChangeTour", RPCMode.Others, tour);
                }
                Debug.Log(tour);
            }
            if (Input.GetKeyDown("s"))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, bac, out hit, 1);
                if (!Physics.Raycast(transform.position, bac, 1))
                {
                    if (ntView.isMine)
                    {
                        transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 180, 0);
                        transform.Translate(Vector3.back);
                    }
                    if (tour == 1)
                    {
                        tour = 0;
                    }
                    else if (tour == 0)
                    {
                        tour = 1;
                    }
                    ntView.RPC("ChangeTour", RPCMode.Others, tour);
                }
                Debug.Log(tour);
            }
            if (Input.GetKeyDown("d"))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, rig, out hit, 1);
                if (!Physics.Raycast(transform.position, rig, 1))
                {
                    if (ntView.isMine)
                    {
                        transform.GetChild(0).Rotate(0, -transform.GetChild(0).eulerAngles.y + 90, 0);
                        transform.Translate(Vector3.right);
                    }
                    if (tour == 1)
                    {
                        tour = 0;
                    }
                    else if (tour == 0)
                    {
                        tour = 1;
                    }
                    ntView.RPC("ChangeTour", RPCMode.Others, tour);
                }
            }
        }
    }
}