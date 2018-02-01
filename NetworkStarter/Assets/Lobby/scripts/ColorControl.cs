using UnityEngine;
using UnityEngine.Networking;

public class ColorControl : NetworkBehaviour
{
    private static Color[] colors = new Color[] { Color.white, Color.magenta, Color.red, Color.cyan, Color.blue, Color.green, Color.yellow };

    [SyncVar(hook = "OnMyColor")]
    public Color myColor = Color.white;

    private NetworkLobbyPlayer lobbyPlayer;
    private PlayerLobby playerUI;

    private void Awake()
    {
        lobbyPlayer = GetComponent<NetworkLobbyPlayer>();
        playerUI = GetComponent<PlayerLobby>();
    }

    [Command]
    private void CmdSetMyColor(Color col)
    {
        // cant change color after turning ready
        if (lobbyPlayer.readyToBegin)
        {
            return;
        }

        myColor = col;
    }

    public void ClientChangeColor()
    {
        var newCol = colors[Random.Range(0, colors.Length)];
        CmdSetMyColor(newCol);
    }

    private void OnMyColor(Color newColor)
    {
        myColor = newColor;
        playerUI.SetColor(newColor);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
    }
}