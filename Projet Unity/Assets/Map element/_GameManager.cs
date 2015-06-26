using UnityEngine;
using System.Collections;

public class _GameManager : MonoBehaviour 
{
    public _Entity player;
    public static int niveau;

    public int get_niv()
    {
        return niveau;
    }
    public void set_niv(int n)
    {
        niveau = n;
    }


    public static _GameManager instance;



    void Start()
    {
        instance = this;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
