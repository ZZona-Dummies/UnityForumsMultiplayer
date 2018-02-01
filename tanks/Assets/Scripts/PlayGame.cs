using UnityEngine;
using UnityEngine.Networking;

public class PlayGame : NetworkBehaviour
{
    static public PlayGame singleton;

    [SyncVar]
    private int barrelScore;

    [SyncVar]
    private int controlScoreOne;

    [SyncVar]
    private int controlScoreTwo;

    [SyncVar]
    public int controlComplete;

    [SyncVar]
    private bool complete;

    private void Awake()
    {
        singleton = this;
    }

    public static int GetBarrelScore()
    {
        return singleton.barrelScore;
    }

    public static int GetControlScore(int team)
    {
        if (team == 0)
            return singleton.controlScoreOne;
        else
            return singleton.controlScoreTwo;
    }

    public static bool GetComplete()
    {
        return singleton.complete;
    }

    public int AddBarrelScore()
    {
        barrelScore += 1;
        if (barrelScore >= 3)
            complete = true;

        return barrelScore;
    }

    public int AddControlScore(int team, int amount)
    {
        if (team == 0)
        {
            controlScoreOne += amount;
            if (controlScoreOne >= controlComplete)
                complete = true;

            return controlScoreOne;
        }
        else
        {
            controlScoreTwo += amount;
            if (controlScoreTwo >= controlComplete)
                complete = true;

            return controlScoreTwo;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isServer)
            {
                NetworkManager.singleton.StopHost();
            }
            else
            {
                NetworkManager.singleton.client.Disconnect();
                Application.LoadLevel("title");
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // spawn heavy tank
            GameObject tank = (GameObject)Instantiate(NetworkManager.singleton.playerPrefab, Vector3.zero, Quaternion.identity);
            TankCombat tc = tank.GetComponent<TankCombat>();
            tc.InitializeFromTankType(TankTypeManager.Random());
            NetworkServer.Spawn(tank);
        }
    }
}