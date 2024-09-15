using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间
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
    public Image BuildingShowImage;//大图展示pic

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
    // 用于设置itemImage的图片
 
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
            // 给按钮添加点击事件监听器
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
    // 用于设置itemText的文本
    public void SetItemText(string newText)
    {
        BuildingText.text = newText;
    }
 
}

