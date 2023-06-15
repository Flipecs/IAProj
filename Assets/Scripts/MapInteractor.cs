using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map), typeof(Pather))]
public class MapInteractor : MonoBehaviour
{
    public new Camera camera;
    public Transform highlight;
    public Transform highlightContainer;
    private Map map;
    private Pather pather;
    private Vector2Int start, end;

    
    public void Start() {
        pather = GetComponent<Pather>();
        map = GetComponent<Map>();

        start = Vector2Int.zero;
        end = map.size - Vector2Int.one;
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
        foreach (Vector2Int p in pather.FindPath(start, end)) {
            Instantiate(highlight, map.grid.GetCellCenterLocal(new Vector3Int(p.x, p.y, 1)), Quaternion.identity, highlightContainer);
        }
    }
}
