using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int maxMob = 5;
    public int curMob;
    public float spawnTime = 3f;
    public float curTime;
    public bool[] isSpawn;
    public Transform[] spawnPoints;
    public GameObject mobs;

    public static SpawnManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        isSpawn = new bool[spawnPoints.Length];
        for (int i = 0; i < isSpawn.Length; i++)
        {
            isSpawn[i] = false;
        }
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime >= spawnTime && curMob <= maxMob)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if (!isSpawn[x])
            Spawnmobs(x);
        }
        curTime += Time.deltaTime;
    }

    void Spawnmobs(int ranNum)
    {
        curTime = 0;
        Instantiate(mobs, spawnPoints[ranNum]);
        curMob += 1;
        isSpawn[ranNum] = true;
    }
}
