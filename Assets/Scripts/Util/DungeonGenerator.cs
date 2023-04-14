using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class DungeonGenerator : MonoBehaviour
{
    public int iterations = 200;
    public int walkLength = 300;

    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public Tilemap exitTilemap;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase exitTile;

    static List<Vector2Int> DIRECTIONS = new List<Vector2Int> {
        Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left
    };

    public void GenerateDungeon()
    {
        var floorPositions = GenerateFloorPositions();
        VisualizeTiles(floorPositions, floorTilemap, floorTile);

        var wallPositions = GenerateWallPositions(floorPositions);
        VisualizeTiles(wallPositions, wallTilemap, wallTile);

        var exitPosition = wallPositions.ElementAt(Random.Range(0, wallPositions.Count));
        var exitTilePosition = exitTilemap.WorldToCell((Vector3Int)exitPosition);
        wallTilemap.SetTile(exitTilePosition, null);
        exitTilemap.SetTile(exitTilePosition, exitTile);
    }

    public void ClearDungeon()
    {
        wallTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
        exitTilemap.ClearAllTiles();
    }

    private HashSet<Vector2Int> GenerateFloorPositions()
    {
        var floorPositions = new HashSet<Vector2Int>();
        var position = Vector2Int.zero;
        
        for (int i = 0; i < iterations; i++)
        {
            var walk = SimpleRandomWalk(position, walkLength);
            floorPositions.UnionWith(walk);
        }

        return floorPositions;
    } 

    private HashSet<Vector2Int> GenerateWallPositions(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();

        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in DIRECTIONS)
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

    private HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int length)
    {
        var walk = new HashSet<Vector2Int>();
        var position = startPosition;

        for (int i = 0; i < length; i ++)
        {
            walk.Add(position);
            position += DIRECTIONS[Random.Range(0, DIRECTIONS.Count)];
        }

        return walk;
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
