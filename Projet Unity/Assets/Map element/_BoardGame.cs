using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Random = UnityEngine.Random;

public class _BoardGame : MonoBehaviour 
{
    public static _BoardGame instance;

    public GameObject[] brick;
    public GameObject perso;
    public GameObject sol;
    public GameObject exit;
    public GameObject[] decor;
    public GameObject camera;
    public GameObject[] enemy;


    //public GameObject light;
    public Transform li;

    _MapPlayer p1;
    List<_Enemy> enemies = new List<_Enemy>();

    public int game_State;

    public int min_room_size = 5;
    public int max_room_size = 10;
    public int dungeon_length = 50;
    public int dungeon_width = 50;
    public int density_room = 80;

    private char[,] solution;
    public char[,] gameBoard;

    private void a_maze_ing(int n)
    {
        List<List<int>> rooms = new List<List<int>>(); //utile pour instance de debut et fin



        // Board Generation
        char[,] board = new char[dungeon_width, dungeon_length];
        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                board[i, j] = 'w';
            }
        }


        // Room Generation
        for (int t = 0; t < density_room; t++)
        {
            int pos_x = Random.Range(1, dungeon_width);
            int pos_y = Random.Range(1, dungeon_length);
            int room_x = Random.Range(min_room_size, max_room_size + 1);
            int room_y = Random.Range(min_room_size, max_room_size + 1);
            bool empty_space = true;
            for (int i = pos_x - 1; i < pos_x + room_x + 1; i++)   // verif place
            {
                for (int j = pos_y - 1; j < pos_y + room_y + 1; j++)
                {
                    if ((pos_y + room_y >= dungeon_length) || (pos_x + room_x >= dungeon_width))
                    {
                        empty_space = false;
                        break;
                    }
                    else if (((i != pos_x - 1) && ((j != pos_y - 1) || (j != pos_y + room_y + 1))) || ((i != pos_x + room_x + 1) && ((j != pos_y - 1) || (j != pos_y + room_y + 1))))
                    {
                        if ((board[i, j] == 'r') || (board[i, j] == 'm'))
                        {
                            empty_space = false;
                            break;
                        }
                    }
                }
                if (!empty_space) break;
            }
            if (empty_space)
            {
                for (int i = pos_x; i < pos_x + room_x; i++)   // si espace libre place room
                {
                    for (int j = pos_y; j < pos_y + room_y; j++)
                    {
                        if ((i == pos_x + (room_x / 2)) && (j == pos_y + (room_y / 2)))
                        {
                            rooms.Add(new List<int> { i, j });
                        }
                        board[i, j] = 'r';
                    }
                }
            }
        }


        // placement sortie
        List<int> sortie = rooms[Random.Range(0, rooms.Count)];
        while (board[sortie.ElementAt(0), sortie.ElementAt(1)] == 'r')
        {
            sortie[1]++;
        }
        board[sortie.ElementAt(0), sortie.ElementAt(1)] = 'o';
        board[sortie.ElementAt(0) - 1, sortie.ElementAt(1)] = 'o';
        board[sortie.ElementAt(0) + 1, sortie.ElementAt(1)] = 'o';

        //position perso
        List<int> per = rooms[Random.Range(0, rooms.Count)];
        board[per[0], per[1]] = 'p';

        //position enemy
        List<List<int>> enem = new List<List<int>>();
        int nb_enemy = 0;
        while(nb_enemy < (n*n + 1))
        {
            int x = Random.Range(1, (dungeon_width - 1));
            int y = Random.Range(1, (dungeon_length - 1));
            if (board[x, y] == 'r')
            {
                enem.Add(new List<int> { x, y });
                board[x, y] = 'e';
                nb_enemy++;
            }
        }

        // position decor
        int nb_element = 0;
        List<List<int>> dec = new List<List<int>>();
        while (nb_element < 20)
        {
            int x = Random.Range(0, dungeon_width);
            int y = Random.Range(0, dungeon_length);
            if (board[x, y] == 'r')
            {
                dec.Add(new List<int> { x, y });
                nb_element++;
            }
        }

        // clean board
        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                if(board[i,j] == 'e' || board[i,j] == 'p')
                {
                    board[i, j] = 'r';
                }
            }
        }



            // Maze Generation
            for (int i = 1; i < dungeon_width - 1; i++)
            {
                for (int j = 1; j < dungeon_length - 1; j++)
                {
                    if (board[i, j] == 'w')
                    {
                        if (((board[i - 1, j] == 'w') && (board[i + 1, j] == 'w') && (board[i, j - 1] == 'w') && (board[i, j + 1] == 'w')))
                        {
                            //exposed_Square.Add(new List<int> { i, j });
                            board[i, j] = 't';
                            List<List<int>> exposed_Square = new List<List<int>>();
                            List<List<int>> temp_maze = new List<List<int>>();
                            temp_maze.Add(new List<int> { i, j });
                            if (i < dungeon_width - 2 && verif(board, i + 1, j))
                            {
                                exposed_Square.Add(new List<int> { i + 1, j });
                            }
                            if (i > 1 && verif(board, i - 1, j))
                            {
                                exposed_Square.Add(new List<int> { i - 1, j });
                            }
                            if (j > 1 && verif(board, i, j - 1))
                            {
                                exposed_Square.Add(new List<int> { i, j - 1 });
                            }
                            if (j < dungeon_length - 2 && verif(board, i, j + 1))
                            {
                                exposed_Square.Add(new List<int> { i, j + 1 });
                            }
                            while (exposed_Square.Count != 0)
                            {
                                int pos = exposed_Square.Count - Random.Range(0, 3) - 1;
                                if (pos < 0)
                                {
                                    pos = exposed_Square.Count - 1;
                                }
                                List<int> coord = exposed_Square[pos];
                                if (verif(board, coord.ElementAt(0), coord.ElementAt(1)))
                                {
                                    board[coord.ElementAt(0), coord.ElementAt(1)] = 't';
                                    temp_maze.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) });
                                    if (coord.ElementAt(0) < dungeon_width - 2 && verif(board, coord.ElementAt(0) + 1, coord.ElementAt(1)))
                                    {
                                        exposed_Square.Add(new List<int> { coord.ElementAt(0) + 1, coord.ElementAt(1) });
                                    }
                                    if (coord.ElementAt(0) > 1 && verif(board, coord.ElementAt(0) - 1, coord.ElementAt(1)))
                                    {
                                        exposed_Square.Add(new List<int> { coord.ElementAt(0) - 1, coord.ElementAt(1) });
                                    }
                                    if (coord.ElementAt(1) > 1 && verif(board, coord.ElementAt(0), coord.ElementAt(1) - 1))
                                    {
                                        exposed_Square.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) - 1 });
                                    }
                                    if (coord.ElementAt(1) < dungeon_length - 2 && verif(board, coord.ElementAt(0), coord.ElementAt(1) + 1))
                                    {
                                        exposed_Square.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) + 1 });
                                    }
                                }
                                exposed_Square.RemoveAt(pos);
                            }


                            if (temp_maze.Count > 3)
                            {
                                foreach (List<int> maze in temp_maze)
                                {
                                    board[maze.ElementAt(0), maze.ElementAt(1)] = 'm';
                                }
                            }
                            else
                            {
                                foreach (List<int> maze in temp_maze)
                                {
                                    board[maze.ElementAt(0), maze.ElementAt(1)] = 'w';
                                }
                            }
                        }
                    }
                }
            }



        // connecteurs
        List<List<int>> connections = new List<List<int>>();
        for (int i = 1; i < dungeon_width - 1; i++)
        {
            for (int j = 1; j < dungeon_length - 1; j++)
            {
                if (board[i, j] == 'w')
                {
                    if (!(board[i - 1, j] == 'm' && board[i - 1, j - 1] == 'm' && board[i, j - 1] == 'm'))
                    {
                        if (!(board[i - 1, j] == 'm' && board[i - 1, j + 1] == 'm' && board[i, j + 1] == 'm'))
                        {
                            if (!(board[i, j + 1] == 'm' && board[i + 1, j + 1] == 'm' && board[i + 1, j] == 'm'))
                            {
                                if (!(board[i, j - 1] == 'm' && board[i + 1, j - 1] == 'm' && board[i + 1, j] == 'm'))
                                {
                                    int room = 0;
                                    int maze = 0;
                                    for (int x = i - 1; x < i + 2; x++)
                                    {
                                        if (board[x, j] == 'r')
                                        {
                                            room++;
                                        }
                                        if (board[x, j] == 'm')
                                        {
                                            maze++;
                                        }
                                    }
                                    for (int y = j - 1; y < j + 2; y++)
                                    {
                                        if (board[i, y] == 'r')
                                        {
                                            room++;
                                        }
                                        if (board[i, y] == 'm')
                                        {
                                            maze++;
                                        }
                                    }
                                    if (room == 2 || (room == 1 && maze > 0))
                                    {
                                        connections.Add(new List<int> { i, j });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        List<int> start_room = rooms.ElementAt(Random.Range(0, rooms.Count));
        int a = start_room.ElementAt(0);
        int b = start_room.ElementAt(1);
        int x_min = a;
        while (board[x_min - 1, b] == 'r')
        {
            x_min--;
        }
        int x_max = a;
        while (board[x_max + 1, b] == 'r')
        {
            x_max++;
        }
        int y_min = b;
        while (board[a, y_min - 1] == 'r')
        {
            y_min--;
        }
        int y_max = b;
        while (board[a, y_max + 1] == 'r')
        {
            y_max++;
        }
        for (int i = x_min; i <= x_max; i++)
        {
            for (int j = y_min; j <= y_max; j++)
            {
                board[i, j] = 'e';
            }
        }

        while (connections.Count != 0)
        {
            List<List<int>> remplir_room = new List<List<int>>();
            List<List<int>> remplir_maze = new List<List<int>>();
            int pos = Random.Range(0, connections.Count);
            int x = connections[pos].ElementAt(0);
            int y = connections[pos].ElementAt(1);
            bool connect = false;
            if (board[x - 1, y] == 'e' || board[x - 1, y] == 'l')
            {
                if (board[x + 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }
                if (board[x, y - 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x, y + 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }

                if (board[x + 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }
                if (board[x, y - 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x, y + 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }
                connect = true;
            }

            if (board[x + 1, y] == 'e' || board[x + 1, y] == 'l')
            {
                if (board[x - 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x, y - 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x, y + 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }

                if (board[x - 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x, y - 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x, y + 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }
                connect = true;
            }

            if (board[x, y - 1] == 'e' || board[x, y - 1] == 'l')
            {
                if (board[x, y + 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }
                if (board[x - 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x + 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }

                if (board[x, y + 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y + 1 });
                    board[x, y] = 's';
                }
                if (board[x - 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x + 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }
                connect = true;
            }

            if (board[x, y + 1] == 'e' || board[x, y + 1] == 'l')
            {
                if (board[x, y - 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x - 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x + 1, y] == 'm')
                {
                    remplir_maze.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }

                if (board[x, y - 1] == 'r')
                {
                    remplir_room.Add(new List<int> { x, y - 1 });
                    board[x, y] = 's';
                }
                if (board[x - 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x - 1, y });
                    board[x, y] = 's';
                }
                if (board[x + 1, y] == 'r')
                {
                    remplir_room.Add(new List<int> { x + 1, y });
                    board[x, y] = 's';
                }
                connect = true;
            }



            while (remplir_maze.Count != 0)
            {
                List<int> coord = remplir_maze[0];
                board[coord.ElementAt(0), coord.ElementAt(1)] = 'l';
                if (board[coord.ElementAt(0) + 1, coord.ElementAt(1)] == 'm')
                {
                    remplir_maze.Add(new List<int> { coord.ElementAt(0) + 1, coord.ElementAt(1) });
                }
                if (board[coord.ElementAt(0) - 1, coord.ElementAt(1)] == 'm')
                {
                    remplir_maze.Add(new List<int> { coord.ElementAt(0) - 1, coord.ElementAt(1) });
                }
                if (board[coord.ElementAt(0), coord.ElementAt(1) - 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) - 1 });
                }
                if (board[coord.ElementAt(0), coord.ElementAt(1) + 1] == 'm')
                {
                    remplir_maze.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) + 1 });
                }
                remplir_maze.RemoveAt(0);
            }

            while (remplir_room.Count != 0)
            {
                List<int> coord = remplir_room[0];
                board[coord.ElementAt(0), coord.ElementAt(1)] = 'e';
                if (board[coord.ElementAt(0) + 1, coord.ElementAt(1)] == 'r')
                {
                    remplir_room.Add(new List<int> { coord.ElementAt(0) + 1, coord.ElementAt(1) });
                }
                if (board[coord.ElementAt(0) - 1, coord.ElementAt(1)] == 'r')
                {
                    remplir_room.Add(new List<int> { coord.ElementAt(0) - 1, coord.ElementAt(1) });
                }
                if (board[coord.ElementAt(0), coord.ElementAt(1) - 1] == 'r')
                {
                    remplir_room.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) - 1 });
                }
                if (board[coord.ElementAt(0), coord.ElementAt(1) + 1] == 'r')
                {
                    remplir_room.Add(new List<int> { coord.ElementAt(0), coord.ElementAt(1) + 1 });
                }
                remplir_room.RemoveAt(0);
            }
            if (connect)
            {
                connections.RemoveAt(pos);
            }
        }



        // unperfect maze
        int connec = 0;
        while (connec < 10)
        {
            bool succes_connec = true;
            while (succes_connec) // ajout compteur option
            {
                int i = Random.Range(1, dungeon_width - 1);
                int j = Random.Range(1, dungeon_length - 1);
                if (board[i, j] == 'w')
                {
                    if (!((board[i - 1, j] == 'l' || board[i - 1, j] == 's') &&
                        (board[i - 1, j - 1] == 'l' || board[i - 1, j - 1] == 's') &&
                        (board[i, j - 1] == 'l' || board[i, j - 1] == 's')))
                    {
                        if (!((board[i - 1, j] == 'l' || board[i - 1, j] == 's') &&
                            (board[i - 1, j + 1] == 'l' || board[i - 1, j + 1] == 's') &&
                            (board[i, j + 1] == 'l' || board[i, j + 1] == 's')))
                        {
                            if (!((board[i, j + 1] == 'l' || board[i, j + 1] == 's') &&
                                (board[i + 1, j + 1] == 'l' || board[i + 1, j + 1] == 's') &&
                                (board[i + 1, j] == 'l' || board[i + 1, j] == 's')))
                            {
                                if (!((board[i, j - 1] == 'l' || board[i, j - 1] == 's') &&
                                    (board[i + 1, j - 1] == 'l' || board[i + 1, j - 1] == 's') &&
                                    (board[i + 1, j] == 'l' || board[i + 1, j] == 's')))
                                {
                                    board[i, j] = 'l';
                                    succes_connec = false;
                                    connec++;
                                }
                            }
                        }
                    }
                }
            }
        }


        // reduce dead-ends
        List<List<int>> dead_ends = new List<List<int>>();
        for (int i = 1; i < dungeon_width - 1; i++)
        {
            for (int j = 1; j < dungeon_length - 1; j++)
            {
                if (board[i, j] == 'l')
                {
                    int compte = 0;
                    if (board[i - 1, j] == 'w') compte++;
                    if (board[i + 1, j] == 'w') compte++;
                    if (board[i, j - 1] == 'w') compte++;
                    if (board[i, j + 1] == 'w') compte++;
                    if (compte == 3)
                    {
                        dead_ends.Add(new List<int> { i, j });
                    }
                }
            }
        }
        while (dead_ends.Count > 10)
        {
            int pos = Random.Range(0, dead_ends.Count);
            List<int> coord = dead_ends[pos];
            int i = coord.ElementAt(0);
            int j = coord.ElementAt(1);
            board[i, j] = 'w';
            if (board[i + 1, j] == 'l')
            {
                int compte = 0;
                if (board[i, j] == 'w') compte++;
                if (board[i + 2, j] == 'w') compte++;
                if (board[i + 1, j - 1] == 'w') compte++;
                if (board[i + 1, j + 1] == 'w') compte++;
                if (compte == 3)
                {
                    dead_ends.Add(new List<int> { i + 1, j });
                }
            }
            else if (board[i - 1, j] == 'l')
            {
                int compte = 0;
                if (board[i - 2, j] == 'w') compte++;
                if (board[i, j] == 'w') compte++;
                if (board[i - 1, j - 1] == 'w') compte++;
                if (board[i - 1, j + 1] == 'w') compte++;
                if (compte == 3)
                {
                    dead_ends.Add(new List<int> { i - 1, j });
                }
            }
            else if (board[i, j + 1] == 'l')
            {
                int compte = 0;
                if (board[i, j + 2] == 'w') compte++;
                if (board[i, j] == 'w') compte++;
                if (board[i + 1, j + 1] == 'w') compte++;
                if (board[i - 1, j + 1] == 'w') compte++;
                if (compte == 3)
                {
                    dead_ends.Add(new List<int> { i, j + 1 });
                }
            }
            else if (board[i, j - 1] == 'l')
            {
                int compte = 0;
                if (board[i, j - 2] == 'w') compte++;
                if (board[i, j] == 'w') compte++;
                if (board[i + 1, j - 1] == 'w') compte++;
                if (board[i - 1, j - 1] == 'w') compte++;
                if (compte == 3)
                {
                    dead_ends.Add(new List<int> { i, j - 1 });
                }
            }
            dead_ends.RemoveAt(pos);
        }

        for (int i = 1; i < dungeon_width - 1; i++)
        {
            for (int j = 0; j < dungeon_length - 1; j++)
            {
                if (board[i, j] == 'e' || board[i, j] == 'l')
                {
                    if (board[i + 1, j] == 'w' && board[i - 1, j] == 'w' && board[i, j - 1] == 'w' && board[i, j + 1] == 'w')
                    {
                        board[i, j] = 'w';
                    }
                }
            }
        }

        board[sortie.ElementAt(0) - 1, sortie.ElementAt(1)] = 'w';
        board[sortie.ElementAt(0) + 1, sortie.ElementAt(1)] = 'w';

        //int perso_nb = Random.Range(0, rooms.Count);
        //List<int> per = rooms[Random.Range(0, rooms.Count)];
        //rooms.RemoveAt(perso_nb);
        board[per.ElementAt(0), per.ElementAt(1)] = 'p';

        foreach (List<int> en in enem)
        {
            board[en.ElementAt(0), en.ElementAt(1)] = 'a';
        }

        board[sortie[0], sortie[1]] = 'o';
        // Affichage

        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                Instantiate(sol, new Vector3(i, -0.5f, j), Quaternion.identity);
                if (board[i, j] == 'w')
                {
                    Instantiate(brick[Random.Range(0, brick.Length)], new Vector3(i, 0, j), Quaternion.identity);
                }
                if (board[i, j] == 'o')
                {
                    Instantiate(exit, new Vector3(i, 0, j), Quaternion.Euler(0, 90, 0));
                }
            }
        }
        //Instantiate(camera, new Vector3(per.ElementAt(0), 6, per.ElementAt(1) - 4), Quaternion.Euler(60, 0, 0));
        if (n == 0)
        {
            p1 = ((GameObject)Instantiate(perso, new Vector3(per.ElementAt(0), 0, per.ElementAt(1)), Quaternion.identity)).GetComponent<_MapPlayer>();
            Instantiate(camera, new Vector3(per.ElementAt(0), 6, per.ElementAt(1) - 4), Quaternion.Euler(60, 0, 0));
            //camera.transform.Translate(per.ElementAt(0), 6, per.ElementAt(1) - 4);
        }
        else
        {
            p1 = ((GameObject)Instantiate(perso, new Vector3(per.ElementAt(0), 0, per.ElementAt(1)), Quaternion.identity)).GetComponent<_MapPlayer>();
            //camera.transform.Translate(per.ElementAt(0) - camera.transform.position.x, 6, per.ElementAt(1) - camera.transform.position.z);
            Instantiate(camera, new Vector3(per.ElementAt(0), 6, per.ElementAt(1) - 4), Quaternion.Euler(60, 0, 0));
            p1.set_life(_GameManager.instance.player.get_life());
            p1.l = _GameManager.instance.player.GetComponent<_MapPlayer>().l;
            p1.set_dmg(_GameManager.instance.player.get_dmg());
            p1.d = _GameManager.instance.player.GetComponent<_MapPlayer>().d;
            p1.set_range(_GameManager.instance.player.get_range());
            p1.r = _GameManager.instance.player.GetComponent<_MapPlayer>().r;
            p1.currentWeaponName = _GameManager.instance.player.GetComponent<_MapPlayer>().currentWeaponName;
        }
        li.transform.Rotate(5, 0, 0);
        li.transform.Translate(per.ElementAt(0), per.ElementAt(1) + 2, 0);
        li.transform.Rotate(-5, 0, 0);
        

        foreach (List<int> en in enem)
        {
            enemies.Add(((GameObject)Instantiate(enemy[Random.Range(0, enemy.Length)], new Vector3(en[0], 0, en[1]), Quaternion.identity)).GetComponent<_Enemy>());
        }

        board[sortie[0], sortie[1]] = 'w';
        foreach (List<int> deco in dec)
        {
            if (!(board[deco.ElementAt(0) - 1, deco.ElementAt(1)] == 's' || board[deco.ElementAt(0) + 1, deco.ElementAt(1)] == 's' ||
                board[deco.ElementAt(0), deco.ElementAt(1) - 1] == 's' || board[deco.ElementAt(0), deco.ElementAt(1) + 1] == 's'))
            {
                if (board[deco.ElementAt(0), deco.ElementAt(1)] == 'e')
                {
                    Instantiate(decor[Random.Range(0, decor.Length)], new Vector3(deco.ElementAt(0), 0, deco.ElementAt(1)), Quaternion.identity);
                    board[deco.ElementAt(0), deco.ElementAt(1)] = 'd';
                }
            }
        }
        gameBoard = board;

        if(board[per.ElementAt(0), per.ElementAt(1)] == 'o')
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public string solve(Transform player, Transform enemy, int range)
    {
        int en_x = (int)enemy.transform.position.x;
        int en_y = (int)enemy.transform.position.z;
        int pl_x = (int)player.transform.position.x;
        int pl_y = (int)player.transform.position.z;


        string[,] temp_board = new string[dungeon_width, dungeon_length];
        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                if (gameBoard[i, j] == 'w' || gameBoard[i, j] == 'd')
                {
                    temp_board[i, j] = "w";
                }
                else
                {
                    temp_board[i, j] = "e";
                }
            }
        }
        temp_board[en_x, en_y] = "o";

        int distance = 0;
        List<List<int>> current = new List<List<int>>();
        current.Add(new List<int> { pl_x, pl_y });
        List<List<int>> next = new List<List<int>>();
        bool solution = false;
        while (!solution)
        {
            while (current.Count != 0)
            {
                int x = current[0].ElementAt(0);
                int y = current[0].ElementAt(1);
                if (temp_board[x, y] == "o")
                {
                    solution = true;
                    break;
                }
                else if (temp_board[x, y] == "e")
                {
                    temp_board[x, y] = Convert.ToString(distance);
                    next.Add(new List<int> { x + 1, y });
                    next.Add(new List<int> { x - 1, y });
                    next.Add(new List<int> { x, y + 1 });
                    next.Add(new List<int> { x, y - 1 });
                }
                current.RemoveAt(0);
            }
            distance++;
            foreach (List<int> pos in next)
            {
                current.Add(pos);
            }
            next.Clear();
        }
        distance -= 2;
        float px = enemy.transform.position.x - player.position.x;
        float pz = enemy.transform.position.z - player.position.z;
        if (distance > 15)
        {
            return ("null");
        }
        else if ((px <= range && px > 1 || px < -1 || px >= -range) && (pz > 0 && pz < range) && gameBoard[en_x, en_y - 1] != 'w' )
        {
            return ("back");
        }
        else if ((px <= range && px > 1 || px < -1 || px >= -range) && (pz < 0 && pz > -range) && gameBoard[en_x, en_y + 1] != 'w')
        {
            return ("forward");
        }
        else if (temp_board[en_x - 1, en_y] == Convert.ToString(distance))
        {
            return ("left");
        }
        else if (temp_board[en_x + 1, en_y] == Convert.ToString(distance))
        {
            return ("right");
        }
        else if (temp_board[en_x, en_y + 1] == Convert.ToString(distance))
        {
            return ("forward");
        }
        else //if (temp_board[en_x, en_y - 1] == Convert.ToString(distance))
        {
            return ("back");
        }
    }
    public static bool verif(char[,] tab, int a, int b)
    {
        int compte = 0;
        if (tab[a, b] == 'w')
        {
            if (tab[a - 1, b] == 'w') compte++;
            if (tab[a + 1, b] == 'w') compte++;
            if (tab[a, b - 1] == 'w') compte++;
            if (tab[a, b + 1] == 'w') compte++;
        }
        return compte >= 3;
    }


	void Start () 
    {
        p1 = null;
        instance = this;
        game_State = 0;
        a_maze_ing(_GameManager.niveau);
	}

    public void next_level()
    {
        _GameManager.niveau++;
        if (_GameManager.niveau % 2 == 0)
        {
            Application.LoadLevel("Map");
        }
        else
        {
            Application.LoadLevel("Map2");
        }
    }

    public void change_State()
    {
        game_State++;
    }
	


	void Update () 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            p1.gameObject.SetActive(false);
            _GameManager.niveau = 0;
            _GameManager.instance.player.set_life(100);
            _GameManager.instance.GetComponent<_MapPlayer>().l = 10;
            _GameManager.instance.player.set_dmg(20);
            _GameManager.instance.GetComponent<_MapPlayer>().d = 20;
            _GameManager.instance.player.set_range(1);
            _GameManager.instance.GetComponent<_MapPlayer>().r = 1;
            Application.LoadLevel("Menu0.2");
        }
        if (game_State % 3 <= 1)
        {
            p1.turnUpdate();
            
        }
        if (game_State % 3 == 2)
        {
            foreach (_Enemy enemy in enemies)
            {
                enemy.turnUpdate(p1.transform);
            }
            game_State++;
        }
	}
}
