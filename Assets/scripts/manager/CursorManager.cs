using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursor; // ��ͨ״̬
    public Texture2D clickCursor;  // ���״̬
    public Texture2D dragCursor;   // ��ק״̬
    public Vector2 hotSpot = Vector2.zero; // �����ȵ�λ�ã�Ĭ������Ϊ(0,0)

    private bool isDragging = false;

    void Start()
    {
        // ��ʼ����Ϊ��ͨ״̬
        SetCursor(normalCursor);
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(clickCursor);
        }

        // ����������ͷ�
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // ������ק״̬
            SetCursor(normalCursor);
        }

        // ����������������²������ƶ�
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
