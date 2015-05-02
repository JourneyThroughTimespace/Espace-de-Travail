using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Random = UnityEngine.Random;

public class Generation_map : MonoBehaviour
{


    public GameObject brick;
    public GameObject perso;
    public GameObject sol;
    public GameObject exit;
    public GameObject[] decor;
    public GameObject camera;
    public GameObject enemies;
    //public GameObject light;
    public Light light;
    

    //a_maze_ing
    public int min_room_size = 5;
    public int max_room_size = 10;
    public int dungeon_length = 50;
    public int dungeon_width = 50;
    public int density_room = 80;

    private char[,] solution;
    private char[,] gameBoard;

    private void a_maze_ing()
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

        // placement decor
        int nb_element = 0;
        List<List<int>> dec = new List<List<int>>();
        while (nb_element < 10)
        {
            int x = Random.Range(0, dungeon_width);
            int y = Random.Range(0, dungeon_length);
            if (board[x, y] == 'r')
            {
                dec.Add(new List<int> { x, y });
                nb_element++;
                //board[x, y] = 'D';
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
        

        List<int> per = rooms[Random.Range(0, rooms.Count)];
        board[per.ElementAt(0), per.ElementAt(1)] = 'p';

        List<int> en = rooms[Random.Range(0, rooms.Count)];
        while(en == per)
        {
            en = rooms[Random.Range(0, rooms.Count)];
        }
        board[en.ElementAt(0), en.ElementAt(1)] = 'a';

        // Affichage

        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                Instantiate(sol, new Vector3(i, -0.5f, j), Quaternion.identity);
                if (board[i, j] == 'w')
                {
                    Instantiate(brick, new Vector3(i, 0, j), Quaternion.identity);
                }
                if (board[i, j] == 'p')
                {
                    Instantiate(perso, new Vector3(i, 0, j), Quaternion.identity);
                    Instantiate(light, new Vector3(i, 0, j), Quaternion.identity);
                    //Instantiate(camera, new Vector3(i, 6, j - 5), Quaternion.Euler(60,0,0));
                    Instantiate(camera, new Vector3(25, 50, 25), Quaternion.Euler(90, 0, 0));
                }
                if (board[i, j] == 'a')
                {
                    Instantiate(enemies/*[Random.Range(0, enemies.Length)]*/, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
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
        //solution = resolution(board);

    }

    public char[,] resolution(char[,] sol, Transform player, Transform enemy)
    {
        int en_x = (int)enemy.transform.position.x;
        int en_y = (int)enemy.transform.position.z;
        int pl_x = (int)player.transform.position.x;
        int pl_y = (int)player.transform.position.z;

        sol[en_x, en_y] = 'E';
        sol[pl_x, pl_y] = 'P';


        string[,] temp_board = new string[dungeon_width, dungeon_length];
        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                if (sol[i, j] == 'w' || sol[i,j] == 'd')
                {
                    temp_board[i, j] = "w";
                }
                else
                {
                    temp_board[i, j] = "e";
                }
            }
        }
        //temp_board[depart.ElementAt(0), depart.ElementAt(1)] = "d";
        temp_board[pl_x, pl_y] = "o";

        int distance = 0;
        List<List<int>> current = new List<List<int>>();
        current.Add(new List<int> { en_x, en_y });
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
        distance--;
        int x_sol = pl_x;
        int y_sol = pl_y;
        while (solution)
        {
            string rang = Convert.ToString(distance);
            if (temp_board[x_sol, y_sol] == "0")
            {
                temp_board[x_sol, y_sol] = "d";
                solution = false;
            }
            else
            {
                temp_board[x_sol, y_sol] = "c";
                if (temp_board[x_sol - 1, y_sol] == rang)
                {
                    x_sol--;
                }
                else if (temp_board[x_sol + 1, y_sol] == rang)
                {
                    x_sol++;
                }
                else if (temp_board[x_sol, y_sol - 1] == rang)
                {
                    y_sol--;
                }
                else if (temp_board[x_sol, y_sol + 1] == rang)
                {
                    y_sol++;
                }
            }
            distance--;
        }
        for (int i = 0; i < dungeon_width; i++)
        {
            for (int j = 0; j < dungeon_length; j++)
            {
                if (temp_board[i, j] == "c")
                {
                    sol[i, j] = 'c';
                }
            }
        }
        return sol;
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

    // Use this for initialization
    void Start()
    {
        instance = this;
        a_maze_ing();
    }

    public static Generation_map instance;
    public int turn = 0;
    
    
    
    void Update()
    {
        if(turn%3 == 2)
        {
            temp_enemy_mvt.instance.enemy_Turn(gameBoard, Temp_Perso_mvt.instance.transform);
        }
        else
        {
            Temp_Perso_mvt.instance.player_turn();
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("Menu0.1");
        }
    }
}
