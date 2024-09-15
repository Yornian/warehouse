using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class pick : MonoBehaviour
{
    //private GameObject pickedItem = null;
    private itemAnimControllor pickedItemAnimControllor;
    public Vector2 lastMotionVector;
    public bool canUpdate = false;
    private float throwFor = 15.0f;
    float pickDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // 设置 Gizmo 的颜色为红色
        Vector3 pos = transform.position;
        pos.y -= 0.45f;
        pos.x += 0.15f;
        Gizmos.DrawWireCube(pos, new Vector2(0.4f, 0.5f)); // 绘制一个线框矩形
    }
    public GameObject tryPick(Vector3 position, Vector2 direction, GameObject pickedItem, float pickDistance = 1f)
    {
        // 绘制射线以便在Unity编辑器中可视化
        //Debug.DrawRay(position, direction * pickDistance, Color.red, 1.0f);

        // 计算 LayerMask 以排除 "Ignore Raycast" 层和其他必要的层
        int ignoreRaycastLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        int itemLayerMask = LayerMask.GetMask("item");
        int floorLayerMask = 1 << LayerMask.NameToLayer("floor"); // 新增：获取 "floor" 层的 LayerMask

        // 生成最终的 LayerMask，总是排除 "Ignore Raycast" 和 "floor" 层，
        // 如果正在拿东西，也排除 "item" 层
        int layerMask = ~(ignoreRaycastLayerMask | floorLayerMask); // 同时排除 "Ignore Raycast" 和 "floor" 层
        if (pickedItem != null)
        {
            layerMask &= ~itemLayerMask; // 同时排除 "item" 层
        }

        // 进行射线检测
        RaycastHit2D hit = Physics2D.Raycast(position, direction, pickDistance, layerMask);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;  // 如果检测到了有效的物体，返回该物体
        }
        else if (pickedItem != null)
        {
            return pickedItem;  // 如果没有检测到其他物体而且正在拿东西，返回被拿起的物品
        }

        return null;  // 如果什么都没检测到，返回null
    }



    //    public GameObject tryPick(Vector3 position, Vector2 MoveVector,GameObject pickedItem)
    //{
    //    // Define the angle of the rectangle (0 for no rotation)
    //    float angle = 0f;

    //        Vector2 boxSize = new Vector2(0.4f, 0.5f);

    //        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, boxSize, angle);




    //    Collider2D closestItemInFront = null;
    //    float closestDistanceInFront = float.MaxValue;
    //    Collider2D closestItemOverall = null;
    //    float closestDistanceOverall = float.MaxValue;

    //    Vector2 direction = MoveVector.normalized; // Determine direction using MoveVector
    //        if (MoveVector == new Vector2(0, -1) && pickedItem!=null&& colliders.Length>1)
    //        {


    //            colliders= colliders.Where(collider => collider.gameObject != pickedItem).ToArray();
    //        }
    //        foreach (Collider2D collider in colliders)
    //    {
    //        // Skip the collider if it belongs to this GameObject
    //        if (collider.gameObject == this.gameObject)
    //        {
    //            continue;
    //        }

    //        Vector2 toItem = collider.transform.position - position;
    //        float distance = toItem.sqrMagnitude; // Use squared distance to avoid overhead

    //        // Prioritize items in the character's direction
    //        if (Vector2.Dot(toItem.normalized, direction) > 0.3f) // Adjust this value to define direction strictness
    //        {
    //            if (distance < closestDistanceInFront)
    //            {
    //                closestItemInFront = collider;
    //                closestDistanceInFront = distance;
    //            }
    //        }

    //        // Also record the closest item overall
    //        if (distance < closestDistanceOverall)
    //        {
    //            closestItemOverall = collider;
    //            closestDistanceOverall = distance;
    //        }
    //    }

    //    // Try to pick the closest item in front
    //    if (closestItemInFront != null)
    //    {
    //        return closestItemInFront.gameObject;
    //    }
    //    else if (closestItemOverall != null)
    //    {
    //        return closestItemOverall.gameObject;
    //    }

    //    return null; // Return null if no items can be picked up
    //}

    public void pickItemToHold(GameObject item,Transform holdTransform, Vector2 MoveVector)//捡了之后一直拿着 
        

    {
       
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // 清除所有现有动量
            rb.angularVelocity = 0;  // 清除旋转动量
            rb.isKinematic = true;  // 停止物理计算

        }
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;  // 将碰撞体设置为触发器
        }
        item.transform.SetParent(holdTransform, false);  // 将物品设为子对象

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity; // 重置局部旋转
      //  item.transform.localScale = new Vector3(1, 1, 1); // 设置局部缩放为1
        pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        pickedItemAnimControllor.setBeHold(true);                //更改动画蓝图值
        updateHoldItemPos(item, MoveVector);
    }

    public void updateHoldItemPos(GameObject pickedItem, Vector2 MoveVector)
    {
      
        if (pickedItem != null)
        {
                pickedItemAnimControllor.setMoveXAndMoveY(MoveVector);
          
        }

    }
    void pickItemToInventory()//捡了之后放背包、库存
    {



    }
    public void dropItemToGround(GameObject item, Vector2 MoveVector,float distance)    //扔到玩家脚部位置
    {

        item.transform.SetParent(null);  // 取消父对象
        SpriteRenderer spriteRenderer = item.transform.Find("itemSprite").GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("物体缺少SpriteRenderer组件！");
            return;
        }
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float itemLengthInMoveDirection = Mathf.Abs(Vector2.Dot(spriteSize, MoveVector.normalized));
        //item 的长度，在移动方向上
        Vector3 dropPosition = item.transform.position + new Vector3(MoveVector.x, MoveVector.y, 0).normalized *( itemLengthInMoveDirection+distance);
        item.transform.position = dropPosition;//开始掉落的位置 
        item.transform.rotation = Quaternion.identity; // 重置旋转
        //item.transform.localScale = new Vector3(1, 1, 1); // 重置缩放
        pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        if (pickedItemAnimControllor != null)
        {
            pickedItemAnimControllor.setBeHold(false);  // 更改动画蓝图值
        }


        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;  // 恢复物理运动
          
        }
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;  // 恢复碰撞体
        }
        rb.AddForce(Vector2.down * throwFor);


    }
    public void dropItemToSpecifiedPosition(GameObject item, Vector3 position,bool ifOpenCollider )
    {
        item.transform.SetParent(null);  // 取消父对象

        SpriteRenderer spriteRenderer = item.transform.Find("itemSprite").GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("物体缺少SpriteRenderer组件！");
            return;
        }

        item.transform.position = position; // 设置物品到指定位置
        item.transform.rotation = Quaternion.identity; // 重置旋转

        itemAnimControllor pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        if (pickedItemAnimControllor != null)
        {
            pickedItemAnimControllor.setBeHold(false);  // 更改动画蓝图值
        }

        if(ifOpenCollider)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;  // 恢复物理运动
            }

            Collider2D collider = item.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = false;  // 恢复碰撞体
            }
        }
       

       
    }

}
