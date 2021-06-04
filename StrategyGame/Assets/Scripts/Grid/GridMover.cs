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

    public float stepTime = 0.4f;
    private IEnumerator MoveByStep(List<GridTile> path) {
        yield return new WaitForSeconds(0.1f);
        bool cancelStep = false;
        for (int i = 0; i < path.Count; i++) {
            GridTile dest = path[i];
            //If the grid changes while in movement, recalculate path
            if (dest.isOccupied) {
                for (int f = path.Count - 1; f >= 0; f--) {
                    
                    if (f < i) { //We are as far as we can get along the path
                        cancelStep = true;
                        break;
                    }

                    
                    if (!path[f].isOccupied) {
                        Move(path[f]);
                        cancelStep = true;
                        break;
                    }
                }      
            }

            if (!cancelStep) {
                float timer = stepTime;
                Vector2 startPos = this.transform.position;
                Vector2 stepEndPos = GridMan.GetTileTransformPosition(dest);

                currentLocation.isOccupied = false;
                currentLocation = dest;
                currentLocation.isOccupied = true;

                while (timer > 0) {
                    timer -= Time.deltaTime;
                    this.transform.position = Vector3.Lerp(startPos, stepEndPos, 1 - timer / stepTime);
                    yield return new WaitForEndOfFrame();
                }
            }
            
            
            //yield return new WaitForSeconds(0.5f);

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
