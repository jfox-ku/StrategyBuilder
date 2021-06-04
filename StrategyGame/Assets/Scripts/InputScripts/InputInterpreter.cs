using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{ 
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
        
        
    }
    public void SecondaryInputDown(Vector2 pos) {
        if (CurrentSelection == null) return;

        GridTile g = GridMan.ScreenPointToGridTile(pos);
        UnitBase unit;
        if(unit = CurrentSelection.GetComponent<UnitBase>()) {
            unit.MoveTo(g);
        }

        BuildingBase build;
        if(build = CurrentSelection.GetComponent<BuildingBase>()) {
            if (!g.isOccupied) {
                build.SetSpawnDestination(g);
            }
        }
    }


    public void BuildingGhostSelected(string poolName) {
        
        if(CurrentSelection!=null) {
            ClearSelection();
        }
        CurrentSelection = PoolMan.GetFromPoolString(poolName);


    }

    public void ProduceUnitClicked(string unitPoolName) {
        BuildingBase sel = CurrentSelection.GetComponent<BuildingBase>();
        if (sel != null) {
            if (sel.CanProduceFromPool(unitPoolName)) {
                sel.ProduceUnit(unitPoolName);
            }

        } else {
            Debug.Log("Selection can not produce units.");
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
