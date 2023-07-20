using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MouseManagement : MonoBehaviour
{
    [SerializeField]
    private Texture2D IdleState;
    [SerializeField]
    private Texture2D HoverState;
    [SerializeField]
    private Texture2D PressedState;
    [SerializeField]
    private bool isDown = false;

    private void OnMouseEnter()
    {
        if (FindObjectOfType<MouseManagement>().isDown)
            Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(HoverState, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        FindObjectOfType<MouseManagement>().isDown = true;
    }

    private void OnMouseUpAsButton()
    {
        Cursor.SetCursor(HoverState, Vector2.zero, CursorMode.Auto);
        FindObjectOfType<MouseManagement>().isDown = false;
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(IdleState, Vector2.zero, CursorMode.Auto);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        FindObjectOfType<MouseManagement>().isDown = false;
    }

    private void OnMouseExit()
    {
        if (FindObjectOfType<MouseManagement>().isDown)
            Cursor.SetCursor(PressedState, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(IdleState, Vector2.zero, CursorMode.Auto);
    }
}