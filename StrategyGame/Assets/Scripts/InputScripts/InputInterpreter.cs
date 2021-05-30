using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{
    InputManager InputMan;

    GridManager GridMan;

    private GameObject CurrentSelection;

    private void Start() {
        InputMan = FindObjectOfType<InputManager>();
        GridMan = FindObjectOfType<GridManager>();

        InputMan.InputDownEvent += PrimaryInputDown;
        InputMan.SecondaryInputDownEvent += SecondaryInputDown;
    }

    private void OnDestroy() {
        InputMan.InputDownEvent -= PrimaryInputDown;
        InputMan.SecondaryInputDownEvent -= SecondaryInputDown;
    }



    public void PrimaryInputDown(Vector2 pos) {
        //Debug.Log(InputMan.IsOverUiElement());
        if (CurrentSelection != null) {


        }
    }

    public void SecondaryInputDown(Vector2 pos) {
        Vector2 g = GridMan.ScreenPointToGridTile(pos).CellPos();
        Debug.Log(g);
    }

    public void SelectableSelected() {

    }

}
