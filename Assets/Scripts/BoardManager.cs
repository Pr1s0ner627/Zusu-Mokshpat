using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    [Header("Board Setup")]
    public Transform[] tiles; // Assign each room tile in order (Tile_1 → Tile_100)
    public Dictionary<int, int> specialTiles = new Dictionary<int, int>();

    void Awake()
    {
        // Define hidden doors (ladders) and pitfalls (snakes)
        // Format: currentTile → destinationTile
        specialTiles.Add(3, 22);   // Hidden door
        specialTiles.Add(5, 8);    // Small bonus move
        specialTiles.Add(27, 1);   // Pitfall
        specialTiles.Add(21, 9);   // Pitfall
        specialTiles.Add(17, 28);  // Hidden door
        specialTiles.Add(19, 7);   // Pitfall
        specialTiles.Add(54, 34);  // Pitfall
        specialTiles.Add(72, 91);  // Hidden door
        specialTiles.Add(98, 79);  // Pitfall
    }

    public Vector3 GetTilePosition(int tileIndex)
    {
        if (tileIndex < 0 || tileIndex >= tiles.Length)
        {
            Debug.LogWarning("Tile index out of range!");
            return Vector3.zero;
        }
        return tiles[tileIndex].position;
    }

    public int CheckForSpecialTile(int currentTile)
    {
        if (specialTiles.ContainsKey(currentTile))
        {
            return specialTiles[currentTile];
        }
        return currentTile;
    }
}
