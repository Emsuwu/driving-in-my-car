using System;
using UnityEngine;

public class MouseType : MonoBehaviour
{
    public enum CURSOR { Pointer, SlideArrow };

    public Texture2D CPointer;
    public Texture2D CSlideArrow;
    public static MouseType Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetCursor(CURSOR cursor)
    {
        switch (cursor)
        {
            case CURSOR.Pointer:
                SetCursor(Instance.CPointer);
                break;
            case CURSOR.SlideArrow:
                SetCursor(Instance.CSlideArrow);
                break;
            default:
                SetCursor(Instance.CPointer);
                break;
        }
    }
    public static void SetCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    public static void Pointer()
    {
        SetCursor(Instance.CPointer);
    }
    public static void SlideArrow()
    {
        SetCursor(Instance.CSlideArrow);
    }
}
