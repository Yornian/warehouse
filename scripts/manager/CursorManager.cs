using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursor; // 普通状态
    public Texture2D clickCursor;  // 点击状态
    public Texture2D dragCursor;   // 拖拽状态
    public Vector2 hotSpot = Vector2.zero; // 光标的热点位置，默认设置为(0,0)

    private bool isDragging = false;

    void Start()
    {
        // 初始设置为普通状态
        SetCursor(normalCursor);
    }

    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(clickCursor);
        }

        // 检测鼠标左键释放
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // 重置拖拽状态
            SetCursor(normalCursor);
        }

        // 检测鼠标左键持续按下并且有移动
        if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            if (!isDragging)
            {
                isDragging = true;
                SetCursor(dragCursor);
            }
        }
    }

    private void SetCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
    }
}
