using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int maxX = 1;
    [SerializeField] int maxY = 1;

    [SerializeField] GameObject GridBackgroundPrefab;
    [SerializeField] private Pathfinder Pather;
    private Camera cam;

    private GridTile[,] AllTiles;

    private void Start() {
        cam = Camera.main;
        InitializeGrid();
        this.transform.position -= new Vector3(maxX / 2f, maxY / 2f,0);
    }

    private void InitializeGrid() {
        AllTiles = new GridTile[maxX, maxY];
        for (int i = 0; i < maxX; i++) {
            for (int j = 0; j < maxY; j++) {
                AllTiles[i, j] = new GridTile(i,j);
            }
        }

        var gridSpriteObj = Instantiate(GridBackgroundPrefab, this.transform);
        gridSpriteObj.GetComponent<SpriteRenderer>().size = new Vector2(maxX,maxY);

    }

    public GridTile ScreenPointToGridTile(Vector2 pos) {
        var point = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, cam.nearClipPlane));
        return GetValidTileFromWorldPoint(point.x, point.y);
    }

    private GridTile GetValidTileFromWorldPoint(float x,float y) {
        Vector2 ManagerPos = this.transform.position;

        var nx = Mathf.FloorToInt(x - ManagerPos.x);
        var ny = Mathf.FloorToInt(y - ManagerPos.y);
        return InBoundGridTile(nx,ny);

    }

    private GridTile InBoundGridTile(int ix,int iy) {
        int validX = ix < 0 ? 0 : (ix >= maxX ? maxX-1 : ix);
        int validY = iy < 0 ? 0 : (iy >= maxY ? maxY-1 : iy);

        return AllTiles[validX, validY];
    }

    private bool InBounds(Vector2 Pos) {
        if (Pos.x < 0 || Pos.x >= maxX || Pos.y < 0 || Pos.y >= maxY) return false;
        else return true;
    }

    public bool CheckGridRegion(GridTile startTile,Vector2 size) {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                Vector2 cell = new Vector2(i,j) + startTile.CellPos();
                
                if (InBounds(cell)) {
                    if (AllTiles[(int)cell.x, (int)cell.y].isOccupied) return false;
                } else return false;
            }
        }
        return true;
    }

    public void SetGridRegionOccupied(GridTile startTile, Vector2 size) {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                AllTiles[(int)startTile.CellPos().x+i, (int)startTile.CellPos().y+j].isOccupied = true;
            }

        }
    }

    public Vector2 GetTileTransformPosition(GridTile tile) {
        return tile.CellPos() + (Vector2)this.transform.position;
    }

    public List<GridTile> GetNeigh(GridTile center) {
        List<GridTile> neighs = new List<GridTile>();
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i == 0 && j == 0) continue;

                Vector2 checkPos = center.CellPos() + new Vector2(i,j);
                if (InBounds(checkPos)) {
                    neighs.Add(AllTiles[(int)checkPos.x, (int)checkPos.y]);
                }
            }
        }

        return neighs;
    }

    public List<GridTile> GetPath(GridTile start, GridTile end) {
        return Pather.FindPath(start,end);
    }

}

public class GridTile {
    public readonly int gx;
    public readonly int gy;
    public bool isOccupied { get; set; }

    public GridTile cameFrom;
    public int gCost;
    public int hCost;

    public GridTile(int x, int y) {
        gx = x;
        gy = y;
        isOccupied = false;
    }

    public Vector2 CellPos() {
        return new Vector2(gx, gy);
    }

    public int fCost {
        get { return gCost + hCost;}
    }

    

}
