using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�
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
            // ����ť��ӵ���¼�������
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
    // ��������itemText���ı�
    public void SetItemCount(string newText)
    {
        CountText.text = newText;
    }

}

