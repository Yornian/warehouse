using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingMarketUISlot : MonoBehaviour, IPointerEnterHandler
{

    public Image BuildingImage;
    public TMP_Text BuildingText;
    public TMP_Text numText;
    public Button myButton;
    public BuildingInfo BuildingStruct;
    public TMP_Text MoneyText;
    public Image BuildingShowImage;//��ͼչʾpic

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MoneyText != null)
        {
            SetItemMoneyText(BuildingStruct.price.ToString());
        }
        if (BuildingShowImage != null)
        {
            SetItemShowImage(BuildingStruct.pic);
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
            BuildingShowImage = itemShowObject.GetComponent<Image>();
        }

    }
    public void SetItemMoneyText(string newText)
    {
        MoneyText.text = newText;
    }
    // ��������itemImage��ͼƬ
 
    public void InitSlot(BuildingInfo Struct)
    {

        SetItemImage(Struct.pic);
        SetItemText(Struct.name);
        //SetItemShowImage(Struct.pic);
        //SetItemMoneyText(Struct.price.ToString());
        BuildingStruct = Struct;
    }

    public void SetItemImage(Sprite newImage)
    {
        BuildingImage.sprite = newImage;
        if (myButton != null)
        {
            // ����ť��ӵ���¼�������
            myButton.onClick.AddListener(OnButtonClick);
        }
    }
    public void SetItemShowImage(Sprite newImage)
    {
        BuildingShowImage.sprite = newImage;

    }
    void OnButtonClick()
    {
        if (GameManager.Instance.TrySpendMoney(BuildingStruct.price))
        {
            BuildingManager.Instance.BuyBuilding(BuildingStruct);
        }

    }
    // ��������itemText���ı�
    public void SetItemText(string newText)
    {
        BuildingText.text = newText;
    }
 
}

