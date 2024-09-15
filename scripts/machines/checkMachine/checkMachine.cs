using UnityEngine;
using UnityEngine.UI;

public class CheckMachine : MonoBehaviour,IInteractable
{
    public Sprite rightSprite; // 用于显示正确结果的图片
    public Sprite wrongSprite; // 用于显示错误结果的图片
    public Image displayImage; // UI组件，显示当前的图片
    public Transform posTransform;
    public GameObject ItemOnTable;

    private void Update()
    {
        if (ItemOnTable==null)
        {

            HideImageFadeOut();
        }
    }
    private void Start()
    {
        HideImageFadeOut();    }
    public void setOrGetItem(PlayerControler PlayerControlerscript)
    {
        GameObject pickedItem = PlayerControlerscript.pickedItem;
        if (pickedItem != null)
        {
            ItemOnTable = PlayerControlerscript.pickedItem;
            PlayerControlerscript.dropItemInPos(posTransform.position, false, false);
            setCondition(!ItemOnTable.GetComponent<item>().ifBroken);

        }
        else if (pickedItem == null)
        {
            if (true)
            {
                if (ItemOnTable != null)
                {
                   
                    PlayerControlerscript.spawnItemToHold(ItemOnTable);
                    ItemOnTable = null;
                    HideImageFadeOut();
                   
                }


            }
        }


    }

    public void PerformInteraction(GameObject Player)
    {
        PlayerControler PlayerControlerscript = Player.GetComponent<PlayerControler>();

        setOrGetItem(PlayerControlerscript);
    }

    public void PerformInteractionClick(GameObject Player)
    {
        throw new System.NotImplementedException();
    }

    public void PerformInteractionF(GameObject Player)
    {
        throw new System.NotImplementedException();
    }

    public void PerformInteractionLongPress(GameObject Player)
    {
        throw new System.NotImplementedException();
    }

    public void setCondition(bool condition)
    {
      
        if (displayImage == null)
        {
            
            return;
        }
        ShowImageFadeIn();
        // 根据条件设置图片
        if (condition)
        {
            displayImage.sprite = rightSprite;
        }
        else
        {
            displayImage.sprite = wrongSprite;
        }
    }
    public void HideImageFadeOut()
    {
        if (displayImage == null)
        {

            return;
        }
        Color color = displayImage.color;
        color.a = 0; // 设置透明度为0
        displayImage.color = color; // 应用新的颜色
      
    }

    public void ShowImageFadeIn()
    {
        if (displayImage == null)
        {

            return;
        }
        Color color = displayImage.color;
        color.a = 1; // 设置透明度为1（完全不透明）
        displayImage.color = color; // 应用新的颜色
   
    }
}
