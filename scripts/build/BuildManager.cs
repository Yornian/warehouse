using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // 单例实例
    private static BuildingManager instance;

    // 获取实例的属性
    public static BuildingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BuildingManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("BuildingManager");
                    instance = obj.AddComponent<BuildingManager>();
                }
            }
            return instance;
        }
    }

    public List<BuildingInfo> BuildingList = new List<BuildingInfo>();
    public List<BuildingInfo> TotalBuildingList = new List<BuildingInfo>();
    public BuildingInfo selectedBuildingInfo;

    public GridManager gridSystem;
    public Building selectedBuilding;
    public BuildingPreview buildingPreview;
    public GameObject buildingPrefab;
    public GameObject buildingBackpackPrefab;
    public string prefabPath = "Prefab/Building";
    private GameObject[,] gridArray;

    public GameObject SlotPrefab;
  
    public Transform BuildingPackageTransform;
    public GameObject market;

    public Scroll marketScroll;

    void Awake()
    {
        // 确保只有一个 BuildingManager 实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (buildingPreview == null)
        {
            buildingPreview = FindObjectOfType<BuildingPreview>();
        }

       
    }

    void Start()
    {
       // InitTotalListInUI();
    }
    public void initBuildingList(List<BuildingInfo> iBuildingList)
    {
        BuildingList = iBuildingList;


    }
    void Update()
    {
        if (selectedBuilding != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0; // 确保 Z 轴位置为 0
            int x, y;
            gridSystem.GetXY(worldPosition, out x, out y);

            Vector3 alignedPosition = gridSystem.GetWorldPosition(x, y);
            buildingPreview.SetPreviewPosition(alignedPosition); // 更新预览位置

            gridSystem.UpdateGridPosition(selectedBuilding, alignedPosition, gridArray);
            


            if (IsValidPosition())
            {


                if (Input.GetMouseButtonDown(0))
                {

                    StopBuilding();
                  


                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                CancleBuilding();
            }

        }

    }
    public void InitTotalListInUI()
    {
        marketScroll.ClearContent();
        if (TotalBuildingList != null && TotalBuildingList.Count != 0)
        {
            foreach (BuildingInfo building in TotalBuildingList)
            {
                marketScroll.AddBuildingMarketUIPrefab(building);

            }
        }

        //

    }
    public void InitBackPackListInUI()
    {
        ClearBuildingPackageContent();
        if (BuildingList != null && BuildingList.Count != 0)
        {
            foreach (BuildingInfo building in BuildingList)
            {
                AddBuildinBackpackUIPrefab(building);

            }
        }

        //

    }
    public bool IsValidPosition()
    {

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (!gridArray[x, y].GetComponent<GridCell>().IfCanBuild())
                {
                    return false;

                }
            }
        }

        return true;

    }
    public void BuyBuilding(BuildingInfo newBuilding)
    {
        AddBuilding(newBuilding);
        InitBackPackListInUI();
      
        
    }
    public void AddBuilding(BuildingInfo newBuilding)
    {
        // 遍历列表，检查是否存在相同名称的建筑
        for (int i = 0; i < BuildingList.Count; i++)
        {
            if (BuildingList[i].name == newBuilding.name)
            {
                // 如果找到，更新数量
                BuildingInfo updatedBuilding = BuildingList[i];
                updatedBuilding.count += newBuilding.count;
                BuildingList[i] = updatedBuilding;
                return;  // 完成更新后退出函数
            }
        }

        // 如果列表中没有相同名称的建筑，添加新建筑
        BuildingList.Add(newBuilding);
    }
    public void SelectBuilding(BuildingInfo BuildingStruct)
    {



        selectedBuildingInfo = BuildingStruct;
        buildingPrefab = LoadBuildingPrefab(BuildingStruct.name);
        selectedBuilding = LoadBuildingScript(buildingPrefab);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0; // 确保 Z 轴位置为 0
        int x, y;
        gridSystem.GetXY(worldPosition, out x, out y);

        Vector3 alignedPosition = gridSystem.GetWorldPosition(x, y);
        if (gridArray != null)
        {
            foreach (GameObject cell in gridArray)
            {
                if (cell != null)
                {
                    Destroy(cell);
                }
            }
        }
        // 创建并更新网格
        gridArray = gridSystem.CreateGridCells(selectedBuilding, alignedPosition);

        if (buildingPrefab != null)
        {
            buildingPreview.CreatePreview(buildingPrefab);
        }
       

    }
    public void updateMarketBuidingUI()
    {
        marketScroll.GetComponent<Scroll>().ClearContent();
        foreach (BuildingInfo building in TotalBuildingList)
        {
            
            marketScroll.GetComponent<Scroll>().AddBuildingMarketUIPrefab(building);

        }



    }
    public void updateBuidingPackageUI()
    {
        ClearBuildingPackageContent();
        foreach (BuildingInfo building in BuildingList)
        {
            AddBuildinBackpackUIPrefab(building);


        }


    }
   
    public void AddBuildinBackpackUIPrefab(BuildingInfo Struct)
    {
        if (buildingBackpackPrefab != null && BuildingPackageTransform != null)
        {
            // 实例化预制体
            GameObject uiInstance = Instantiate(buildingBackpackPrefab);

            // 将实例化的对象设置为 Panel 的子对象
            uiInstance.transform.SetParent(BuildingPackageTransform, false);
            //
            uiInstance.GetComponent<BuildingBackPackSlot >().InitSlot(Struct);

        }
        else
        {
            Debug.LogWarning("UIPrefab or PanelTransform is not assigned.");
        }
    }
   
    public void ClearBuildingPackageContent()
    {
        // 获取所有子对象
        foreach (Transform child in BuildingPackageTransform)
        {
            // 销毁每个子对象
            Destroy(child.gameObject);
        }
    }
    public void StopBuilding()
    {


        selectedBuilding = null;
        buildingPrefab = null;

        buildingPreview.build();
        SubtractBuilding(selectedBuildingInfo);
        updateBuidingPackageUI();
        DistroyGridArray();
    }
    public void SubtractBuilding(BuildingInfo selectedBuildingInfo)
    {
        for (int i = 0; i < BuildingList.Count; i++)
        {
            if (BuildingList[i].name == selectedBuildingInfo.name)
            {
                BuildingInfo updatedBuilding = BuildingList[i];
                updatedBuilding.count -= selectedBuildingInfo.count;

                if (updatedBuilding.count <= 0)
                {
                    BuildingList.RemoveAt(i);
                }
                else
                {
                    BuildingList[i] = updatedBuilding;
                }

                return; // 找到匹配项并处理后退出函数
            }
        }

        Debug.LogWarning("Building not found in the list: " + selectedBuildingInfo.name);
    }
    public void CancleBuilding()
    {
        // 取消选中的建筑
        selectedBuilding = null;
        buildingPrefab = null;

        // 销毁建筑预览
        if (buildingPreview != null)
        {
            buildingPreview.ClearPreview();
        }

        // 销毁网格数组
        DistroyGridArray();
    }
    public void DistroyGridArray()
    {
        // 获取数组的维度
        int width = gridArray.GetLength(0);
        int height = gridArray.GetLength(1);

        // 遍历数组中的每个元素
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 销毁每个 GameObject
                if (gridArray[x, y] != null)
                {
                    Destroy(gridArray[x, y]);
                    gridArray[x, y] = null; // 确保引用被清除
                }
            }
        }
    }
    public GameObject LoadBuildingPrefab(string prefabName)
    {
        string path = prefabPath + "/" + prefabName;
        GameObject loadedPrefab = Resources.Load<GameObject>(path);

        if (loadedPrefab != null)
        {
            return loadedPrefab;
        }
        else
        {
            Debug.LogError("Prefab not found at path: " + path);
            return null;
        }
    }

    public Building LoadBuildingScript(GameObject buildingPrefab)
    {
        if (buildingPrefab != null)
        {
            return buildingPrefab.GetComponent<Building>();
        }
        else
        {
            return null;
        }
    }
}
[Serializable]
public struct BuildingInfo
{
    public string name;
    public Sprite pic;
    public int count;
    public int price;
    public BuildingInfo(string iname, Sprite ipic, int icount, int iprice)
    {
        name = iname;
        pic = ipic;
        count = icount;
        price = iprice;
    }
}