using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoardManager : MonoBehaviour
{
    [Header("Auto Assign & Sort")]
    public Transform tileParent;   // Parent object containing all tile GameObjects
    public Transform[] tiles;
    public int columns = 10;       // 10x10 grid
    public Dictionary<int, int> specialTiles = new Dictionary<int, int>();

    void Awake()
    {
        AutoAssignTiles();
        AutoSortTiles();

        // Example hidden door & pitfall setup
        specialTiles.Add(3, 22);    // Hidden door
        specialTiles.Add(27, 1);    // Pitfall
        specialTiles.Add(72, 91);   // Hidden door
        specialTiles.Add(98, 79);   // Pitfall
    }

    private void AutoAssignTiles()
    {
        if (tileParent == null)
        {
            Debug.LogError("❌ Tile parent not assigned in BoardManager!");
            return;
        }

        // Grab all child transforms automatically
        tiles = tileParent.GetComponentsInChildren<Transform>()
                          .Where(t => t != tileParent)
                          .ToArray();
        Debug.Log($"🧩 Found {tiles.Length} tiles under parent '{tileParent.name}'.");
    }

    private void AutoSortTiles()
    {
        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogError("❌ No tiles found to sort!");
            return;
        }

        // Sort by grid pattern
        tiles = tiles.OrderByDescending(t => Mathf.RoundToInt(t.position.z))
                     .ThenBy(t => Mathf.RoundToInt(t.position.x))
                     .ToArray();

        // Alternate every other row (snake-board style)
        List<Transform> sorted = new List<Transform>();
        for (int i = 0; i < tiles.Length; i += columns)
        {
            var row = tiles.Skip(i).Take(columns).ToList();
            if ((i / columns) % 2 == 1) row.Reverse();
            sorted.AddRange(row);
        }

        tiles = sorted.ToArray();
        Debug.Log($"✅ Auto-sorted {tiles.Length} tiles successfully.");
    }

    public Vector3 GetTilePosition(int index)
    {
        if (index < 0 || index >= tiles.Length)
        {
            Debug.LogWarning("Tile index out of range!");
            return Vector3.zero;
        }
        return tiles[index].position;
    }

    public int CheckForSpecialTile(int index)
    {
        return specialTiles.ContainsKey(index) ? specialTiles[index] : index;
    }
}
