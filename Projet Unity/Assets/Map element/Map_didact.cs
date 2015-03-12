using UnityEngine;
using System.Collections;

public class Map_didact : MonoBehaviour
{
    public static Map_didact instance;

    public GameObject brick;
    public GameObject perso;
    public GameObject enemy;
    public GameObject sol;
    public GameObject sortie;

    didact_Player p1;
    didact_Enemy en;

    public int game_state = 0;

    void Awake()
    {
        instance = this;
    }
    private void Map()
    {
        for (int y = 0; y < 21; y++)
        {
            for (int x = 0; x < 21; x++)
            {
                Instantiate(sol, new Vector3(x, -0.5f, y), Quaternion.identity);
                if (x == 19 && y == 20)
                {
                    Instantiate(sortie, new Vector3(x, 0, y), Quaternion.identity);
                }
                else if (x == 0 || x == 20 || y == 0 || y == 20)
                {
                    Instantiate(brick, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
        p1 = ((GameObject)Instantiate(perso, new Vector3(10, 0, 3), Quaternion.identity)).GetComponent<didact_Player>();
        en = ((GameObject)Instantiate(enemy, new Vector3(10, 0, 17), Quaternion.identity)).GetComponent<didact_Enemy>(); ;
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
        if (game_state == 0 || game_state == 1)
        {
            p1.TurnUpdate();
        }
        if (game_state == 2)
        {
            en.TurnUpdate(p1.transform);
        }
    }
}
