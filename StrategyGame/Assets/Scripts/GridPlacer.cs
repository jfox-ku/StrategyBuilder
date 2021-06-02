using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public bool isGhost;
    public GridManager GM;
    private DefaultInputManager DIM;

    private int startX;
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
            Debug.Log("Valid building");
            this.transform.position = GM.GetTileTransformPosition(tile);
            GM.SetGridRegionOccupied(tile,CellSize);
            return true;
        } else {
            Debug.Log("Invalid building");
            return false;
        }


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
