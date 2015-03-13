using UnityEngine;
using System.Collections;

public class killzikmu : MonoBehaviour
{


    private static killzikmu instance = null;
    public static killzikmu Instance
    {
        get
        {

            return instance;
        }
    }

    void Awake() {
if (instance != null && instance != this)
        {
Destroy(this.gameObject);
return;
} 
        else
        {
instance = this;
}
DontDestroyOnLoad(this.gameObject);
}
}
