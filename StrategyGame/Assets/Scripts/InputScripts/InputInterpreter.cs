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
    }

    public void BuildingGhostSelected(string poolName) {
        Debug.Log("Ghost requested: "+poolName);
        if(CurrentSelection!=null) {
            ClearSelection();
        }
        CurrentSelection = PoolMan.GetFromPoolString(poolName);


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

    public void SecondaryInputDown(Vector2 pos) {
        Vector2 g = GridMan.ScreenPointToGridTile(pos).CellPos();
        Debug.Log(g);
    }

   
}
