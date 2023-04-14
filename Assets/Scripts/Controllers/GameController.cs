using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject skeletonPrefab;
    public CinemachineVirtualCamera playerCamera;
    public DungeonGenerator dungeonGenerator;

    GameObject player;
    Vector2Int playerStartPosition = Vector2Int.zero;

    GameObject skeleton;

    bool nextLevel = false;
    int currentLevel = 1;

    public void Start()
    {
        StartLevel();
    }

    public void Update()
    {
        if (nextLevel) StartLevel();
        nextLevel = false;
    }

    public void PlayerExit()
    {
        Debug.Log("Level " + currentLevel + " completed!");
        currentLevel ++;
        nextLevel = true;
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

        // Reset skeleton
        Destroy(skeleton);
        skeleton = Instantiate(skeletonPrefab, (Vector3Int)(playerStartPosition + new Vector2Int(5, 0)), Quaternion.identity);
    }
    
}
