using UnityEngine;
using System.Collections;

public class Map_didact : MonoBehaviour {

    public static Map_didact instance;

    public GameObject brick;
    public GameObject perso;
    public GameObject enemy;
    public GameObject sol;

    didact_Player p1;
    didact_Enemy en;

    private int game_state = 0;

    void Awake()
    {
        instance = this;
    }
    private void Map()
    {
        for (int y = -10; y < 11; y++)
        {
            for (int x = -10; x < 11; x++)
            {
                Instantiate(sol, new Vector3(x, -0.5f, y), Quaternion.identity);
                if (x == -10 || x == 10 || y == -10 || y == 10)
                {
                    Instantiate(brick, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
        p1 = ((GameObject)Instantiate(perso, new Vector3(0, 0, -7), Quaternion.identity)).GetComponent<didact_Player>();
        en = ((GameObject)Instantiate(enemy, new Vector3(0, 0, 7), Quaternion.identity)).GetComponent<didact_Enemy>(); ;
    }

    public void change_state(int t)
    {
        game_state = t;
    }

    void Start()
    {
        Map();
    }

    void Update()
    {
        if (game_state == 0)
        {
            p1.TurnUpdate();
        }
        if (game_state == 1)
        {
            en.TurnUpdate();
        }
    }
}
