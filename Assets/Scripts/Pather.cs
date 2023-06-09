using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class Pather : MonoBehaviour
{
    Map map;

    public void Start() {
        map = GetComponent<Map>();
    }
}
