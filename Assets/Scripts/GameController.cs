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
    public GameObject healthPotionPrefab;
    public GameObject speedPotionPrefab;

    public CinemachineVirtualCamera playerCamera;
    public DungeonGenerator dungeonGenerator;

    GameObject player;
    Vector2Int playerStartPosition = Vector2Int.zero;
    
    Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> potions = new Dictionary<int, GameObject>();

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
        Destroy(enemies[id]);
        enemies.Remove(id);
    }

    public void PotionConsumed(GameObject potion)
    {
        int id = potion.GetInstanceID();
        Destroy(potions[id]);
        potions.Remove(id);
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
        dungeonGenerator.GenerateDungeon(playerStartPosition, currentLevel + 1);

        // Reset player
        Destroy(player);
        player = Instantiate(playerPrefab, (Vector3Int)playerStartPosition, Quaternion.identity);
        playerCamera.Follow = player.transform;

        // Clear enemies
        foreach (var pair in enemies) Destroy(pair.Value);
        enemies.Clear();
        
        // Spawn skeletons
        int skeletonsCount = currentLevel * 10;
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
        int orcsCount = currentLevel + 3;
        for (int i = 0; i < orcsCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 orcPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            GameObject orc = Instantiate(orcPrefab, (Vector3)(orcPosition), Quaternion.identity);
            orc.GetComponent<EnemyCombat>().SetPlayer(player);
            orc.GetComponent<EnemyMovement>().SetPlayer(player);
            enemies.Add(orc.GetInstanceID(), orc);
        }

        // Clear health postions
        foreach (var pair in potions) Destroy(pair.Value);
        potions.Clear();

        // Spawn healthPotions
        int healthPotionCount = (currentLevel + 1) * 2;
        for (int i = 0; i < healthPotionCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 potionPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            GameObject potion = Instantiate(healthPotionPrefab, (Vector3)(potionPosition), Quaternion.identity);
            potions.Add(potion.GetInstanceID(), potion);
        }

        // Spawn speedPotions
        int speedPotionCount = (currentLevel + 1);
        for (int i = 0; i < speedPotionCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 potionPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            GameObject potion = Instantiate(speedPotionPrefab, (Vector3)(potionPosition), Quaternion.identity);
            potions.Add(potion.GetInstanceID(), potion);
        }
    }
    
}
