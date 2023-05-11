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
    List<GameObject> skeletons = new List<GameObject>();
    int orcsCount = 30;
    List<GameObject> orcs = new List<GameObject>();

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

        // Reset skeletons
        foreach (var skeleton in skeletons) Destroy(skeleton);
        skeletons.Clear();
        for (int i = 0; i < skeletonsCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorPositions.Count);
            Vector2 skeletonPosition = dungeonGenerator.FloorPositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            skeletons.Add(Instantiate(skeletonPrefab, (Vector3)(skeletonPosition), Quaternion.identity));
        }

        // Reset orcs
        foreach (var orc in orcs) Destroy(orc);
        orcs.Clear();
        for (int i = 0; i < orcsCount; i++)
        {
            int randomIndex = Random.Range(0, dungeonGenerator.FloorInsidePositions.Count);
            Vector2 orcPosition = dungeonGenerator.FloorInsidePositions.ElementAt(randomIndex) + new Vector2(0.5f, 0.5f);
            orcs.Add(Instantiate(orcPrefab, (Vector3)(orcPosition), Quaternion.identity));
        }
    }
    
}
