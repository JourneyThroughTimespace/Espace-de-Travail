using UnityEngine;
using System.Collections;

public class retourmenu : MonoBehaviour {

    public void NextLevelButton(string levelName)
    {
        Application.LoadLevel(levelName);
    }
	
}
