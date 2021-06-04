using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private GridManager GridMan;

    // Start is called before the first frame update
    void Start()
    {
        GridMan = this.GetComponent<GridManager>();
    }

    

    public List<GridTile> FindPath(GridTile startTile,GridTile endTile) {

        List<GridTile> openSet = new List<GridTile>();
        List<GridTile> closedSet = new List<GridTile>();
        openSet.Add(startTile);

        while(openSet.Count > 0) {
            GridTile currentTile = openSet[0];
            //Find lowest F cost
            for (int i = 1; i < openSet.Count; i++) {
                if(openSet[i].fCost < currentTile.fCost || 
                  (openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)) {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == endTile) {
                return RetracePath(startTile,endTile);  
            }

            foreach(GridTile neig in GridMan.GetNeigh(currentTile)) {
                if (neig.isOccupied || closedSet.Contains(neig)) continue;
                int newMoveCost = currentTile.gCost + GetDistance(currentTile,neig);
                if(newMoveCost < neig.gCost || !openSet.Contains(neig)) {
                    neig.gCost = newMoveCost;
                    neig.hCost = GetDistance(neig,endTile);
                    neig.cameFrom = currentTile;

                    if (!openSet.Contains(neig)) {
                        openSet.Add(neig);
                    }
   
                }

            }

        }

        return null;

    }

    public List<GridTile> RetracePath(GridTile startTile,GridTile targetTile) {
        List<GridTile> path = new List<GridTile>();
        GridTile currentTile = targetTile;
        int safety = 0;
        while (currentTile != startTile && safety < 1000) {
            safety++;
            path.Add(currentTile);
            currentTile = currentTile.cameFrom;
        }

        path.Reverse();
        return path;
    }

    public int GetDistance(GridTile a,GridTile b) {
        int distX = Mathf.Abs(a.gx-b.gx);
        int distY = Mathf.Abs(a.gy - b.gy);
        if (distX > distY) {
            return 14 * distY + 10 * (distX - distY);
        } else {
            return 14 * distX + 10 * (distY - distX);
        }

    }



}
