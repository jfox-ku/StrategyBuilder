using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{

    public bool IsInputDisabled;
    public event Action<Vector2> InputEvent;
    public event Action<Vector2> InputDownEvent;
    public event Action<Vector2> InputUpEvent;
    public event Action<Vector2> SecondaryInputDownEvent;

    public virtual void OnInput(Vector2 pos)
    {
        InputEvent?.Invoke(pos);
    }

    public virtual void OnInputDown(Vector2 pos)
    {
        InputDownEvent?.Invoke(pos);
    }

    public virtual void OnInputUp(Vector2 pos)
    {
        InputUpEvent?.Invoke(pos);
    }

    public virtual void OnSecondaryInputDown(Vector2 pos) {
        
        SecondaryInputDownEvent?.Invoke(pos);
    }
    
    public bool IsOverUiElement()
    {
        return EventSystem.current.IsPointerOverGameObject();

    }
}
