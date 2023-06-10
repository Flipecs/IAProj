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
        start = Vector2Int.zero;
        end = map.size;
        AStar();
    }

    private void AStar() {
        (int f, int g, int h, Vector2Int p,  bool v)[,] values = new (int,int,int,Vector2Int,bool)[map.size.x, map.size.y];
        for (int i = 0; i < map.size.x; i++) {
            for (int j = 0; j < map.size.y; j++) {
                values[i,j] = (int.MaxValue, 0, 0, new Vector2Int(-1,-1), false);
            } 
        }
        int tmpf, tmpg, tmph = (end - start).sqrMagnitude;
        values[start.x, start.y] = (tmph, 0, tmph, start, true);

        List<Tile> list = new List<Tile>();
        Tile tile;
        list.Add(map[start.x, start.y]);
        while (list.Count > 0) {
            tile = list[0];
            list.RemoveAt(0);
        // Debug.Log("a");
            values[tile.position.x, tile.position.y].v = true;
            foreach(Tile neigh in map.Neighbours4(tile.position)) {
        // Debug.Log("b");
                tmph = (end - neigh.position).sqrMagnitude;
                tmpg = values[tile.position.x, tile.position.y].g + neigh.data.weight * neigh.data.weight;
                tmpf = tmph + tmpg;
                if (!values[neigh.position.x, neigh.position.y].v && tmph+tmpg < values[neigh.position.x, neigh.position.y].f) {
        // Debug.Log("c1");
                    values[neigh.position.x, neigh.position.y] = (tmpf, tmpg , tmph, tile.position, false);
        // Debug.Log("c2");
                    Debug.Log(("index", FindIndex(list, values, tmpf)));
        // Debug.Log("c3");
                    list.Insert(FindIndex(list, values, tmpf), neigh);
        // Debug.Log("d");
                }
        // Debug.Log("e");
            }
        Debug.Log(("size", list.Count));
        }
        string text = "";
        for (int i = 0; i < map.size.x; i++) {
            for (int j = 0; j < map.size.y; j++) {
                text += "(" + values[i,j].p.x + ", " + values[i,j].p.y + ") , ";
            }
            text += '\n';
        } 
        Debug.Log(text);
    }

    private int FindIndex(List<Tile> list, (int f, int g, int h, Vector2Int p, bool v)[,] values, int f) => FindIndex(list, values, f, 0, list.Count);
    private int FindIndex(List<Tile> list, (int f, int g, int h, Vector2Int p, bool v)[,] values, int f, int i, int j) {
        // Debug.Log("aa");
        if (i>=j) return i;
        // Debug.Log("ab");
        int mid = (i+j)>>1;
        // Debug.Log("ac");
        Vector2Int pos = list[mid].position;
        // Debug.Log(("ad",i,j,pos.x, pos.y));
        if (f < values[pos.x, pos.y].f) {
        // Debug.Log("ae");
            return FindIndex(list, values, f, mid+1, j);
        }
        else {
        // Debug.Log("ag");
            return FindIndex(list, values, f, i, mid-1);
        }
    }
}
