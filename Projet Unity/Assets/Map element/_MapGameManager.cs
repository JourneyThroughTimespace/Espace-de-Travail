using UnityEngine;
using System.Collections;

public class _MapGameManager : _GameManager 
{
    public static _MapGameManager instance;

    public GameObject cam;

    void Start()
    {
        instance = this;
    }
}
