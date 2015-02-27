using UnityEngine;
using System.Collections;

public class GoToCrédits : MonoBehaviour {

    public void NextLevelButton(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
