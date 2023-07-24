using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private Texture2D idleState;
    [SerializeField]
    private Texture2D hoverState;
    [SerializeField]
    private Texture2D pressedState;
    private static bool isDown = false;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(hoverState, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseOver()
    {
        if (isDown)
            Cursor.SetCursor(pressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(hoverState, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseDown()
    {
        Cursor.SetCursor(pressedState, Vector2.zero, CursorMode.Auto);
        isDown = true;
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(idleState, Vector2.zero, CursorMode.Auto);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        isDown = false;
    }

    private void OnMouseUpAsButton()
    {
        Cursor.SetCursor(hoverState, Vector2.zero, CursorMode.Auto);
        isDown = false;
    }

    private void OnMouseExit()
    {
        if (isDown)
            Cursor.SetCursor(pressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(idleState, Vector2.zero, CursorMode.Auto);
    }
}