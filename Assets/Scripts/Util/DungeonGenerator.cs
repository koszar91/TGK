using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class DungeonGenerator : MonoBehaviour
{
    public int iterations = 120;
    public int walkLength = 120;
    public int roomsCount = 7;
    public int corridorLength = 60;

    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public Tilemap exitTilemap;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase exitTile;

    static List<Vector2Int> DIRECTIONS = new List<Vector2Int> {
        Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left
    };

    public void GenerateDungeon(Vector2Int playerStartPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var currentStartPosition = Vector2Int.zero;
        HashSet<Vector2Int> room = GenerateRoom(currentStartPosition);
        floorPositions.UnionWith(room);

        for (int i = 0; i < roomsCount - 1; i++)
        {
            Vector2Int randomRoomPosition = room.ElementAt(Random.Range(0, room.Count));
            List<Vector2Int> corridor = GenerateCorridor(randomRoomPosition);
            floorPositions.UnionWith(new HashSet<Vector2Int>(corridor));

            currentStartPosition = corridor.Last();
            room = GenerateRoom(currentStartPosition);
            floorPositions.UnionWith(room);
        }

        VisualizeTiles(floorPositions, floorTilemap, floorTile);

        HashSet<Vector2Int> wallPositions = GenerateWalls(floorPositions);
        VisualizeTiles(wallPositions, wallTilemap, wallTile);

        Vector2Int exitPosition = CreateExitPosition(wallPositions, playerStartPosition);
        Vector3Int exitTilePosition = exitTilemap.WorldToCell((Vector3Int)exitPosition);
        wallTilemap.SetTile(exitTilePosition, null);
        exitTilemap.SetTile(exitTilePosition, exitTile);
    }

    public void ClearDungeon()
    {
        wallTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
        exitTilemap.ClearAllTiles();
    }

    private List<Vector2Int> GenerateCorridor(Vector2Int startPosition)
    {
        var corridor = new List<Vector2Int>();
        Vector2Int direction = DIRECTIONS[Random.Range(0, DIRECTIONS.Count)];
        var position = startPosition;

        for (int i = 0; i < corridorLength; i ++)
        {
            corridor.Add(position);
            position += direction;
        }
        return corridor;
    }

    private HashSet<Vector2Int> GenerateRoom(Vector2Int startPosition)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var position = startPosition;
        
        for (int i = 0; i < iterations; i++)
        {
            var walk = SimpleRandomWalk(position, walkLength);
            floorPositions.UnionWith(walk);
        }
        return floorPositions;
    }

    private Vector2Int CreateExitPosition(HashSet<Vector2Int> wallPositions, Vector2Int playerPosition)
    {
        Vector2Int currentExitPosition = wallPositions.ElementAt(0);
        float currentDistance = Vector2Int.Distance(currentExitPosition, playerPosition); 
        foreach (var position in wallPositions)
        {
            float distance = Vector2Int.Distance(position, playerPosition); 
            if (distance > currentDistance)
            {
                currentExitPosition = position;
                currentDistance = distance; 
            }
        }
        return currentExitPosition;
    }

    private HashSet<Vector2Int> GenerateWalls(HashSet<Vector2Int> floorPositions)
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
