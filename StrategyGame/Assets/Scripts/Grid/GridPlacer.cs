using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public bool isGhost;
    public GridManager GM;
    private DefaultInputManager DIM;

    private int StartX;
    private int StartY;
    public Vector2 CellSize;

    private void Start() {
        GM = FindObjectOfType<GridManager>();
        DIM = FindObjectOfType<DefaultInputManager>();
    }

    public bool TryPlace() {
        var mousePos = DIM.GetInputPosition();
        GridTile tile = GM.ScreenPointToGridTile(mousePos);
        if (GM.CheckGridRegion(tile, CellSize)) {
            this.transform.position = GM.GetTileTransformPosition(tile);
            StartX = tile.gx;
            StartY = tile.gy;

            GM.SetGridRegionOccupied(tile,CellSize);
            return true;
        } else {
            return false;
        }


    }

    public GridTile GetViableNeighbour(GridTile destination) {
        foreach (var tile in GM.GetNeigh(destination)) {
            if (!tile.isOccupied) return tile;
        }
        return null;
    }

    public GridTile FindSpawnPoint() {
        foreach (var tile in GetPerimeterTiles()) {
            if (!tile.isOccupied) return tile;
        }
        Debug.Log("No valid spawn points around building.");
        return null;
    }

    public List<GridTile> GetPerimeterTiles() {
        List<GridTile> per = new List<GridTile>();
        for (int i = -1; i < CellSize.x+1; i++) {
            for (int j = -1; j < CellSize.y+1; j++) {
                if(i==-1 || i == CellSize.x) {
                    var tile = GM.GetTile(StartX+i,StartY+j);
                    if (tile != null) per.Add(tile);
                }

                if(j==-1 || j == CellSize.y) {
                    var tile = GM.GetTile(StartX + i, StartY + j);
                    if (tile != null) per.Add(tile);
                }

            }

        }

        return per;
    }

    private void Update() {
        if (isGhost) FollowMouse();    
    }


    public void FollowMouse() {
        var mousePos = DIM.GetInputPosition();
        GridTile tile = GM.ScreenPointToGridTile(mousePos);
        this.transform.position = GM.GetTileTransformPosition(tile);



    }

}
