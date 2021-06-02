using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{
    public GridManager GridMan;
    public bool isMoving;
    public GridTile currentLocation;

    private void Awake() {
        GridMan = FindObjectOfType<GridManager>();
    }

    public void MoverInitialize(GridTile startPos) {
        this.transform.position = GridMan.GetTileTransformPosition(startPos);
        currentLocation = startPos;
        currentLocation.isOccupied = true;
    }

    public void Move(GridTile dest) {
        StopAllCoroutines();
        List<GridTile> path = GridMan.GetPath(currentLocation, dest);
        if (path != null) {
            //PathDebug(path);
            StartCoroutine(MoveByStep(path));

        }
    }

    private IEnumerator MoveByStep(List<GridTile> path) {
        for (int i = 0; i < path.Count; i++) {
            GridTile dest = path[i];
            //If the grid changes while in movement, recalculate path
            if (dest.isOccupied) {
                Move(path[path.Count-1]);
                break;
            }

            currentLocation.isOccupied = false;
            this.transform.position = GridMan.GetTileTransformPosition(dest);
            currentLocation = dest;
            currentLocation.isOccupied = true;
            yield return new WaitForSeconds(0.5f);

        }
        yield return null;
    }

    private void PathDebug(List<GridTile> path) {
        string ret = "Path) ";
        for (int i = 0; i < path.Count; i++) {
            ret += (i+": "+path[i].CellPos()+"\n");
        }
        Debug.Log(ret);
    }

}
