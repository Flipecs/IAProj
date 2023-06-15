using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ITile
{
    public TileData data;
    [HideInInspector] public Vector2Int position { get; set; }
    [HideInInspector] public bool walkable { get => data.walkable; }
    [HideInInspector] public int weight { get => data.weight; }

    public void Awake() {
        GetComponent<SpriteRenderer>().color = data.color;
    }
}
