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

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end) {
        (float f, float g, float h, Vector2Int p,  bool v)[,] values = new (float,float,float,Vector2Int,bool)[map.size.x, map.size.y];
        for (int i = 0; i < map.size.x; i++) {
            for (int j = 0; j < map.size.y; j++) {
                values[i,j] = (float.MaxValue, 0, 0, new Vector2Int(-1,-1), false);
            } 
        }
        float tmpf, tmpg, tmph = (end - start).magnitude;
        values[start.x, start.y] = (tmph, 0, tmph, start, true);

        List<ITile> list = new List<ITile>();
        ITile tile;
        list.Add(map[start.x, start.y]);
        while (list.Count > 0) {
            tile = list[0];
            list.RemoveAt(0);
            values[tile.position.x, tile.position.y].v = true;
            foreach(ITile neigh in map.Neighbours4(tile.position)) {
                tmph = (end - neigh.position).magnitude;
                tmpg = values[tile.position.x, tile.position.y].g + neigh.weight;
                tmpf = tmph + tmpg;
                if (!values[neigh.position.x, neigh.position.y].v && tmph+tmpg < values[neigh.position.x, neigh.position.y].f) {
                    values[neigh.position.x, neigh.position.y] = (tmpf, tmpg , tmph, tile.position, false);
                    list.Insert(FindIndex(list, values, tmpf), neigh);
                }
            }
        }
        List<Vector2Int> path = new List<Vector2Int>();
        if (values[end.x, end.y].p.x != -1) {
            path.Add(end);
            tile = map[end.x, end.y];
            while (values[tile.position.x, tile.position.y].p != tile.position) {
                path.Insert(0, values[tile.position.x, tile.position.y].p);
                tile = map[values[tile.position.x, tile.position.y].p.x, values[tile.position.x, tile.position.y].p.y];
            }
        }
        return path;
    }

    private int FindIndex(List<ITile> list, (float f, float g, float h, Vector2Int p, bool v)[,] values, float f) => FindIndex(list, values, f, 0, list.Count);
    private int FindIndex(List<ITile> list, (float f, float g, float h, Vector2Int p, bool v)[,] values, float f, int i, int j) {
        if (i>=j) return i;
        int mid = (i+j)>>1;
        Vector2Int pos = list[mid].position;
        if (f >= values[pos.x, pos.y].f) {
            return FindIndex(list, values, f, mid+1, j);
        }
        else {
            return FindIndex(list, values, f, i, mid-1);
        }
    }
}
