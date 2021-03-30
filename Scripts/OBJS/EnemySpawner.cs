using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] enemies;
    [SerializeField] bool canSpawn = true;
    bool spawnNormal = false;
    bool spawnRange = false;
    bool spawnBig = false;
    bool spawnSmall = false;
    //called by enemy just before it is destroyed
    public void RespawnEnemy(GameObject enemy)
    {
        if(canSpawn == true)
        {
            foreach (GameObject boi in enemies)
            {
                if (enemy.GetComponent<Enemy>().model == boi.GetComponent<Enemy>().model)
                {
                    CreateEnemy(boi);
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject boi in enemies)
            {
                if (enemy.GetComponent<Enemy>().model == boi.GetComponent<Enemy>().model)
                {
                    AddEnemiesToSpawn(enemy.GetComponent<Enemy>().model);
                    break;
                }
            }
        }

    }
    //add an amount of enemies to spawn
    void AddEnemiesToSpawn(string enemyModel)
    {
        switch(enemyModel)
        {
            case "normal":
                spawnNormal = true;
                break;
            case "range":
                spawnRange = true;
                break;
            case "big":
                spawnBig = true;
                break;
            case "small":
                spawnSmall = true;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySpawn" || other.tag == "ImpassableOBJ" || other.GetComponent<AttackRange>() != null || other.GetComponent<RoomHitBox>() != null)
            return;
        else
            canSpawn = false;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemySpawn" || other.tag == "ImpassableOBJ" || other.GetComponent<AttackRange>() != null || other.GetComponent<RoomHitBox>() != null)
            return;
        else
            StartCoroutine(WaitForSpawn());
    }
    //called when something exits the spawner. closes the door and spawns enemies
    IEnumerator WaitForSpawn()
    {
        GetComponentInChildren<Door>().LerpInit(false);
        yield return new WaitForSeconds(3);
        canSpawn = true;
        DelayedSpawn();
    }
    //called by wait for spawn, spawns enemies
    void DelayedSpawn()
    {
        if(spawnNormal == true)
        {
            CreateEnemy(enemies[1]);
            return;
        }
        if (spawnRange == true)
        {
            CreateEnemy(enemies[2]);
            return;
        }
        if (spawnBig == true)
        {
            CreateEnemy(enemies[0]);
            return;
        }
        if (spawnSmall == true)
        {
            CreateEnemy(enemies[3]);
            return;
        }
    }
    //called when you want to create enemies
    void CreateEnemy(GameObject robot)
    {
        Instantiate(robot);
        robot.transform.position = spawnPoint.position;
        GetComponentInChildren<Door>().LerpInit(true);
    }
}
