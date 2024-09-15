using UnityEngine;
using UnityEngine.UI;

public class CheckMachine : MonoBehaviour,IInteractable
{
    public Sprite rightSprite; // ������ʾ��ȷ�����ͼƬ
    public Sprite wrongSprite; // ������ʾ��������ͼƬ
    public Image displayImage; // UI�������ʾ��ǰ��ͼƬ
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
        // ������������ͼƬ
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
        color.a = 0; // ����͸����Ϊ0
        displayImage.color = color; // Ӧ���µ���ɫ
      
    }

    public void ShowImageFadeIn()
    {
        if (displayImage == null)
        {

            return;
        }
        Color color = displayImage.color;
        color.a = 1; // ����͸����Ϊ1����ȫ��͸����
        displayImage.color = color; // Ӧ���µ���ɫ
   
    }
}
