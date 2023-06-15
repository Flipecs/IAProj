using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData {
    public string name;
    public bool walkable;
    public int weight;
    public Color color;

    public TileData(string name, bool walkable, int weight, Color color) {
        this.name = name;
        this.walkable = walkable;
        this.weight = weight;
        this.color = color;            
    }
}