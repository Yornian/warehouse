using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageReciver : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D  zone;
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     
        GameObject collidedObject = other.gameObject;
        if (collidedObject.GetComponent<StorageBox>()!=null)
        {
           
            if (collidedObject.GetComponent<StorageBox>().CheckIfOrderIsFulfilled())
            {
              if(IsLinkedListEmpty(collidedObject.GetComponent<StorageBox>().CheckBadItem()))
                {
                    GameManager.Instance.moneyToday += 100;
                    OrderManager.Instance.todayRightOrder++;
                    Destroy(other.gameObject);
                    Debug.Log("true");
                }
              else 
                {
                    OrderManager.Instance.AddItemListToFailedList(collidedObject.GetComponent<StorageBox>().CheckBadItem());
                    Destroy(other.gameObject);
                  
                }

               
            }
           else
            {
                OrderManager.Instance.AddItemListToFailedList(collidedObject.GetComponent<StorageBox>().ItemList);
                Destroy(other.gameObject);
           
            }

            

        }
       
    }
    public bool IsLinkedListEmpty(LinkedList<ItemInfo> list)
    {
        return list.Count == 0;
    }
}
