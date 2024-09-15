using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarketUISlot : MonoBehaviour, IPointerEnterHandler
{

    public Image itemImage;
    public TMP_Text itemText;
    public TMP_Text numText;
    public Button myButton;
    public ItemStruct itemStruct;
    public TMP_Text MoneyText;
    public Image itemShowImage;//��ͼչʾpic

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MoneyText != null)
        {
            SetItemMoneyText(itemStruct.itemPrice.ToString());
        }
        if (itemShowImage != null)
        {
            SetItemShowImage(itemStruct.itemSprite);
        }

    }
    private void Start()
    {
        GameObject moneyObject = GameObject.Find("SellMoney");
        if (moneyObject != null)
        {
            MoneyText = moneyObject.GetComponent<TMP_Text>();
        }
        GameObject itemShowObject = GameObject.Find("itemShowPic");
        if (itemShowObject != null)
        {
            itemShowImage = itemShowObject.GetComponent<Image>();
        }

    }
    public void SetItemMoneyText(string newText)
    {
        MoneyText.text = newText;
    }
    // ��������itemImage��ͼƬ
    public void InitItemSlot(ItemStruct Struct)
    {
        SetItemImage(Struct.itemSprite);
        SetItemText(Struct.itemName);
        itemStruct = Struct;

    }
    public void InitOrderSlot(ItemStruct Struct)
    {

        SetItemImage(Struct.itemSprite);
        SetItemText(Struct.itemName);
        SetItemNum(Struct.itemNum.ToString());
        itemStruct = Struct;
    }

    public void SetItemImage(Sprite newImage)
    {
        itemImage.sprite = newImage;
        if (myButton != null)
        {
            // ����ť��ӵ���¼�������
            myButton.onClick.AddListener(OnButtonClick);
        }
    }
    public void SetItemShowImage(Sprite newImage)
    {
        itemShowImage.sprite = newImage;

    }
    void OnButtonClick()
    {
        if (GameManager.Instance.TrySpendMoney(itemStruct.itemPrice))
        {
            OrderManager.Instance.buyItem(itemStruct);

        }

    }
    // ��������itemText���ı�
    public void SetItemText(string newText)
    {
        itemText.text = newText;
    }
    public void SetItemNum(string newText)
    {
        numText.text = "��" + newText;
    }
}


