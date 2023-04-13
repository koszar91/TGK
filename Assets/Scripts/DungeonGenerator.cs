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

    public Tilemap wallTilemap;
    public TileBase wallTile;

    public void Start()
    {
        Generate();
    }
    
    private void Generate()
    {
        var floorPositions = GenerateFloorPositions();
        var wallPositions = GenerateWallPositions(floorPositions);

        VisualizeTiles(floorPositions, floorTilemap, floorTile);
        VisualizeTiles(wallPositions, wallTilemap, wallTile);
    }

    private HashSet<Vector2Int> GenerateWallPositions(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();

        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in ProceduralGeneration.Directions)
            {
                var potentialWallPosition = floorPosition + direction;
                if (!floorPositions.Contains(potentialWallPosition))
                {
                    wallPositions.Add(potentialWallPosition);
                }
            }
        }

        return wallPositions;
    }

    private HashSet<Vector2Int> GenerateFloorPositions()
    {
        var floorPositions = new HashSet<Vector2Int>();
        var position = startPosition;
        
        for (int i = 0; i < iterations; i++)
        {
            var walk = ProceduralGeneration.SimpleRandomWalk(position, walkLength);
            floorPositions.UnionWith(walk);
        }

        return floorPositions;
    }

    private void VisualizeTiles(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
