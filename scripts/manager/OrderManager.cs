using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct ItemStruct
{
    public string itemName;
    public int itemNum;
    public int itemPrice;
    public Sprite itemSprite;

    public ItemStruct(string name, Sprite sprite, int Num, int Price)
    {
        itemName = name;
        itemSprite = sprite;
        itemNum = Num;
        itemPrice = Price;
    }
}
public class OrderManager : MonoBehaviour    //和批发商谈合作 选择要合作的商品，合作的商品才会出现在order list里，
{
    public static OrderManager Instance;
 

    public List<ItemStruct> TotalItemList = new List<ItemStruct>();
    public List<ItemStruct> currentItemList = new List<ItemStruct>();
    public List<ItemStruct> BoughtItemList = new List<ItemStruct>();
    public List<ItemStruct> ownItemList = new List<ItemStruct>();
    public List<List<ItemStruct>> listOfOrders = new List<List<ItemStruct>>();
    private int currentOrder=0;
    public bool ifBought=false;
    private System.Random random;
    public uiManager UiManager;

  
    // Start is called before the first frame update

    public LinkedList<LinkedList<ItemInfo>> FailedItemList = new LinkedList<LinkedList<ItemInfo>>();



    public bool isWorking = false; // 玩家是否正在工作
    public float minOrderInterval = 25f; // 最短生成间隔（游戏分钟）
    public float maxOrderInterval = 60f;   // 最长生成间隔（游戏分钟）




    public int minutesInterval = 3; // 每隔几分钟（游戏时间）遍历下一个元素
    private LinkedListNode<LinkedList<ItemInfo>> currentFailedItemNode;
    public bool isWorkingForRefund = false; // 玩家是否正在处理退货

    
    public GameObject StorageBox;
    public int todayRightOrder=0;
 
    private OrderManager() { }

