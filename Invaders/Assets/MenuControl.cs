using UnityEngine;
using UnityEngine.Networking;

public class MenuControl : MonoBehaviour
{
    public void StartLocalGame()
    {
        NetworkManager.singleton.StartHost();
    }

    public void JoinLocalGame()
    {
        if (hostNameInput.text != "Hostname")
        {
            NetworkManager.singleton.networkAddress = hostNameInput.text;
        }
        NetworkManager.singleton.StartClient();
    }

    public void StartMatchMaker()
    {
        NetworkManager.singleton.StartMatchMaker();
    }

    public UnityEngine.UI.Text hostNameInput;

    private void Start()
    {
        hostNameInput.text = NetworkManager.singleton.networkAddress;
    }
}