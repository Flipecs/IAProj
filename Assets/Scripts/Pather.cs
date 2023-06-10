using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class Pather : MonoBehaviour
{
    public Transform highlight;
    Map map;
    Vector2Int start, end;

    public void Start() {
        map = GetComponent<Map>();
        start = Vector2Int.zero;
        end = map.size - Vector2Int.one;
        foreach (Vector2Int p in AStar()) {
            Instantiate(highlight, map.grid.GetCellCenterLocal(new Vector3Int(p.x, p.y, 1)), Quaternion.identity);
        }
    }

    private List<Vector2Int> AStar() {
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
            values[tile.position.x, tile.position.y].v = true;
            foreach(Tile neigh in map.Neighbours4(tile.position)) {
                tmph = (end - neigh.position).sqrMagnitude;
                tmpg = values[tile.position.x, tile.position.y].g + neigh.data.weight * neigh.data.weight;
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

    private int FindIndex(List<Tile> list, (int f, int g, int h, Vector2Int p, bool v)[,] values, int f) => FindIndex(list, values, f, 0, list.Count);
    private int FindIndex(List<Tile> list, (int f, int g, int h, Vector2Int p, bool v)[,] values, int f, int i, int j) {
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
