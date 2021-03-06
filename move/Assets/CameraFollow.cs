﻿using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour
{
    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector3 pos = this.transform.position;
        pos.y = pos.y / 2;

        Vector3 cpos = Camera.main.transform.position;
        cpos.x = pos.x;

        Camera.main.transform.position = cpos;
        Camera.main.transform.LookAt(pos);
    }
}