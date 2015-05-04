using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    public const string TypeName = "JTT4LIF";
    public static string GameName = "Room1";
    public static HostData GameToJoin = null;

    private Rect _startBtnRect;
    private Rect _joinBtnRect;
    private Rect _cacheRect;
    private Rect _bgRect;
    private HostData[] _hostData;

    public Texture2D background;
    public string levelToLoad;

    void Start()
    {
        GameToJoin = null;

        _bgRect = new Rect(0, 0, Screen.width, Screen.height);

        // Rectangles pour les boutons
        _startBtnRect = new Rect(
            Screen.width - 605,
            Screen.height / 2 - 45, 180, 35);


        _joinBtnRect = new Rect(
            Screen.width - 605,
            Screen.height / 2 + 15, 180, 35);

        _cacheRect = new Rect(
            Screen.width - 605,
            Screen.height / 2 + 45, 180, 35);
    }

    // Affichage de l'interface.
    void OnGUI()
    {
        GUI.DrawTexture(_bgRect, background);

        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(_startBtnRect,"Start Server"))
                StartServer();

            if (GUI.Button(_joinBtnRect, "Refresh List"))
                MasterServer.RequestHostList(TypeName);

            if (_hostData != null)
            {
                for (int i = 0, l = _hostData.Length; i < l; i++)
                {
                    _cacheRect = new Rect(
                                    Screen.width - 605,
                                    Screen.height / 2 + 75, 180, 35);

                    if (GUI.Button(_cacheRect, _hostData[i].gameName))
                        JoinServer(_hostData[i]);
                }
            }
        }
    }

    // Appelée quand le serveur est initialisé par le créateur de la partie.
    void OnServerInitialized()
    {
        Debug.Log("Serveur pret et joueur connecté");
        Application.LoadLevel(levelToLoad);
    }

    // Appelée quand un événement serveur a lieux.
    void OnMasterServerEvent(MasterServerEvent sEvent)
    {
        if (sEvent == MasterServerEvent.HostListReceived)
        {
            Debug.Log("Liste des parties mise à jour");
            _hostData = MasterServer.PollHostList();
        }
    }

    private void StartServer()
    {
        if (!Network.isClient && !Network.isServer)
        {
            // Initialisation du serveur pour 4 joueurs max. sur le port 2500.
            Network.InitializeServer(4, 2500, !Network.HavePublicAddress());

            // Enregistrement du serveur avec l'identifiant du jeu et le nom de la partie.
            MasterServer.RegisterHost(TypeName, GameName);
            Debug.Log("Server créé");
        }
    }

    private void JoinServer(HostData gameToJoint)
    {
        // Identificateur unique de la partie
        GameToJoin = gameToJoint;

        // Chargement du niveau, on se connect au serveur juste après.
        Application.LoadLevel(levelToLoad);
    }
}
