using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public List<item> itemList;
    public int maxInventoryCapacity;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void initialize(int maxInventoryCapacity)
    {
        this.maxInventoryCapacity = maxInventoryCapacity;

    }
    void addItemInInventory(item item)
    {
         foreach (item itemInInventory in this.itemList)
        {
          
            //
        }
        this.itemList.Add(item);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
