using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{ 
    enum InputContext{Ghost, Unit, Building, Empty }

    InputManager InputMan;

    GridManager GridMan;

    ObjectPoolManager PoolMan;

    private GameObject CurrentSelection;

    private void Start() {
        InputMan = FindObjectOfType<InputManager>();
        GridMan = FindObjectOfType<GridManager>();
        PoolMan = FindObjectOfType<ObjectPoolManager>();


        InputMan.InputDownEvent += PrimaryInputDown;
        InputMan.SecondaryInputDownEvent += SecondaryInputDown;
    }

    private void OnDestroy() {
        InputMan.InputDownEvent -= PrimaryInputDown;
        InputMan.SecondaryInputDownEvent -= SecondaryInputDown;
    }



    public void PrimaryInputDown(Vector2 pos) {
        //Debug.Log(InputMan.IsOverUiElement());
        //PoolMan.GetFromPoolString("Barracks");
        //if (CurrentSelection != null) return;
        var unit = PoolMan.GetFromPoolString("Soldier").GetComponent<UnitBase>();
        GridTile tile = GridMan.ScreenPointToGridTile(pos);
        if (!tile.isOccupied) {
            unit.InitializeAt(tile);
            CurrentSelection = unit.gameObject;
        } else {
            PoolMan.ReturnToPool(unit.gameObject,unit);
            CurrentSelection = null;
        }
        
    }
    public void SecondaryInputDown(Vector2 pos) {
        GridTile g = GridMan.ScreenPointToGridTile(pos);
        UnitBase unit;
        if(unit = CurrentSelection.GetComponent<UnitBase>()) {
            unit.MoveTo(g);
        }


        Debug.Log(g.CellPos());
    }


    public void BuildingGhostSelected(string poolName) {
        
        if(CurrentSelection!=null) {
            ClearSelection();
        }
        CurrentSelection = PoolMan.GetFromPoolString(poolName);


    }

    public void ProduceUnitClicked(string unitPoolName) {
        BuildingBase sel = CurrentSelection.GetComponent<BuildingBase>();
        UnitBase unit = PoolMan.GetFromPoolString(unitPoolName).GetComponent<UnitBase>();
        if (sel != null && unit!=null) {
            if (sel.CanProduceFromPool(unitPoolName)) {

            } else {
                PoolMan.ReturnToPool(unit.gameObject,unit);
            }

        }

    }

    public void GOSelected(GameObject go) {
        ClearSelection();
        CurrentSelection = go;
    }

    public void ClearSelection() {
        IPoolable sel;
        if (CurrentSelection != null) {
            if((sel = CurrentSelection.GetComponent<IPoolable>()) != null) {
                BuildingBase baseBuild;
                if((baseBuild = CurrentSelection.GetComponent<BuildingBase>())) {
                    if (baseBuild.isGhost()) {
                        PoolMan.ReturnToPool(CurrentSelection, sel);
                        CurrentSelection = null;
                    }

                }
            }
            CurrentSelection = null;

        }
    }

    

   
}
