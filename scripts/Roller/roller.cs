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
    public BoxCollider2D boxCollider;  // BoxCollider2D组件
    public float speed = 0.2f;         // 移动速度
    public MovementDirection direction = MovementDirection.Left; // 默认移动方向

    void Start()
    {
        // 确保游戏对象有BoxCollider2D组件
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        // 设置为触发器
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
                // 计算移动向量
                Vector2 moveDirection = GetMovementDirection();
                // 应用移动效果
                other.transform.Translate(moveDirection * speed * Time.deltaTime);
            }
        }
      
        else
        {
            // 计算移动向量
            Vector2 moveDirection = GetMovementDirection();
            // 应用移动效果
            other.transform.Translate(moveDirection * speed * Time.deltaTime);
        }
       
       
    }

    Vector2 GetMovementDirection()
    {
        // 根据枚举值返回对应的向量
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
