using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
  /*  public int xPos;
    public int zPos;
    public Vector3 spawnPosition;*/
    public int enemyCount;
    public int enemyLimit;

    public GameObject[] spawnPoints;
    public List<GameObject> zombies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnZombie());
       


    }
    private void Update()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            if (zombies[i] != null)
            {
                CheckZombieHealth(zombies[i]);
            }
            
        }
    }
    public void CheckZombieHealth(GameObject zombie)
    {
        if (zombie.GetComponent<ShootObject>().currentHealth <= 0)
        {
            Destroy(zombie.gameObject);
            enemyCount--;
            Debug.Log("There are " + enemyCount + " enemies");
        }
    }

    IEnumerator SpawnZombie()
    {
        while (enemyCount < enemyLimit)
        {
            /* xPos = Random.Range(530, 551);
             zPos = Random.Range(200, 220);
             spawnPosition = new Vector3(xPos, 20, zPos);
             zombies.Add(Instantiate(zombie, spawnPosition, Quaternion.identity));*/
            
            zombies.Add(Instantiate(zombie, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position , Quaternion.identity));
            yield return new WaitForSeconds(2);
            enemyCount++;
            //Debug.Log("There are " + enemyCount + " enemies");
        }
        if(enemyCount >= enemyLimit)
        {
            //Debug.Log("You have spawned max enemies");
        }
    }
}
