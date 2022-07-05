using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public int xPos;
    public int zPos;
    public Vector3 spawnPosition;
    public int enemyCount;
    public int enemyLimit;

    public List<GameObject> zombies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnZombie());
    }

    IEnumerator SpawnZombie()
    {
        while(enemyCount < enemyLimit)
        {
            xPos = Random.Range(530, 551);
            zPos = Random.Range(200, 220);
            spawnPosition = new Vector3(xPos, 20, zPos);
            zombies.Add(Instantiate(zombie, spawnPosition, Quaternion.identity));
            yield return new WaitForSeconds(2);
            enemyCount++;
        }
    }
}
