using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // ����ʵ��
    private static BuildingManager instance;

    // ��ȡʵ��������
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
        // ȷ��ֻ��һ�� BuildingManager ʵ������
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
            worldPosition.z = 0; // ȷ�� Z ��λ��Ϊ 0
            int x, y;
            gridSystem.GetXY(worldPosition, out x, out y);

            Vector3 alignedPosition = gridSystem.GetWorldPosition(x, y);
            buildingPreview.SetPreviewPosition(alignedPosition); // ����Ԥ��λ��

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
        // �����б�����Ƿ������ͬ���ƵĽ���
        for (int i = 0; i < BuildingList.Count; i++)
        {
            if (BuildingList[i].name == newBuilding.name)
            {
                // ����ҵ�����������
                BuildingInfo updatedBuilding = BuildingList[i];
                updatedBuilding.count += newBuilding.count;
                BuildingList[i] = updatedBuilding;
                return;  // ��ɸ��º��˳�����
            }
        }

        // ����б���û����ͬ���ƵĽ���������½���
        BuildingList.Add(newBuilding);
    }
    public void SelectBuilding(BuildingInfo BuildingStruct)
    {



        selectedBuildingInfo = BuildingStruct;
        buildingPrefab = LoadBuildingPrefab(BuildingStruct.name);
        selectedBuilding = LoadBuildingScript(buildingPrefab);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0; // ȷ�� Z ��λ��Ϊ 0
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
        // ��������������
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
            // ʵ����Ԥ����
            GameObject uiInstance = Instantiate(buildingBackpackPrefab);

            // ��ʵ�����Ķ�������Ϊ Panel ���Ӷ���
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
        // ��ȡ�����Ӷ���
        foreach (Transform child in BuildingPackageTransform)
        {
            // ����ÿ���Ӷ���
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

                return; // �ҵ�ƥ���������˳�����
            }
        }

        Debug.LogWarning("Building not found in the list: " + selectedBuildingInfo.name);
    }
    public void CancleBuilding()
    {
        // ȡ��ѡ�еĽ���
        selectedBuilding = null;
        buildingPrefab = null;

        // ���ٽ���Ԥ��
        if (buildingPreview != null)
        {
            buildingPreview.ClearPreview();
        }

        // ������������
        DistroyGridArray();
    }
    public void DistroyGridArray()
    {
        // ��ȡ�����ά��
        int width = gridArray.GetLength(0);
        int height = gridArray.GetLength(1);

        // ���������е�ÿ��Ԫ��
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // ����ÿ�� GameObject
                if (gridArray[x, y] != null)
                {
                    Destroy(gridArray[x, y]);
                    gridArray[x, y] = null; // ȷ�����ñ����
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