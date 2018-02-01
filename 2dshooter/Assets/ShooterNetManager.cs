using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShooterNetManager : NetworkManager
{
    public GameObject bulletPrefab;

    public int BulletsInPool = 50;
    public int BulletsToStart = 5;
    public List<GameObject> bullets;

    private void AddBulletToPool()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        bullet.SetActive(false);
        GameObject.DontDestroyOnLoad(bullet);
        bullets.Add(bullet);
    }

    private void OnLevelLoaded()
    {
        for (int i = 0; i < BulletsInPool; i++)
        {
            bullets[i].SetActive(false);
        }
    }

    private int FindFreeBulletIndex()
    {
        for (int i = 0; i < BulletsInPool; i++)
        {
            if (i >= bullets.Count)
            {
                AddBulletToPool();
            }
            if (bullets[i].activeSelf == false)
            {
                return i;
            }
        }
        return -1;
    }

    private void Start()
    {
        /*
            ClientScene.RegisterPrefab(bulletPrefab, OnSpawnBullet, OnUnSpawnBullet);

            bullets = new List<GameObject>();
            for (int i=0; i < BulletsToStart; i++)
            {
                AddBulletToPool();
            }
            */
    }

    public GameObject OnSpawnBullet(Vector3 position, string assetId)
    {
        int bulletIndex = FindFreeBulletIndex();
        if (bulletIndex == -1)
        {
            Debug.LogError("no more bullets");
            return null;
        }

        GameObject newBullet = bullets[bulletIndex];
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        return newBullet;
    }

    public void OnUnSpawnBullet(GameObject spawned)
    {
        spawned.SetActive(false);
    }
}