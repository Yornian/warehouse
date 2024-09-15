using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 使其在Unity Inspector中可见
public struct ItemInfo
{
    public string itemName;  // Now using a string to reference the item by name
    public bool broken;
 
    public ItemInfo(string itemName, bool broken)
    {
        this.itemName = itemName;
        this.broken = broken;
    }
}

public class StorageBox : MonoBehaviour, IInteractable // 设置物品list清单 系统 多发和少发的。
{
    private Animator animator; // 动画控制器
    public string itemName;
    private Rigidbody2D rb;
    private string prefabFolderPath = "Prefab/Item/";
    private Transform SpawnPointTransform;
    private Transform spritePic;
    public bool isFull ;
    public bool open = false;

    public float triggerSpeed = 0.13f;  // 定义速度阈值
    public float DropProbability = 0.0f;// 定义掉落几率
    public float DropProbabilityPerItem = 0.08f;
    public float MassPerItem = 2.0f;
    public float timeInterval = 0.5f;  // 检测间隔时间，单位秒
    private float timeSinceLastCheck = 0;  // 自上次检查以来经过的时间

    public Dictionary<string, int> ItemOrder = new Dictionary<string, int>();
    public LinkedList<ItemInfo> ItemList = new LinkedList<ItemInfo>(); // Using LinkedList of ItemInfo

    public void PerformInteractionClick(GameObject Player)
    {

    }

    public void PerformInteractionF(GameObject Player)
    {
        openOrCloseBox();
    }

    public void init(LinkedList<ItemInfo> iItemList,bool ifOpen,bool ifFull)
    {
        ItemList = iItemList;
        open = ifOpen;
        isFull = ifFull;
    }
    public void PerformInteractionLongPress(GameObject Player)
    {

    }

    public void PerformInteraction(GameObject Player)
    {
        PlayerControler PlayerControlerScript = Player.GetComponent<PlayerControler>();
        putOrGetItem(PlayerControlerScript);
    }

