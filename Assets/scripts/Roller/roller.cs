using UnityEngine;
public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Roller : MonoBehaviour
{
    public BoxCollider2D boxCollider;  // BoxCollider2D���
    public float speed = 0.2f;         // �ƶ��ٶ�
    public MovementDirection direction = MovementDirection.Left; // Ĭ���ƶ�����

    void Start()
    {
        // ȷ����Ϸ������BoxCollider2D���
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        // ����Ϊ������
        boxCollider.isTrigger = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<item>()!=null)
        {
            if (other.GetComponent<item>().beHold)
            {

            }
            else
            {
                // �����ƶ�����
                Vector2 moveDirection = GetMovementDirection();
                // Ӧ���ƶ�Ч��
                other.transform.Translate(moveDirection * speed * Time.deltaTime);
            }
        }
      
        else
        {
            // �����ƶ�����
            Vector2 moveDirection = GetMovementDirection();
            // Ӧ���ƶ�Ч��
            other.transform.Translate(moveDirection * speed * Time.deltaTime);
        }
       
       
    }

    Vector2 GetMovementDirection()
    {
        // ����ö��ֵ���ض�Ӧ������
        switch (direction)
        {
            case MovementDirection.Up:
                return Vector2.up;
            case MovementDirection.Down:
                return Vector2.down;
            case MovementDirection.Left:
                return Vector2.left;
            case MovementDirection.Right:
                return Vector2.right;
            default:
                return Vector2.right;
        }
    }
}
