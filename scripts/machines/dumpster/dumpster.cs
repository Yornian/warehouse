using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster : MonoBehaviour,IInteractable
{
   
   
    
    public Transform posTransform;
    public BoxCollider2D AnimCollider;
    // Start is called before the first frame update
    void Start()
    {
       
   
       
    }
  
    public void DropItem(PlayerControler PlayerControlerscript)
    {
        GameObject pickedItem = PlayerControlerscript.pickedItem;
        if (pickedItem != null)
        {
          
            PlayerControlerscript.dropItemInPos(posTransform.position, false, true);
           

        }
      


    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformInteraction(GameObject Player)
    {

        PlayerControler PlayerControlerScript = Player.GetComponent<PlayerControler>();
        DropItem(PlayerControlerScript);
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
