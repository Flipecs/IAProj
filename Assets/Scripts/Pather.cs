using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class Pather : MonoBehaviour
{
    Map map;
    Vector2Int start, end;

    public void Start() {
        map = GetComponent<Map>();
    }

}
