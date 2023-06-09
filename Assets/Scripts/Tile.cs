using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileData data;
    [HideInInspector] public Vector2Int position;

    public void Awake() {
        GetComponent<SpriteRenderer>().color = data.color;
    }
}
