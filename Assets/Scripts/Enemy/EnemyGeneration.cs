using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] spawnPoints;

    int day;
    private Dictionary<string, float> enemySpawnTimes; // Dictionary ánh xạ tag name và thời gian sinh

    private void Start()
    {
        enemySpawnTimes = new Dictionary<string, float>
    {
        { "Bee", 8f },
        { "Boar", 4f },
        { "Goblin", 2f }
    };

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Sinh Bee
            SpawnEnemy("Bee");

            // Chờ theo thời gian beeSpawnTime
            yield return new WaitForSeconds(enemySpawnTimes["Bee"]);

            // Sinh Boar
            SpawnEnemy("Boar");

            // Chờ theo thời gian boarSpawnTime
            yield return new WaitForSeconds(enemySpawnTimes["Boar"]);

            // Sinh Goblin
            SpawnEnemy("Goblin");

            // Chờ theo thời gian goblinSpawnTime
            yield return new WaitForSeconds(enemySpawnTimes["Goblin"]);
        }
    }

    private void SpawnEnemy(string enemyTagName)
    {
        
        // Tạo một danh sách các enemy prefab có cùng tag name
        List<GameObject> enemyPrefabsWithTag = new List<GameObject>();

        // Lặp qua tất cả enemy prefabs và lọc các prefab có tag name khớp
        foreach (GameObject prefab in enemyPrefabs)
        {          
            if (prefab.tag == enemyTagName)
            {
                enemyPrefabsWithTag.Add(prefab);
            }
        }

        if (enemyPrefabsWithTag.Count == 0)
        {
            Debug.LogError("No enemy prefab found with tag: " + enemyTagName);
            return;
        }

        // Chọn ngẫu nhiên một enemy prefab từ danh sách có cùng tag name
        int randomIndex = Random.Range(0, enemyPrefabsWithTag.Count);
        GameObject enemyPrefab = enemyPrefabsWithTag[randomIndex];

        // Tìm vị trí tương ứng dựa trên tag name trong mảng spawnPoints
        GameObject spawnPoint = null;
        foreach (GameObject point in spawnPoints)
        {
            if (point.name == enemyTagName + "Point")
            {
                spawnPoint = point;
                break;
            }
        }

        if (spawnPoint == null)
        {
            Debug.LogError(enemyTagName + "Position not found in spawnPoints");
            return;
        }

        // Lấy tọa độ vị trí spawn từ thành phần transform của GameObject spawnPoint
        Vector2 spawnPosition = spawnPoint.transform.position;

        // Sinh ra enemy tại vị trí spawnPosition
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
