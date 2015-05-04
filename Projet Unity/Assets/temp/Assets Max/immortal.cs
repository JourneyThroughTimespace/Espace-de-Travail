using UnityEngine;
using System.Collections;

public class immortal : MonoBehaviour
{


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}


