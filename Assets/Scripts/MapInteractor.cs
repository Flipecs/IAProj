using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map), typeof(Pather))]
public class MapInteractor : MonoBehaviour
{
    public new Camera camera;
    public Transform highlight;
    public Transform startHighlight;
    public Transform endHighlight;
    public Transform highlightContainer;
    private Map map;
    private Pather pather;
    private Vector2Int start, end;

    
    public void Start() {
        pather = GetComponent<Pather>();
        map = GetComponent<Map>();

        start = Vector2Int.zero;
        startHighlight = Instantiate(startHighlight, map.grid.GetCellCenterLocal(new Vector3Int(start.x, start.y, 1)), Quaternion.identity, this.transform);
        end = map.size - Vector2Int.one;
        endHighlight = Instantiate(endHighlight, map.grid.GetCellCenterLocal(new Vector3Int(end.x, end.y, 1)), Quaternion.identity, this.transform);
    }

    public void Update() {
        CheckPoints();
    }

    public void CheckPoints() {
        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("a1");
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100);
            if (hit != null && hit.collider != null) {
            Debug.Log("b1");
                Tile t = hit.collider.transform.GetComponent<Tile>();
            Debug.Log(t);
                if (t != null) {
            Debug.Log("c1");
                    start = t.position;
                    startHighlight.position = map.grid.GetCellCenterLocal(new Vector3Int(t.position.x, t.position.y, 1));
                    DrawPath();
                }
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            Debug.Log("a2");
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100);
            if (hit != null && hit.collider != null) {
                Tile t = hit.collider.transform.GetComponent<Tile>();
                if (t != null) {
                    end = t.position;
                    endHighlight.position = map.grid.GetCellCenterLocal(new Vector3Int(t.position.x, t.position.y, 1));;
                    DrawPath();
                }
            }
        }
    }

    private void ClearHighlight() {
        foreach (Transform t in highlightContainer) {
            Destroy(t.gameObject);
        }
    }

    private void DrawPath() {
        ClearHighlight();
        List<Vector2Int> path = pather.FindPath(start, end);
        for (int i = 1; i < path.Count -1; i++) {
            Instantiate(highlight, map.grid.GetCellCenterLocal(new Vector3Int(path[i].x, path[i].y, 1)), Quaternion.identity, highlightContainer);
        }
    }
}
