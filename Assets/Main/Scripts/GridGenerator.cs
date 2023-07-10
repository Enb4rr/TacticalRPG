using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject tilePrefab;

    [Header("Bounds and Offsets")]
    [SerializeField] private float xBound;
    [SerializeField] private float yBound;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;   

    [Header("Grid Components")]
    public List<Tile> tiles;

    [Header("Grid Creation Settings")]
    [Header("No Walkable Tiles")]
    [SerializeField] public List<int> noWalkable;

    private int counter;

    private void Awake()
    {
        tiles = new List<Tile>();

        for (int i = 0; i < xBound; i++)
        {
            for(int j = 0; j < yBound; j++)
            {
                var newTile = Instantiate(tilePrefab, new Vector2(xOffset * i, yOffset * j), Quaternion.identity);
                newTile.transform.parent = transform;

                var tileComponent = newTile.GetComponent<Tile>();
                InitializeTile(tileComponent);
                tiles.Add(tileComponent);

                counter++;
            }
        }
    }

    private void InitializeTile(Tile tileToEdit)
    {
        tileToEdit.TileID = counter;

        foreach(var tileId in noWalkable)
        {
            if (counter == tileId) tileToEdit.IsWalkable = false; 
        }
    }
}
