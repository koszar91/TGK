using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject skeletonPrefab;
    public GameObject orcPrefab;
    public CinemachineVirtualCamera playerCamera;
    public DungeonGenerator dungeonGenerator;

    GameObject player;
    Vector2Int playerStartPosition = Vector2Int.zero;

    int skeletonsCount = 50;
    int orcsCount = 30;
    Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    int currentLevel = 1;

    public void Start()
    {
        StartLevel();
    }

    public void Update()
    {
        
    }

    public void PlayerExit()
    {
        Debug.Log("Level " + currentLevel + " completed!");
        currentLevel ++;
        StartLevel();
    }

    public void EnemyDead(GameObject enemy)
    {
        int id = enemy.GetInstanceID();
        Debug.Log("Enemy " + id + " should be destroyed. Is in map: " + enemies.ContainsKey(id));
        Destroy(enemies[id]);
        Debug.Log("destroyed enemy: " + enemies[id]);
        enemies.Remove(id);
    }

    public void PlayerDead()
    {
        Debug.Log("Game lost!");
        currentLevel = 1;
        StartLevel();
    }

    private void StartLevel()
    {
        // Reset dungeon
        dungeonGenerator.ClearDungeon();
        dungeonGenerator.GenerateDungeon(playerStartPosition);

        // Reset player
        Destroy(player);
        player = Instantiate(playerPrefab, (Vector3Int)playerStartPosition, Quaternion.identity);
        playerCamera.Follow = player.transform;

        // Clear enemies
        foreach (var pair in enemies) Destroy(pair.Value);
        enemies.Clear();
        
        // Spawn skeletons
        for (int i = 0; i < skeletonsCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 skeletonPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            GameObject skeleton = Instantiate(skeletonPrefab, (Vector3)(skeletonPosition), Quaternion.identity);
            skeleton.GetComponent<EnemyCombat>().SetPlayer(player);
            skeleton.GetComponent<EnemyMovement>().SetPlayer(player);
            enemies.Add(skeleton.GetInstanceID(), skeleton);
        }

        // Spawn orcs
        for (int i = 0; i < orcsCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 orcPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            GameObject orc = Instantiate(orcPrefab, (Vector3)(orcPosition), Quaternion.identity);
            orc.GetComponent<EnemyCombat>().SetPlayer(player);
            orc.GetComponent<EnemyMovement>().SetPlayer(player);
            enemies.Add(orc.GetInstanceID(), orc);
        }
    }
    
}
