using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DidacUI : MonoBehaviour {
    public static DidacUI instance;
    public GameObject PanneauDidac;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Destroy(PanneauDidac);
        }
    }
}
