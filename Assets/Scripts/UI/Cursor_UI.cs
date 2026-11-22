using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;

    // The click point offset from the top-left of the texture
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        SetToDefaultCursor();
    }

    public void SetToHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
    }

    public void SetToDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }
}