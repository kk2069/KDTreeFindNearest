using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 200;

    private KDTree<GameObject> kdTree;
    public List<GameObject> enemies;

    void Start()
    {
        kdTree = new KDTree<GameObject>(3);
        enemies = new List<GameObject>();

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-50, 50), Random.Range(-10, 10), Random.Range(-50, 50));
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemies.Add(enemy);
            kdTree.Insert(new float[] { position.x, position.y, position.z }, enemy);
        }
    }

    void Update()
    {
        foreach (var enemy in enemies)
        {
            Vector3 newPosition = enemy.transform.position;

            // ���� KD ���е�λ��
            kdTree.Remove(new float[] { newPosition.x, newPosition.y, newPosition.z });
            kdTree.Insert(new float[] { newPosition.x, newPosition.y, newPosition.z }, enemy);
        }
    }

    public GameObject FindNearestEnemy(Vector3 playerPosition)
    {
        return kdTree.FindNearest(new float[] { playerPosition.x, playerPosition.y, playerPosition.z });
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        kdTree.Remove(new float[] { enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z });
    }

}
