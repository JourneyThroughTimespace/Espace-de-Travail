using UnityEngine;
using System.Collections;

public class _BoardDiadact : MonoBehaviour 
{
    //public GameObject player;
    public GameObject brick;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject sol;
    public GameObject sortie;
    public GameObject camera;
    public GameObject light;
    public GameObject player;

    private int game_State;



    _Enemy en;
    _Player p1;

    public static _BoardDiadact instance;

	void Start () 
    {
        map(_GameManager.niveau);
        instance = this;
        game_State = 0;
        //DontDestroyOnLoad(transform.gameObject);
	}
   

    public void next_level()
    {
        Application.LoadLevel("Didactitiel");
    }

    public void map(int n)
    {
        if (n == 2)
        {
            _GameManager.niveau = 0;
            _GameManager.instance.player.set_life(10); // changement verif si marche encore
            //Destroy(gameObject);
            Application.LoadLevel("Menu0.2");
        }
        else
        {
            for (int y = 0; y < 21; y++)
            {
                for (int x = 0; x < 21; x++)
                {

                    if (x == 19 && y == 20)
                    {
                        Instantiate(sortie, new Vector3(x, 0, y), Quaternion.Euler(0, 90, 0));
                    }
                    else if (x == 0 || x == 20 || y == 0 || y == 20)
                    {
                        Instantiate(brick, new Vector3(x, 0, y), Quaternion.identity);
                    }
                }
            }
            Instantiate(sol, new Vector3(10, -0.5f, 10), Quaternion.identity);

            

            if (n == 0)
            {
                en = ((GameObject)Instantiate(enemy1, new Vector3(10, 0, 17), Quaternion.identity)).GetComponent<_Enemy>();
                p1 = ((GameObject)Instantiate(player, new Vector3(10, 0, 3), Quaternion.identity)).GetComponent<_Player>();
                p1.set_life(_GameManager.instance.player.get_life());
            }
            else
            {
                //p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<_Player>();
                p1 = ((GameObject)Instantiate(player, new Vector3(10, 0, 3), Quaternion.identity)).GetComponent<_Player>();
                p1.set_life(_GameManager.instance.player.get_life());
                _GameManager.instance.player = p1;
                en = ((GameObject)Instantiate(enemy2, new Vector3(10, 0, 17), Quaternion.identity)).GetComponent<_Enemy>();
            }
            light.transform.Translate(0, 0, 0);
        }
    }

	void Update () 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _GameManager.instance.player.set_life(10);
            Application.LoadLevel("Menu0.2");
        }
        if (game_State % 3 <= 1)
        {
            p1.turnUpdate();
        }
        if (game_State % 3 == 2)
        {
            en.turnUpdate(p1.transform);
            change_State();
        }
	}

    public void change_State()
    {
        game_State++;
    }

}
