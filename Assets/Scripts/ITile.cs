using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    public Vector2Int position {get;}
    public int weight {get;}
    public bool walkable {get;}
}
