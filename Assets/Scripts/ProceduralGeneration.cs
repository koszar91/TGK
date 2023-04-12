using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGeneration
{
    public static List<Vector2Int> Directions = new List<Vector2Int> {
        Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left
    }; 

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int length)
    {
        var walk = new HashSet<Vector2Int>();
        var position = startPosition;

        for (int i = 0; i < length; i ++)
        {
            walk.Add(position);
            position += Directions[Random.Range(0, Directions.Count)];
        }

        return walk;
    }
}
