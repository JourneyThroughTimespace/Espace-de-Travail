using UnityEngine;
using System.Collections;

public class Generation_map : MonoBehaviour 
{


    public GameObject brick;
    public GameObject perso;
    public GameObject sol;

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

    private void Isaac()
    {
        int longueur = 41;
        int largeur = 41;
        char[,] TabMap = new char[longueur, largeur];
        for (int i = 0; i < longueur; i++) // init
        {
            for (int j = 0; j < largeur; j++)
            {
                TabMap[i, j] = 'w';
            }
        }

        int RoomX = 5;
        int RoomY = 3;
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
        int px = 20;
        int py = 20;
        TabMap[px, py] = 'p'; // placement personnage
        


        for (int i = 0; i < longueur; i++) // creation map
        {
            for (int j = 0; j < largeur; j++)
            {
                Instantiate(sol, new Vector3(i - 20, -0.5f, j - 20), Quaternion.identity);
                if (TabMap[i, j] == 'w')
                {
                    Instantiate(brick, new Vector3(i - 20, 0, j - 20), Quaternion.identity);
                }
                if (TabMap[i, j] == 'p')
                {
                    Instantiate(perso, new Vector3(i - 20, 0, j - 20), Quaternion.identity);
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
	
	}
}
