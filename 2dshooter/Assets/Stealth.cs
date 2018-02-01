using UnityEngine;
using UnityEngine.Networking;

public class Stealth : NetworkBehaviour
{
    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CmdToggleStealth();
        }
    }

    [Command]
    private void CmdToggleStealth()
    {
        bool hidden = GetComponent<NetworkProximityChecker>().forceHidden;
        GetComponent<NetworkProximityChecker>().forceHidden = !hidden;
    }
}