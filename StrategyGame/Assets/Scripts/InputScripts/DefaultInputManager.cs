using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultInputManager : InputManager
{
    private bool _hasCatchedOnInputDown;

    public void Update()
    {
        InputControl();
    }

    private void InputControl()
    {
        if (IsInputDisabled)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnInputUp(GetInputPosition());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            OnInputDown(GetInputPosition());
        }
        else if (Input.GetMouseButton(0))
        {
            OnInput(GetInputPosition());
        }

        if (Input.GetMouseButtonDown(1)) {
            OnSecondaryInputDown(GetInputPosition());
        }

    }

    public Vector2 GetInputPosition()
    {
        return Input.mousePosition;

    }
    

    public override void OnInput(Vector2 pos)
    {
        if (!_hasCatchedOnInputDown)
        {
            return;
        }

        base.OnInput(pos);
    }

    public override void OnInputDown(Vector2 pos)
    {
        if (IsOverUiElement())
        {
            return;
        }
        _hasCatchedOnInputDown = true;

        base.OnInputDown(pos);
    }

    public override void OnInputUp(Vector2 pos)
    {
        if (!_hasCatchedOnInputDown)
        {
            return;
        }
        _hasCatchedOnInputDown = false;

        base.OnInputUp(pos);
    }

    public override void OnSecondaryInputDown(Vector2 pos) {
        base.OnSecondaryInputDown(pos);
    }

}