using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixMachine : MonoBehaviour,IInteractable
{


    private GameObject ItemOnTable;
    private BoxCollider2D Collider;
    private Transform posTransform;
    // Start is called before the first frame update
    void Start()
    {
        Transform PicTransform = transform.Find("fixMachinePic");
        Collider = GetComponent<BoxCollider2D>();
       posTransform= transform.Find("pos");

    }
  
    public void setOrGetItem(PlayerControler PlayerControlerscript)
    {
        GameObject pickedItem = PlayerControlerscript.pickedItem;
        if (pickedItem != null)
        {
            ItemOnTable = PlayerControlerscript.pickedItem;
            PlayerControlerscript.dropItemInPos(posTransform.position, false,false);
            fixItem();

        }
        else if(pickedItem == null)
        {
            if(true)
            {
                if(ItemOnTable!=null)
                {
                    PlayerControlerscript.spawnItemToHold(ItemOnTable);
                    ItemOnTable = null;

                }


            }
        }


    }
    public void fixItem()
    {
        ItemOnTable.GetComponent<item>().setIfBroken(false);
    }
    public void PerformInteraction(GameObject Player)
    {
        PlayerControler PlayerControlerscript=Player.GetComponent<PlayerControler>();

        setOrGetItem(PlayerControlerscript);




    }

    public void PerformInteractionLongPress(GameObject Player)
    {
        
    }

    public void PerformInteractionClick(GameObject Player)
    {
        
    }

    public void PerformInteractionF(GameObject Player)
    {
       
    }
}
