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
public class OrderManager : MonoBehaviour    //��������̸���� ѡ��Ҫ��������Ʒ����������Ʒ�Ż������order list�
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



    public bool isWorking = false; // ����Ƿ����ڹ���
    public float minOrderInterval = 25f; // ������ɼ������Ϸ���ӣ�
    public float maxOrderInterval = 60f;   // ����ɼ������Ϸ���ӣ�




    public int minutesInterval = 3; // ÿ�������ӣ���Ϸʱ�䣩������һ��Ԫ��
    private LinkedListNode<LinkedList<ItemInfo>> currentFailedItemNode;
    public bool isWorkingForRefund = false; // ����Ƿ����ڴ����˻�

    
    public GameObject StorageBox;
    public int todayRightOrder=0;
 
    private OrderManager() { }

    // ��ʼ��ʱȷ��ֻ��һ��ʵ��
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
        // ���Ŀ���б����Ƿ��Ѿ�����ͬ����Ʒ
        var existingItem = targetList.Find(item => item.itemName == newItem.itemName);

        if (existingItem.itemName != null)
        {
            // ����Ѵ��ڣ�������Ʒ����
            int index = targetList.FindIndex(item => item.itemName == newItem.itemName);
            targetList[index] = new ItemStruct(existingItem.itemName, existingItem.itemSprite, existingItem.itemNum + newItem.itemNum, existingItem.itemPrice);
        }
        else
        {
            // ��������ڣ�ֱ�����
            targetList.Add(newItem);
        }
    }
    //public void AddItemToList(ItemStruct newItem, List<ItemStruct> targetList)
    //{
        
    //        targetList.Add(newItem);
        
    //}

    // ���������Ƴ���
    public void RemoveItemFromList(ItemStruct itemToRemove, List<ItemStruct> targetList)
    {
        targetList.RemoveAll(item => item.itemName == itemToRemove.itemName);
    }

    // ��ȡ�����Ʒ�б�ĺ���
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
            // ���ѡ��һ�� currentItemList �е���Ʒ
            int randomIndex = rand.Next(0, currentItemCount);
            ItemStruct randomItem = currentItemList[randomIndex];

            // �� minQuantity �� maxQuantity ֮�����ѡ��һ������
            int randomQuantity = rand.Next(minQuantity, maxQuantity + 1);

            // ����һ������ѡ����Ʒ������������� ItemStruct
            ItemStruct orderItem = new ItemStruct(randomItem.itemName, randomItem.itemSprite, randomQuantity, randomItem.itemPrice);

            // ����Ʒ��ӵ� randomItemsOrder �б���
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
                bool isBroken = random.Next(1, 101) <= 20;  // 20%�������𻵵�
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
       
        while (isWorking && HasAvailableItems(ownItemList)) // ��ӿ����
        {
            if (currentOrder < listOfOrders.Count)
            {
                UiManager.order.GetComponent<Scroll>().AddOrderUIPrefab(listOfOrders[currentOrder]);
                generateStorageBoxEmpty();
                currentOrder++;

                // ��ָ����Χ�����������һ�������ļ��ʱ��
                float interval = UnityEngine.Random.Range(minOrderInterval, maxOrderInterval);

                // �ȴ���Ϸ�е����ʱ����������һ������
                yield return new WaitForSeconds(interval * 60 / GameTime.Instance.timeScale);
            }
            else
            {
                // ������ж�������������ϣ�ֹͣ����
                StopCoroutine(GenerateOrders());
            }
        }

        // ��û�п�����Ʒʱ��ֹͣ���ɶ���
        if (!HasAvailableItems(ownItemList))
        {
            showWarn("û���㹻����Ʒ���ɶ�����������ֹͣ��");
            StopWorking();
        }
    }






    public void AddItemListToFailedList(LinkedList<ItemInfo> itemList)
    {
        // �� ItemList ��ӵ� FailedItemList ��
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
            // ����ǰ�� FailedItemList �ڵ�
            Debug.Log("Processing Failed Item List Node: " + currentFailedItemNode.Value);

            // �ƶ�����һ���ڵ�
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
             
                // �ƶ�����һ���ڵ�
                currentFailedItemNode = currentFailedItemNode.Next;
             
            }
            else
            {
              
                StopWorkingForRefund();
                // �����Ҫ������Ϊ����ĵ�һ���ڵ�
                // currentFailedItemNode = FailedItemList.First;
            }
           

            //// �ȴ���һ��ʱ����
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
        StopWorkingForRefund(); // ȷ����������ʱֹͣ������ȡ��Э��
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

        // ���ö����������ޣ���ֹ���ɹ��ඩ��
        int maxOrderLimit = 100; // ������Ҫ����

        while (HasAvailableItems(boughtItems) && randomOrders.Count < maxOrderLimit)
        {
            List<ItemStruct> selectedItems = new List<ItemStruct>();

            // ȷ�� itemCount ������ maxSpecies
            int itemCount = GetRandomItemCount(boughtItems.Count, maxSpecies);

            for (int i = 0; i < itemCount; i++)
            {
                ItemStruct randomItem = GetRandomItem(boughtItems);

                // �������Ʒ������Ϊ 0����������Ʒ
                if (randomItem.itemNum <= 0)
                {
                    continue;
                }

                // ��ȡ�������������������Ʒʣ������
                int randomQuantity = GetRandomQuantity(randomItem.itemNum, maxItemCount);

                // ��������ɵ���Ʒ��������ӵ�������
                selectedItems.Add(new ItemStruct(randomItem.itemName, randomItem.itemSprite, randomQuantity, randomItem.itemPrice));

                // ����ԭ��Ʒ������
                int index = boughtItems.FindIndex(item => item.itemName == randomItem.itemName);
                if (index != -1)
                {
                    boughtItems[index] = new ItemStruct(boughtItems[index].itemName,
                                                        boughtItems[index].itemSprite,
                                                        boughtItems[index].itemNum - randomQuantity,
                                                        boughtItems[index].itemPrice);
                }
            }

            // �Ƴ�����Ϊ 0 ����Ʒ����ֹ������ѭ��
            boughtItems.RemoveAll(item => item.itemNum <= 0);

            // �����ɵĶ�����ӵ������б���
            randomOrders.Add(selectedItems);
        }

        return randomOrders;
    }


    private bool HasAvailableItems(List<ItemStruct> items)
    {
        // ����Ƿ������������0����Ʒ
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
        // ���������Ʒ����������ȷ�����ᳬ����Ʒ���������������
        return UnityEngine.Random.Range(1, Math.Min(totalItemsCount, maxSpecies));
    }

    private ItemStruct GetRandomItem(List<ItemStruct> items)
    {
        // ����Ʒ�б������ѡ��һ����Ʒ
        int index = UnityEngine.Random.Range(0, items.Count);
        return items[index];
    }

    private int GetRandomQuantity(int availableQuantity, int maxItemCount)
    {
        // �����־��ӡ��ǰ����� availableQuantity �� maxItemCount
        Debug.Log($"GetRandomQuantity called with availableQuantity: {availableQuantity}, maxItemCount: {maxItemCount}");

        // ��� availableQuantity �� maxItemCount ��С�ڻ����1���򷵻� 1
        if (availableQuantity <= 1 || maxItemCount <= 1)
        {
            return 1;
        }

        // ������ 1 ����Сֵ�������������������������֮������һ�������
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
