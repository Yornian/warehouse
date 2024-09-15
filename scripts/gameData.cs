using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // For file operations like File.WriteAllText

public class handleData :MonoBehaviour
{
    void Start()
    {

 
    }
    void saveGameData()
    {
        uploadItemData();
    }
    void loadGameData()
    {
        loadItemData();

    }
    void loadItemData()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);
            GameData loadedItemData = JsonUtility.FromJson<GameData>(dataAsJson);
            if (loadedItemData != null)
            {
               // foreach (var item in loadedItemData.items)
                {
                    
                  //  Debug.Log($"Item Name: {item.itemName}, Cost: {item.itemCost}");
                    //.........here
                    // you need to get the gameManager instance and updata value for game data
                }
            }
        }
        else
        {
            Debug.LogError("Cannot find game data file.");
           
        }
    }
    void uploadItemData()
    {
  
        GameData gameData = new GameData();
        //.........here
        // you need to get the gameManager instance and pass the data 
        //gameData.items.Add(new ItemData() { itemName = "Sword", itemCost = 100, itemNum = 1 });
        //gameData.items.Add(new ItemData() { itemName = "Shield", itemCost = 50, itemNum = 2 });
        string json = JsonUtility.ToJson(gameData, true);
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
        try
        {
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to write to file: "+ex.Message);
        }
        
        
    }
    void readItemData()
    {

    }
}

    [System.Serializable]
public class GameData 
{
   // public List<ItemData> items = new List<ItemData>();
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

