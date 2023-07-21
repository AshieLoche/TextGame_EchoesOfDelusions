using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private Texture2D IdleState;
    [SerializeField]
    private Texture2D HoverState;
    [SerializeField]
    private Texture2D PressedState;
    public static bool isDown = false;
    public static bool isInteractable = true;

    private void OnMouseEnter()
    {
        if (isDown)
            Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(HoverState, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        isDown = true;
    }

    private void OnMouseUpAsButton()
    {
        Cursor.SetCursor(HoverState, Vector2.zero, CursorMode.Auto);
        isDown = false;
    }

    private void OnMouseUp()
    {
        if (isInteractable)
            Cursor.SetCursor(IdleState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(HoverState, Vector2.zero, CursorMode.Auto);

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        isDown = false;
    }

    private void OnMouseExit()
    {
        if (isDown)
            Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(IdleState, Vector2.zero, CursorMode.Auto);
    }
}