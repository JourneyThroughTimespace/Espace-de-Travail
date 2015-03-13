using UnityEngine;
using System.Collections;

public class Generation_map : MonoBehaviour 
{


    public GameObject brick;
    public GameObject perso;
    public GameObject sol;
    public GameObject exit;

    private void fullBrick()
    {
        for (int y = -20; y < 21; y++)
        {
            for (int x = -20; x < 21; x++)
            {
                Instantiate(brick, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }
    public int longueur = 100;
    public int largeur = 100;
    public int RoomX = 11;
    public int RoomY = 7;

    private void Isaac()
    {
        //int longueur = 100;
        //int largeur = 100;
        char[,] TabMap = new char[longueur, largeur];
        for (int i = 0; i < longueur; i++) // init
        {
            for (int j = 0; j < largeur; j++)
            {
                TabMap[i, j] = 'w';
            }
        }

        //int RoomX = 11;
        //int RoomY = 7;
        int mx = longueur / 2 - (RoomX / 2);
        int my = largeur / 2 - (RoomY / 2);

        for (int i = 0; i < RoomX; i++) // salle central
        {
            for (int j = 0; j < RoomY; j++)
            {
                TabMap[mx + i, my + j] = 'e';
            }
        }
        for (int compt = 0; compt < 75; compt++)
        {
            int pos = Random.Range(1, 5);
            if ((pos == 1) && (my > RoomY + 2)) // salle dessus
            {
                TabMap[mx + (RoomX / 2), my - 1] = 'd';
                my = my - (RoomY + 1);
                for (int i = 0; i < RoomX; i++)
                {
                    for (int j = 0; j < RoomY; j++)
                    {
                        TabMap[mx + i, my + j] = 'e';
                    }
                }
            }

            if ((pos == 2) && (mx < longueur - (2 * RoomX + 1))) // salle droite
            {
                TabMap[mx + RoomX, my + RoomY / 2] = 'd';
                mx = mx + RoomX + 1;
                for (int i = 0; i < RoomX; i++)
                {
                    for (int j = 0; j < RoomY; j++)
                    {
                        TabMap[mx + i, my + j] = 'e';
                    }
                }
            }

            if ((pos == 3) && (my < largeur - (2 * RoomY + 1))) // salle dessous
            {
                TabMap[mx + RoomX / 2, my + RoomY] = 'd';
                my = my + RoomY + 1;
                for (int i = 0; i < RoomX; i++)
                {
                    for (int j = 0; j < RoomY; j++)
                    {
                        TabMap[mx + i, my + j] = 'e';
                    }
                }
            }

            if ((pos == 4) && (mx > RoomX + 1)) // salle gauche
            {
                TabMap[mx - 1, my + RoomY / 2] = 'd';
                mx = mx - (RoomX + 1);
                for (int i = 0; i < RoomX; i++)
                {
                    for (int j = 0; j < RoomY; j++)
                    {
                        TabMap[mx + i, my + j] = 'e';
                    }
                }
            }
        }
        int px = longueur/2;
        int py = largeur/2;
        TabMap[px, py] = 'p'; // placement personnage
        //int pos = Random.Range(1, 5);
        bool b = true;
        while (b)
        {
            int posx = Random.Range(1, longueur-1);
            int posy = Random.Range(1, largeur - 1);
            if(TabMap[posx,posy] == 'e')
            {
                TabMap[posx, posy] = 's';
                b = false;
            }
        }
        /*b = true;
        while (b)
        {
            int posx = Random.Range(1, longueur - 1);
            int posy = Random.Range(1, largeur - 1);
            if (TabMap[posx, posy] == 'e')
            {
                TabMap[posx, posy] = 'p';
                b = false;
            }
        }*/



            for (int i = 0; i < longueur; i++) // creation map
            {
                for (int j = 0; j < largeur; j++)
                {
                    Instantiate(sol, new Vector3(i, -0.5f, j), Quaternion.identity);
                    if (TabMap[i, j] == 'w')
                    {
                        Instantiate(brick, new Vector3(i, 0, j), Quaternion.identity);
                    }
                    if (TabMap[i, j] == 'p')
                    {
                        Instantiate(perso, new Vector3(i, 0, j), Quaternion.identity);
                    }
                    if (TabMap[i, j] == 's')
                    {
                        Instantiate(exit, new Vector3(i, 0, j), Quaternion.identity);
                    }
                }
            }
    }

	// Use this for initialization
	void Start () 
    {
        Isaac();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("Menu0.1");
        }
	}
}
