using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    [SerializeField] private int tileID = 0;
    [SerializeField] private bool isWalkable = true;

    public int TileID { get => tileID; set => tileID = value; }
    public bool IsWalkable { get => isWalkable; set => isWalkable = value; }
}