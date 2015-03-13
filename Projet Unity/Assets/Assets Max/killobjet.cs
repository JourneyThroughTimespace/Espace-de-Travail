using UnityEngine;
using System.Collections;

public class killobjet : MonoBehaviour {
    private static killobjet instance = null;
    public static killobjet Instance
    {
        get
        {

            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        Destroy(this.gameObject);
    }
}
