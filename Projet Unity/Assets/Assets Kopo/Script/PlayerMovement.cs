using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public bool move = false;
    // Use this for initialization
    void Start()
    {
        instance = this;
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
        if (Input.GetKeyDown("w"))// && !Physics.Raycast(transform.position, fwd, 1))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, fwd, out hit, 1);
            if (!Physics.Raycast(transform.position, fwd, 1))
            {
                transform.Translate(Vector3.forward);
                have_move(true);
                //Camera_mvt.instance.Cam_mvt_up(transform);
                //Light_mvt.instance.light_mvt_up();

                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                NextLevel();
            }
            else if (hit.collider.tag == "blocking")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        if (Input.GetKeyDown("a"))// && !Physics.Raycast(transform.position, lef, 1))
        {
            //transform.Translate(Vector3.left);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, lef, out hit, 1);
            if (!Physics.Raycast(transform.position, lef, 1))
            {
                transform.Translate(Vector3.left);
                have_move(true);
                //Camera_mvt.instance.Cam_mvt_left();
                //Light_mvt.instance.light_mvt_left();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                NextLevel();
            }
            else if (hit.collider.tag == "blocking")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        if (Input.GetKeyDown("s"))// && !Physics.Raycast(transform.position, bac, 1))
        {
            //transform.Translate(Vector3.back);
            //Map_didact.instance.change_state(M_didact.instance.game_state + 1);
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, bac, out hit, 1);
            if (!Physics.Raycast(transform.position, bac, 1))
            {
                transform.Translate(Vector3.back);
                have_move(true);
                //Camera_mvt.instance.Cam_mvt_down();
                //Light_mvt.instance.light_mvt_down();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                NextLevel();
            }
            else if (hit.collider.tag == "blocking")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        if (Input.GetKeyDown("d"))// && !Physics.Raycast(transform.position, rig, 1))
        {
            //transform.Translate(Vector3.right);
            //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            RaycastHit hit;
            bool b = Physics.Raycast(transform.position, rig, out hit, 1);
            if (!Physics.Raycast(transform.position, rig, 1))
            {
                transform.Translate(Vector3.right);
                have_move(true);
                //Camera_mvt.instance.Cam_mvt_right();
                //Light_mvt.instance.light_mvt_right();
                //Map_didact.instance.change_state(Map_didact.instance.game_state + 1);
            }
            else if (hit.collider.tag == "Exit")
            {
                NextLevel();
            }
            else if (hit.collider.tag == "blocking")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void have_move(bool b)
    {
        move = b;
    }


}