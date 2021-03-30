using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Enemy item in FindObjectsOfType<Enemy>())
        {
            enemies.Add(item);
        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            GetComponentInChildren<Door>().LerpInit(true);
    }
}
