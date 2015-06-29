using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject playerPrefab;
    //public Camera playerCam;
    public SpawnPoint[] spawnPoints;
    public int game_state = 0;
    public int index;


    void Start()
    {
        if (Network.isServer)
            SpawnPlayer();
        else
            Network.Connect(persoMenu1.GameToJoin);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Un nouveau joueur s'est connecté !");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        index = 0;

        if (persoMenu1.GameToJoin != null)
            index = persoMenu1.GameToJoin.connectedPlayers;

        Debug.Log(index);

        // Attention ici on utilise Network.Instanciate et pas Object.Instanciate.
        var player = Network.Instantiate(playerPrefab, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0) as GameObject;

        //playerCam.transform.position = spawnPoints[index].transform.position - new Vector3(0.49f, 7, -9.05f);
        //playerCam.transform.rotation = spawnPoints[index].transform.rotation;

        // Mise à jour du script de caméra
        //playerCam.target = player.transform;
        //playerCam.enabled = true;
    }
}
