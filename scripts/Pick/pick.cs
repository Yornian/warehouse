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
        Gizmos.color = Color.red; // ���� Gizmo ����ɫΪ��ɫ
        Vector3 pos = transform.position;
        pos.y -= 0.45f;
        pos.x += 0.15f;
        Gizmos.DrawWireCube(pos, new Vector2(0.4f, 0.5f)); // ����һ���߿����
    }
    public GameObject tryPick(Vector3 position, Vector2 direction, GameObject pickedItem, float pickDistance = 1f)
    {
        // ���������Ա���Unity�༭���п��ӻ�
        //Debug.DrawRay(position, direction * pickDistance, Color.red, 1.0f);

        // ���� LayerMask ���ų� "Ignore Raycast" ���������Ҫ�Ĳ�
        int ignoreRaycastLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        int itemLayerMask = LayerMask.GetMask("item");
        int floorLayerMask = 1 << LayerMask.NameToLayer("floor"); // ��������ȡ "floor" ��� LayerMask

        // �������յ� LayerMask�������ų� "Ignore Raycast" �� "floor" �㣬
        // ��������ö�����Ҳ�ų� "item" ��
        int layerMask = ~(ignoreRaycastLayerMask | floorLayerMask); // ͬʱ�ų� "Ignore Raycast" �� "floor" ��
        if (pickedItem != null)
        {
            layerMask &= ~itemLayerMask; // ͬʱ�ų� "item" ��
        }

        // �������߼��
        RaycastHit2D hit = Physics2D.Raycast(position, direction, pickDistance, layerMask);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;  // �����⵽����Ч�����壬���ظ�����
        }
        else if (pickedItem != null)
        {
            return pickedItem;  // ���û�м�⵽����������������ö��������ر��������Ʒ
        }

        return null;  // ���ʲô��û��⵽������null
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

    public void pickItemToHold(GameObject item,Transform holdTransform, Vector2 MoveVector)//����֮��һֱ���� 
        

    {
       
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // ����������ж���
            rb.angularVelocity = 0;  // �����ת����
            rb.isKinematic = true;  // ֹͣ�������

        }
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;  // ����ײ������Ϊ������
        }
        item.transform.SetParent(holdTransform, false);  // ����Ʒ��Ϊ�Ӷ���

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity; // ���þֲ���ת
      //  item.transform.localScale = new Vector3(1, 1, 1); // ���þֲ�����Ϊ1
        pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        pickedItemAnimControllor.setBeHold(true);                //���Ķ�����ͼֵ
        updateHoldItemPos(item, MoveVector);
    }

    public void updateHoldItemPos(GameObject pickedItem, Vector2 MoveVector)
    {
      
        if (pickedItem != null)
        {
                pickedItemAnimControllor.setMoveXAndMoveY(MoveVector);
          
        }

    }
    void pickItemToInventory()//����֮��ű��������
    {



    }
    public void dropItemToGround(GameObject item, Vector2 MoveVector,float distance)    //�ӵ���ҽŲ�λ��
    {

        item.transform.SetParent(null);  // ȡ��������
        SpriteRenderer spriteRenderer = item.transform.Find("itemSprite").GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("����ȱ��SpriteRenderer�����");
            return;
        }
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float itemLengthInMoveDirection = Mathf.Abs(Vector2.Dot(spriteSize, MoveVector.normalized));
        //item �ĳ��ȣ����ƶ�������
        Vector3 dropPosition = item.transform.position + new Vector3(MoveVector.x, MoveVector.y, 0).normalized *( itemLengthInMoveDirection+distance);
        item.transform.position = dropPosition;//��ʼ�����λ�� 
        item.transform.rotation = Quaternion.identity; // ������ת
        //item.transform.localScale = new Vector3(1, 1, 1); // ��������
        pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        if (pickedItemAnimControllor != null)
        {
            pickedItemAnimControllor.setBeHold(false);  // ���Ķ�����ͼֵ
        }


        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;  // �ָ������˶�
          
        }
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;  // �ָ���ײ��
        }
        rb.AddForce(Vector2.down * throwFor);


    }
    public void dropItemToSpecifiedPosition(GameObject item, Vector3 position,bool ifOpenCollider )
    {
        item.transform.SetParent(null);  // ȡ��������

        SpriteRenderer spriteRenderer = item.transform.Find("itemSprite").GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("����ȱ��SpriteRenderer�����");
            return;
        }

        item.transform.position = position; // ������Ʒ��ָ��λ��
        item.transform.rotation = Quaternion.identity; // ������ת

        itemAnimControllor pickedItemAnimControllor = item.transform.Find("itemSprite").GetComponent<itemAnimControllor>();
        if (pickedItemAnimControllor != null)
        {
            pickedItemAnimControllor.setBeHold(false);  // ���Ķ�����ͼֵ
        }

        if(ifOpenCollider)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;  // �ָ������˶�
            }

            Collider2D collider = item.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = false;  // �ָ���ײ��
            }
        }
       

       
    }

}
