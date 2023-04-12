using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Vector2Int startPosition = Vector2Int.zero;
    public int iterations = 10;
    public int walkLength = 10;

    public Tilemap floorTilemap;
    public TileBase floorTile;

    public void Start()
    {
        Generate();
    }
    
    private void Generate()
    {
        var floorPositions = new HashSet<Vector2Int>();
        var position = startPosition;
        
        for (int i = 0; i < iterations; i++)
        {
            var walk = ProceduralGeneration.SimpleRandomWalk(position, walkLength);
            floorPositions.UnionWith(walk);
        }

        VisualizeFloor(floorPositions);
    }

    private void VisualizeFloor(HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            var tilePosition = floorTilemap.WorldToCell((Vector3Int)position);
            floorTilemap.SetTile(tilePosition, floorTile);
        }
    }
}
