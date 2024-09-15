using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingBackPackSlot : MonoBehaviour, IPointerEnterHandler
{

    public Image BuildingImage;
    public TMP_Text CountText;
  
    public Button myButton;
    public BuildingInfo BuildingStruct;


    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    private void Start()
    {
       
    }

    public void InitSlot(BuildingInfo Struct)
    {

        SetItemImage(Struct.pic);
        SetItemCount(Struct.count.ToString());
     
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
  
    void OnButtonClick()
    {
        //if (GameManager.Instance.TrySpendMoney(BuildingStruct.price))
        //{
        //    BuildingManager.Instance.BuyBuilding(BuildingStruct);
        //}
        myButton.Select();
        BuildingManager.Instance.SelectBuilding(BuildingStruct);



       }
    // 用于设置itemText的文本
    public void SetItemCount(string newText)
    {
        CountText.text = newText;
    }

}