    public void clearItemCollision(GameObject item)
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
    }

    public void putOrGetItem(PlayerControler PlayerControlerScript)
    {
        if (open)
        {
            if (PlayerControlerScript.pickedItem != null) // put item in
            {
                Vector3 putPos = SpawnPointTransform.position;
                putPos.y -= 0.1f;
                clearItemCollision(PlayerControlerScript.pickedItem);
                AddNewItem(new ItemInfo(PlayerControlerScript.pickedItem.GetComponent<item>().name, PlayerControlerScript.pickedItem.GetComponent<item>().ifBroken));
                PlayerControlerScript.dropItemInPos(putPos, false, true);

                // Add the item with its properties to the listde
          
           
                
                setBoxIsFull();
            }
            else // get item out
            {
                setBoxIsFull();
                if (IsBoxEmpty())
                {
                    // Box is empty, nothing to retrieve
                }
                else
                {
                    StartCoroutine(LetPlayerPickTheItem(PlayerControlerScript, SpawnPointTransform));
                    setBoxIsFull();
                }
            }
        }
    }

    public void openOrCloseBox()
    {
        animator.SetBool("open", !open);
        open = !open;
    }

    public void setBoxIsFull()
    {
        isFull = !IsBoxEmpty();
        animator.SetBool("isFull", isFull);
    }

    // 检查ItemList是否为空
    public bool IsBoxEmpty()
    {
        return ItemList.Count == 0;
    }

    public void bePickUp(PlayerControler PlayerControlerScript)
    {
        if (PlayerControlerScript.pickedItem == null)
        {
            PlayerControlerScript.pickItemToHold(this.gameObject);
        }
        else
        {
            PlayerControlerScript.dropItem();
        }
    }

    void Start()
    {
        if (IsBoxEmpty())
        {
            isFull = false;
        }
        SpawnPointTransform = transform.Find("point");
        spritePic = transform.Find("sprite");
        rb = GetComponent<Rigidbody2D>();
        animator = spritePic.GetComponent<Animator>();
        animator.SetBool("isFull", isFull);
        animator.SetBool("open", open);
        timeInterval = getRandomTimeInterval();
    }

    public GameObject LoadPrefabWithName(string prefabName)
    {
        string path = prefabFolderPath + prefabName;
        GameObject Prefab = Resources.Load<GameObject>(path);
        return Prefab;
    }

    public IEnumerator LetPlayerPickTheItem(PlayerControler PlayerControlerScript, Transform spawnPoint)
    {
        (string itemName, bool broken) = GetLastItemAndDecrement();
        GameObject instance = Instantiate(LoadPrefabWithName(itemName), spawnPoint.position, spawnPoint.rotation);

        yield return new WaitForSeconds(0.01f);
        instance.GetComponent<item>().setIfBroken(broken);
        yield return new WaitForSeconds(0.2f);

        PlayerControlerScript.spawnItemToHold(instance);
    }

    // Update is called once per frame
    void Update()
    {
        fallItemWhenMoveWithoutPack();
    }

    void AddNewItem(ItemInfo itemInfo)
    {
        ItemList.AddLast(itemInfo);
        rb.mass += MassPerItem;
        changeDropProbability(DropProbabilityPerItem);
    }

    private void changeDropProbability(float value)
    {
        DropProbability += value;
        if (DropProbability > 0.8f)
        {
            DropProbability = 0.8f;
        }
        if (DropProbability < 0.0f)
        {
            DropProbability = 0.0f;
        }
    }

    private void setDropProbability() // 需要修改
    {
        DropProbability += DropProbabilityPerItem;
        if (DropProbability > 0.8f)
        {
            DropProbability = 0.8f;
        }
        if (DropProbability < 0.0f)
        {
            DropProbability = 0.0f;
        }
    }

    // 返回最后一个元素的item，并将其数量减1
    public (string itemName, bool broken) GetLastItemAndDecrement()
    {
        if (IsBoxEmpty())
        {
            return (null, false); // Return default values if the box is empty
        }

        ItemInfo lastItem = ItemList.Last.Value;
        ItemList.RemoveLast();

        rb.mass -= MassPerItem;
        changeDropProbability(-DropProbabilityPerItem);

        return (lastItem.itemName, lastItem.broken); // Return both the item name and broken status
    }

    private float getRandomTimeInterval()
    {
        float min = 0.5f;
        float max = 1.8f;
        float randomValue = Mathf.Pow(Random.value, 0.5f); // 使用幂次变换
        float randomValueInRange = min + (max - min) * randomValue;
        return randomValueInRange;
    }

    private void fallItemWhenMoveWithoutPack()
    {
        if (rb.velocity.magnitude > triggerSpeed)
        {
            // 如果物体移动，累积时间
            timeSinceLastCheck += Time.deltaTime;

            if (timeSinceLastCheck >= timeInterval)
            {
                timeSinceLastCheck = 0;

                float i = Random.value;

                if (i < DropProbability)
                {
                  
                    timeInterval = getRandomTimeInterval();
                    timeSinceLastCheck = 0;
                    StartCoroutine(dropItemToGround());
                }
            }
        }
    }

    public Vector2 GenerateRandomVector()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        return new Vector2(x, y);
    }

    public IEnumerator dropItemToGround() // 掉落物品
    {
        if (open == false)
        {
            openOrCloseBox();
        }

        Vector2 MoveVector = GenerateRandomVector();
        (string itemName, bool broken) = GetLastItemAndDecrement();
        GameObject prefab = LoadPrefabWithName(itemName);
      
        setBoxIsFull();
        if (prefab == null)
        {
            // No item to drop
        }
        else
        {
            GameObject item = Instantiate(prefab, SpawnPointTransform.position, SpawnPointTransform.rotation);
            
            float throwFor = 200.0f;

            item.transform.rotation = Quaternion.identity; // 重置旋转

            yield return new WaitForSeconds(0.01f);
            item.GetComponent<item>().setIfBroken(broken);
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.AddForce(MoveVector * throwFor);
            yield return new WaitForSeconds(0.3f);

            item.GetComponent<item>().setSelfCollision(true);
        }
    }

    public Dictionary<string, int> countItem()
    {
        Dictionary<string, int> itemCount = new Dictionary<string, int>();
        foreach (ItemInfo itemInfo in ItemList)
        {
            if (itemCount.ContainsKey(itemInfo.itemName))
            {
                itemCount[itemInfo.itemName]++;
            }
            else
            {
                itemCount[itemInfo.itemName] = 1;
            }
        }
        return itemCount;
    }

    bool AreDictionariesEqual(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
    {
        // Check if both dictionaries have the same number of elements
        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        // Check if all key-value pairs in dict1 match those in dict2
        foreach (var kvp in dict1)
        {
            // Check if dict2 contains the key and the same value
            if (!dict2.TryGetValue(kvp.Key, out int value) || value != kvp.Value)
            {
                return false;
            }
        }

        return true;
    }

    Dictionary<string, int> FindItemsLess(Dictionary<string, int> itemCount, Dictionary<string, int> ItemOrder)
    {
        Dictionary<string, int> itemsLess = new Dictionary<string, int>();

        foreach (var kvp in ItemOrder)
        {
            if (!itemCount.TryGetValue(kvp.Key, out int countInItemCount) || countInItemCount < kvp.Value)
            {
                itemsLess[kvp.Key] = kvp.Value - countInItemCount;
            }
        }

        return itemsLess;
    }

    Dictionary<string, int> FindItemsMore(Dictionary<string, int> itemCount, Dictionary<string, int> ItemOrder)
    {
        Dictionary<string, int> itemsMore = new Dictionary<string, int>();

        foreach (var kvp in itemCount)
        {
            if (!ItemOrder.TryGetValue(kvp.Key, out int countInItemOrder) || kvp.Value > countInItemOrder)
            {
                itemsMore[kvp.Key] = kvp.Value - countInItemOrder;
            }
        }

        return itemsMore;
    }
    public bool CheckIfOrderIsFulfilled()
    {
        Dictionary<string, int> itemCount = countItem(); // 获取 StorageBox 中的物品计数
        foreach (List<ItemStruct> order in OrderManager.Instance.listOfOrders)
        {
            bool isOrderFulfilled = false;

            foreach (ItemStruct item in order)
            {
                
                if (itemCount.ContainsKey(item.itemName) && itemCount[item.itemName] == item.itemNum)
                {
                    isOrderFulfilled = true;
                    break; // 如果某个订单项无法满足，则跳出当前订单检查
                }
            }

            if (isOrderFulfilled)
            {
                return true; // 如果找到一个满足的订单，则返回 true
            }
        }

        return false; // 如果没有满足的订单，则返回 false
    }
    public LinkedList<ItemInfo> CheckBadItem()
  {
        LinkedList<ItemInfo> badItems = new LinkedList<ItemInfo>();

        foreach (ItemInfo item in ItemList)
        {
            if (item.broken)
            {
                badItems.AddLast(item);
            }
        }

        return badItems;
    }

}
