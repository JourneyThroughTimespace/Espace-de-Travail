using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject playerPrefab;
    public SpawnPoint[] spawnPoints;
    public int game_state = 0;

    void Start()
    {
        if (Network.isServer)
            SpawnPlayer();
        else
            Network.Connect(NetworkManager.GameToJoin);
    }

    

    void OnConnectedToServer()
    {
        Debug.Log("Un nouveau joueur s'est connecté !");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int index = 0;

        if (NetworkManager.GameToJoin != null)
            index = NetworkManager.GameToJoin.connectedPlayers;

        Debug.Log(index);

        // Attention ici on utilise Network.Instanciate et pas Object.Instanciate.
        var player = Network.Instantiate(playerPrefab, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0) as GameObject;

    }
}