    // 初始化时确保只有一个实例
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        random = new System.Random();
       
       
      
      


     
      
    }
  
    public void showWarn(string txt)
    {

        UiManager = FindObjectOfType<uiManager>();
        UiManager.showWarn(txt);
    }
    public void InitTotalListInUI()
    {
        UiManager = FindObjectOfType<uiManager>();
        UiManager.InitMarketTotalListInUI(TotalItemList);
        //  GameObject marketGameObject = GameObject.Find("market");
      
        //marketGameObject.GetComponent<Scroll>().ClearContent();
        //if (TotalItemList != null && TotalItemList.Count != 0)
        //{
        //    foreach (ItemStruct item in TotalItemList)
        //    {
        //        marketGameObject.GetComponent<Scroll>().AddItemUIPrefab(item);

        //    }
        //}
      
        //

    }
    public void buyItem(ItemStruct newItem)
    {
        AddItemToList(newItem, ownItemList);
        AddItemToList(newItem, BoughtItemList);
      
       
    }

   public void DeliverGoods()
    {

        bool isEmpty = BoughtItemList == null || BoughtItemList.Count == 0;
        if(!isEmpty)
        {

            generateStorageBoxBought(ConvertBoughtItemsToItemInfoList(BoughtItemList));
            BoughtItemList = new List<ItemStruct>();
        }
        

    }
    public int GetRandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }
    public void AddItemToList(ItemStruct newItem, List<ItemStruct> targetList)
    {
        // 检查目标列表中是否已经有相同的商品
        var existingItem = targetList.Find(item => item.itemName == newItem.itemName);

        if (existingItem.itemName != null)
        {
            // 如果已存在，增加商品数量
            int index = targetList.FindIndex(item => item.itemName == newItem.itemName);
            targetList[index] = new ItemStruct(existingItem.itemName, existingItem.itemSprite, existingItem.itemNum + newItem.itemNum, existingItem.itemPrice);
        }
        else
        {
            // 如果不存在，直接添加
            targetList.Add(newItem);
        }
    }
    //public void AddItemToList(ItemStruct newItem, List<ItemStruct> targetList)
    //{
        
    //        targetList.Add(newItem);
        
    //}

    // 从链表中移除项
    public void RemoveItemFromList(ItemStruct itemToRemove, List<ItemStruct> targetList)
    {
        targetList.RemoveAll(item => item.itemName == itemToRemove.itemName);
    }

    // 获取随机商品列表的函数
    public List<ItemStruct> getRandomItemsOrder(int minQuantity, int maxQuantity, int numberOfItems)
    {
        List<ItemStruct> randomItemsOrder = new List<ItemStruct>();
        int currentItemCount = currentItemList.Count;

        if (currentItemCount == 0 || numberOfItems <= 0)
        {
            return randomItemsOrder;
        }

        System.Random rand = new System.Random();

        for (int i = 0; i < numberOfItems; i++)
        {
            // 随机选择一个 currentItemList 中的商品
            int randomIndex = rand.Next(0, currentItemCount);
            ItemStruct randomItem = currentItemList[randomIndex];

            // 在 minQuantity 和 maxQuantity 之间随机选择一个数量
            int randomQuantity = rand.Next(minQuantity, maxQuantity + 1);

            // 创建一个包含选定商品和随机数量的新 ItemStruct
            ItemStruct orderItem = new ItemStruct(randomItem.itemName, randomItem.itemSprite, randomQuantity, randomItem.itemPrice);

            // 将商品添加到 randomItemsOrder 列表中
            randomItemsOrder.Add(orderItem);
        }

        return randomItemsOrder;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static LinkedList<ItemInfo> ConvertBoughtItemsToItemInfoList(List<ItemStruct> BoughtItemList)
    {
        LinkedList<ItemInfo> iItemList = new LinkedList<ItemInfo>();

        System.Random random = new System.Random();

        foreach (var item in BoughtItemList)
        {
            for (int i = 0; i < item.itemNum; i++)
            {
                bool isBroken = random.Next(1, 101) <= 20;  // 20%几率是损坏的
                iItemList.AddLast(new ItemInfo(item.itemName, isBroken));
            }
        }

        return iItemList;
    }

    public void StartWorking()
    {
        UiManager = FindObjectOfType<uiManager>();
        UiManager.order.SetActive(true);
       
        isWorking = true;
       
        listOfOrders = GenerateRandomOrdersFromOwnItems(5,3);
       
        StartCoroutine(GenerateOrders());
    }

    public void StopWorking()
    {
        UiManager = FindObjectOfType<uiManager>();
        UiManager.order.SetActive(false);
     
        isWorking = false;
   
        StopCoroutine(GenerateOrders());
        GameManager.Instance.MoneySettlement();
        ownItemList= new List<ItemStruct>();



    }

    private IEnumerator GenerateOrders()
    {
        UiManager = FindObjectOfType<uiManager>();
        UiManager.order.SetActive(true);
       
        while (isWorking && HasAvailableItems(ownItemList)) // 添加库存检查
        {
            if (currentOrder < listOfOrders.Count)
            {
                UiManager.order.GetComponent<Scroll>().AddOrderUIPrefab(listOfOrders[currentOrder]);
                generateStorageBoxEmpty();
                currentOrder++;

                // 在指定范围内随机生成下一个订单的间隔时间
                float interval = UnityEngine.Random.Range(minOrderInterval, maxOrderInterval);

                // 等待游戏中的随机时间再生成下一个订单
                yield return new WaitForSeconds(interval * 60 / GameTime.Instance.timeScale);
            }
            else
            {
                // 如果所有订单都已生成完毕，停止工作
                StopCoroutine(GenerateOrders());
            }
        }

        // 当没有可用商品时，停止生成订单
        if (!HasAvailableItems(ownItemList))
        {
            showWarn("没有足够的物品生成订单，工作已停止。");
            StopWorking();
        }
    }






    public void AddItemListToFailedList(LinkedList<ItemInfo> itemList)
    {
        // 将 ItemList 添加到 FailedItemList 中
        FailedItemList.AddLast(new LinkedList<ItemInfo>(itemList));
    }
    public void StartWorkingForRefund()
    {
       
            isWorkingForRefund = true;
        currentFailedItemNode = FailedItemList.First;
        StartCoroutine(ProcessFailedItems());
        

      
    }
    private void ProcessCurrentFailedItem()
    {
        if (currentFailedItemNode != null)
        {
            // 处理当前的 FailedItemList 节点
            Debug.Log("Processing Failed Item List Node: " + currentFailedItemNode.Value);

            // 移动到下一个节点
            currentFailedItemNode = currentFailedItemNode.Next;
        }
        else
        {
           isWorkingForRefund = false;
        }
    }
    public void StopWorkingForRefund()
    {
        if (isWorkingForRefund)
        {
            isWorkingForRefund = false;
            StopCoroutine(ProcessFailedItems());
            FailedItemList= new LinkedList<LinkedList<ItemInfo>>();
        }
       
    }
    private IEnumerator ProcessFailedItems()
    {
        while (isWorkingForRefund)
        {
            yield return new WaitUntil(() => GameTime.Instance.minutes % minutesInterval == 0);

            if (currentFailedItemNode != null)
            {
                generateStorageBox(currentFailedItemNode.Value);
             
                // 移动到下一个节点
                currentFailedItemNode = currentFailedItemNode.Next;
             
            }
            else
            {
              
                StopWorkingForRefund();
                // 如果需要，重置为链表的第一个节点
                // currentFailedItemNode = FailedItemList.First;
            }
           

            //// 等待下一个时间间隔
            yield return new WaitForSeconds(minutesInterval * 60 / GameTime.Instance.timeScale);
        }
    }
    public void generateStorageBox(LinkedList<ItemInfo> ItemList)
    {

        GameObject returnPosGameObject = GameObject.Find("returnPos");
        GameObject boxInstance = Instantiate(StorageBox, returnPosGameObject.transform.position, Quaternion.identity);
        bool isEmpty = ItemList.Count == 0 || ItemList.First == null;
        boxInstance.GetComponent<StorageBox>().init(ItemList, false, !isEmpty);

    }
    public void generateStorageBoxEmpty()
    {
        GameObject boughtPosGameObject = GameObject.Find("BoughtPos");

        GameObject boxInstance = Instantiate(StorageBox, boughtPosGameObject.transform.position, Quaternion.identity);
       
    

    }
    public void generateStorageBoxBought(LinkedList<ItemInfo> ItemList)
    {

        GameObject boughtPosGameObject = GameObject.Find("BoughtPos");
        GameObject boxInstance = Instantiate(StorageBox, boughtPosGameObject.transform.position, Quaternion.identity);
        boxInstance.GetComponent<StorageBox>().init(ItemList, false, true);


    }
    void OnDestroy()
    {
        StopWorkingForRefund(); // 确保对象销毁时停止工作并取消协程
    }
    public void updateMarketItemUI()
    {
        UiManager = FindObjectOfType<uiManager>();
       


            UiManager.market.GetComponent<Scroll>().ClearContent();
        foreach (ItemStruct item in TotalItemList)
        {

            UiManager.market.GetComponent<Scroll>().AddItemUIPrefab(item);

        }


       
    }
    public void AddItemToBoughtList(ItemStruct item)
    {
        bool found = false;

        for (int i = 0; i < BoughtItemList.Count; i++)
        {
            var currentItem = BoughtItemList[i];
            if (currentItem.itemName == item.itemName)
            {
                currentItem.itemNum += item.itemNum;
                found = true;
                break;
            }
        }

        if (!found)
        {
            BoughtItemList.Add(item);
        }
    }
    public List<List<ItemStruct>> GenerateRandomOrdersFromOwnItems(int maxItemCount, int maxSpecies)
    {
        List<ItemStruct> boughtItems = ownItemList.OrderBy(item => item.itemName).ToList();
        List<List<ItemStruct>> randomOrders = new List<List<ItemStruct>>();

        if (boughtItems.Count == 0)
        {
            return randomOrders;
        }

        // 设置订单生成上限，防止生成过多订单
        int maxOrderLimit = 100; // 根据需要调整

        while (HasAvailableItems(boughtItems) && randomOrders.Count < maxOrderLimit)
        {
            List<ItemStruct> selectedItems = new List<ItemStruct>();

            // 确保 itemCount 不超过 maxSpecies
            int itemCount = GetRandomItemCount(boughtItems.Count, maxSpecies);

            for (int i = 0; i < itemCount; i++)
            {
                ItemStruct randomItem = GetRandomItem(boughtItems);

                // 如果该商品的数量为 0，跳过该商品
                if (randomItem.itemNum <= 0)
                {
                    continue;
                }

                // 获取随机数量，但不超过商品剩余数量
                int randomQuantity = GetRandomQuantity(randomItem.itemNum, maxItemCount);

                // 将随机生成的商品和数量添加到订单中
                selectedItems.Add(new ItemStruct(randomItem.itemName, randomItem.itemSprite, randomQuantity, randomItem.itemPrice));

                // 减少原商品的数量
                int index = boughtItems.FindIndex(item => item.itemName == randomItem.itemName);
                if (index != -1)
                {
                    boughtItems[index] = new ItemStruct(boughtItems[index].itemName,
                                                        boughtItems[index].itemSprite,
                                                        boughtItems[index].itemNum - randomQuantity,
                                                        boughtItems[index].itemPrice);
                }
            }

            // 移除数量为 0 的商品，防止进入死循环
            boughtItems.RemoveAll(item => item.itemNum <= 0);

            // 将生成的订单添加到订单列表中
            randomOrders.Add(selectedItems);
        }

        return randomOrders;
    }


    private bool HasAvailableItems(List<ItemStruct> items)
    {
        // 检查是否存在数量大于0的商品
        foreach (var item in items)
        {
            if (item.itemNum > 0)
            {
                return true;
            }
        }
        return false;
    }

    private int GetRandomItemCount(int totalItemsCount, int maxSpecies)
    {
        // 随机生成商品种类数量，确保不会超过商品总数或最大种类数
        return UnityEngine.Random.Range(1, Math.Min(totalItemsCount, maxSpecies));
    }

    private ItemStruct GetRandomItem(List<ItemStruct> items)
    {
        // 从商品列表中随机选择一个商品
        int index = UnityEngine.Random.Range(0, items.Count);
        return items[index];
    }

    private int GetRandomQuantity(int availableQuantity, int maxItemCount)
    {
        // 添加日志打印当前传入的 availableQuantity 和 maxItemCount
        Debug.Log($"GetRandomQuantity called with availableQuantity: {availableQuantity}, maxItemCount: {maxItemCount}");

        // 如果 availableQuantity 和 maxItemCount 都小于或等于1，则返回 1
        if (availableQuantity <= 1 || maxItemCount <= 1)
        {
            return 1;
        }

        // 否则在 1 到最小值（可用数量和最大允许数量）之间生成一个随机数
        int randomQuantity = UnityEngine.Random.Range(1, Math.Min(availableQuantity, maxItemCount) + 1);

        Debug.Log($"Generated random quantity: {randomQuantity}");
        return randomQuantity;
    }



    private int GetItemCount(int boughtItemsCount, int maxSpecies)
    {
        return Math.Min(boughtItemsCount, maxSpecies);
    }

    private int GetRemainingQuantity(string itemName, Dictionary<string, int> remainingQuantities)
    {
        if (remainingQuantities.ContainsKey(itemName))
        {
            return remainingQuantities[itemName];
        }
        return 0;
    }




    public void PrintListOfOrders(List<List<ItemStruct>> listOfOrders)
    {
        foreach (var subList in listOfOrders)
        {
            foreach (var item in subList)
            {
                Debug.Log($"Item Name: {item.itemName}, Quantity: {item.itemNum}, Price: {item.itemPrice}");
            }
        }
    }
    public void PrintFailedItems()
    {
        foreach (var outerList in FailedItemList)
        {
            foreach (var itemInfo in outerList)
            {
                Debug.Log($"Item Name: {itemInfo.itemName}, Broken: {itemInfo.broken}");
            }
        }
    }
}
