using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetStats : MonoBehaviour
{
    private Dictionary<short, PacketStatInfo> packetStats = new Dictionary<short, PacketStatInfo>();
    private float statTimer;

    private void OnGUI()
    {
        int posY = 80;
        foreach (PacketStatInfo stat in packetStats.Values)
        {
            GUI.Label(new Rect(10, posY, 460, 20), "Msg: " + MsgType.MsgTypeToString(stat.msgId) + " Count: " + stat.packets.GetAverage() + "/sec Bytes: " + stat.bytes.GetAverage() + "/sec");
            posY += 20;
        }
    }

    private void Update()
    {
        if (Time.time <= statTimer)
            return;

        statTimer = Time.time + 1;

        if (NetworkClient.allClients.Count > 0)
        {
            Dictionary<short, NetworkConnection.PacketStat> stats = NetworkClient.allClients[0].GetConnectionStats();
            if (stats != null)
            {
                foreach (var stat in stats.Values)
                {
                    if (stat.count > 0)
                    {
                        if (!packetStats.ContainsKey(stat.msgType))
                        {
                            packetStats[stat.msgType] = new PacketStatInfo(stat.msgType);
                        }
                        packetStats[stat.msgType].Add(stat);
                    }
                }
                NetworkClient.allClients[0].ResetConnectionStats();
            }
        }

        if (NetworkServer.active)
        {
            Dictionary<short, NetworkConnection.PacketStat> stats = NetworkServer.GetConnectionStats();
            if (stats != null)
            {
                foreach (var stat in stats.Values)
                {
                    if (stat.count > 0)
                    {
                        if (!packetStats.ContainsKey(stat.msgType))
                        {
                            packetStats[stat.msgType] = new PacketStatInfo(stat.msgType);
                        }
                        packetStats[stat.msgType].Add(stat);
                    }
                }
                NetworkServer.ResetConnectionStats();
            }
        }
    }

    public class RunningAverageInt
    {
        private const int NumValues = 10;

        private int[] values;
        private int pos;
        private int sum;

        public RunningAverageInt()
        {
            values = new int[NumValues];
        }

        public void Add(int value)
        {
            sum += value;
            sum -= values[pos];
            values[pos] = value;
            pos += 1;
            if (pos == NumValues)
            {
                pos = 0;
            }
        }

        public float GetAverage()
        {
            return sum / (float)NumValues;
        }
    }

    private class PacketStatInfo
    {
        public RunningAverageInt bytes;
        public RunningAverageInt packets;

        public short msgId;

        public PacketStatInfo(short id)
        {
            msgId = id;
            bytes = new RunningAverageInt();
            packets = new RunningAverageInt();
        }

        public void Add(NetworkConnection.PacketStat stat)
        {
            bytes.Add(stat.bytes);
            packets.Add(stat.count);
        }
    }
}