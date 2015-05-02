using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    private BoardManager boardScript;
    public GameObject perso;

    int level = 1;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        Instantiate(perso, new Vector3(0, 100, 0), Quaternion.identity);
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.setupScene(level, perso);
    }
}
