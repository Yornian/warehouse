using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public float cellSize;
    public GameObject gridCellPrefab; // 网格预制体
 

    void Start()
    {
       
    }

    public GameObject[,] CreateGridCells(Building building, Vector3 position)
    {
        int buildingWidth = building.width;
        int buildingHeight = building.height;

        GameObject[,] gridArray;


        gridArray = new GameObject[buildingWidth, buildingHeight];
        float startX = position.x - buildingWidth * cellSize / 2 + cellSize / 2;
        float startY = position.y - buildingHeight * cellSize / 2 + cellSize / 2;

        for (int x = 0; x < buildingWidth; x++)
        {
            for (int y = 0; y < buildingHeight; y++)
            {
                Vector3 cellPosition = new Vector3(startX + x * cellSize, startY + y * cellSize, 0);
                GameObject gridCell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                gridCell.name = $"GridCell_{x}_{y}";
                gridArray[x, y] = gridCell;
            }
        }
      
        return gridArray;
    }

    public void UpdateGridPosition(Building building, Vector3 position, GameObject[,] gridArray)
    {
        int buildingWidth = building.width;
        int buildingHeight = building.height;

        float startX = position.x - buildingWidth * cellSize / 2 + cellSize / 2;
        float startY = position.y - buildingHeight * cellSize / 2 + cellSize / 2;

        for (int x = 0; x < buildingWidth; x++)
        {
            for (int y = 0; y < buildingHeight; y++)
            {
                Vector3 cellPosition = new Vector3(startX + x * cellSize, startY + y * cellSize, 0);
              
                if (gridArray[x, y] != null)
                {
                    gridArray[x, y].transform.position = cellPosition;
                   
                }
              
            }
        }
    }

    //public void SetGridVisibility(bool visible)
    //{
    //    foreach (GameObject cell in gridArray)
    //    {
    //        cell.SetActive(visible);
    //    }
    //}

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        float startX = -width * cellSize / 2 + cellSize / 2;
        float startY = -height * cellSize / 2 + cellSize / 2;

        int x = Mathf.FloorToInt((worldPosition.x - startX) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - startY) / cellSize);
        return new Vector2Int(x, y);
    }

    //public bool IsValidPosition(int x, int y, Building building)
    //{
    //    int buildingWidth = building.width;
    //    int buildingHeight = building.height;
    //    bool allOverlap = true;

    //    Debug.Log($"Checking valid position for building at grid position ({x}, {y}) with width {buildingWidth} and height {buildingHeight}");

    //    for (int i = 0; i < buildingWidth; i++)
    //    {
    //        for (int j = 0; j < buildingHeight; j++)
    //        {
    //            int gridX = x + i;
    //            int gridY = y + j;

    //            // 索引范围检查
    //            if (gridX < 0 || gridX >= width || gridY < 0 || gridY >= height)
    //            {
    //                allOverlap = false;
    //                SetGridCellColor(gridX, gridY, new Color(0.99f, 0.38f, 0.27f)); // FC6145
    //                Debug.Log($"Grid position ({gridX}, {gridY}) is out of bounds.");
    //                continue;
    //            }

    //            // 检查gridArray是否为null
    //            if (gridArray[gridX, gridY] == null)
    //            {
    //                allOverlap = false;
    //                SetGridCellColor(gridX, gridY, new Color(0.99f, 0.38f, 0.27f)); // FC6145
    //                Debug.Log($"Grid cell at position ({gridX}, {gridY}) is null.");
    //                continue;
    //            }

    //            Vector3 cellPosition = gridArray[gridX, gridY].transform.position;
    //            Collider2D[] colliders = Physics2D.OverlapCircleAll(cellPosition, cellSize / 2);
    //            bool overlapsFloor = false;

    //            foreach (var collider in colliders)
    //            {
    //                if (collider.CompareTag("floor"))
    //                {
    //                    overlapsFloor = true;
    //                    break;
    //                }
    //            }

    //            if (!overlapsFloor)
    //            {
    //                allOverlap = false;
    //                SetGridCellColor(gridX, gridY, new Color(0.99f, 0.38f, 0.27f)); // FC6145
    //                Debug.Log($"Grid cell at position ({gridX}, {gridY}) does not overlap with any 'floor' tagged objects.");
    //            }
    //            else
    //            {
    //                SetGridCellColor(gridX, gridY, new Color(0.40f, 0.99f, 0.27f)); // 67FC45
    //                Debug.Log($"Grid cell at position ({gridX}, {gridY}) overlaps with a 'floor' tagged object.");
    //            }
    //        }
    //    }

    //    Debug.Log($"All cells overlap status: {allOverlap}");
    //    return allOverlap;
    //}
    public bool IsValidPosition(GameObject[,] gridArray)
    {
        bool allOverlap = true;

        

        //for (int x = 0; x < gridArray.GetLength(0); x++)
        //{
        //    for (int y = 0; y < gridArray.GetLength(1); y++)
        //    {
        //        if (gridArray[x, y] == null)
        //        {
        //            allOverlap = false;
                 
        //            continue;
        //        }

        //        Vector3 cellPosition = gridArray[x, y].transform.position;
        //        Collider2D[] colliders = Physics2D.OverlapCircleAll(cellPosition, cellSize / 2);
        //        bool overlapsFloor = true;

        //        foreach (var collider in colliders)
        //        {
        //            if (!collider.CompareTag("floor"))
        //            {
        //                overlapsFloor = false;
                      
        //                break;
        //            }
        //            else
        //            {
                       
        //            }
        //        }

        //        if (!overlapsFloor)
        //        {
                    
        //            allOverlap = false;
        //            SetGridCellColor(gridArray, x, y, new Color(0.99f, 0.38f, 0.27f)); // FC6145
        //        }
        //        else
        //        {
        //            Debug.Log("ss");
        //            SetGridCellColor(gridArray, x, y, new Color(0.40f, 0.99f, 0.27f)); // 67FC45
        //        }
        //    }
        //}

        return allOverlap;
    }


    public void SetGridCellColor(GameObject[,] gridArray, int x, int y, Color color)
    {
        if (gridArray[x, y] != null)
        {
            // 找到名为 "gridBottom" 的子对象
            Transform gridBottomTransform = gridArray[x, y].transform.Find("gridBottom");
            if (gridBottomTransform != null)
            {
                // 获取子对象的 SpriteRenderer 组件
                SpriteRenderer spriteRenderer = gridBottomTransform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // 设置颜色并调整透明度
                    color.a = 112 / 255f; // 将透明度设置为 112（0 到 255 范围内）
                    spriteRenderer.color = color;
                }
            }
            else
            {
                Debug.LogWarning($"gridBottom not found in GridCell[{x}, {y}]");
            }
        }
    }



    //public void ResetGridCellColors()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            SetGridCellColor(x, y, Color.white); // 重置为默认颜色
    //        }
    //    }
    //}

    public Vector3 GetWorldPosition(int x, int y)
    {
        float startX = -width * cellSize / 2 + cellSize / 2;
        float startY = -height * cellSize / 2 + cellSize / 2;
        return new Vector3(startX + x * cellSize, startY + y * cellSize, 0);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        float startX = -width * cellSize / 2 + cellSize / 2;
        float startY = -height * cellSize / 2 + cellSize / 2;
        x = Mathf.FloorToInt((worldPosition.x - startX) / cellSize);
        y = Mathf.FloorToInt((worldPosition.y - startY) / cellSize);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float startX = -width * cellSize / 2 + cellSize / 2;
        float startY = -height * cellSize / 2 + cellSize / 2;
        return new Vector3(startX + gridPosition.x * cellSize, startY + gridPosition.y * cellSize, 0);
    }

    //public void Build(int x, int y, Building building)
    //{


    //    if (IsValidPosition(x, y, building))
    //    {
    //        for (int i = 0; i < building.width; i++)
    //        {
    //            for (int j = 0; j < building.height; j++)
    //            {
    //                gridArray[x + i, y + j].GetComponent<GridCell>().PlaceBuilding(building);
    //            }
    //        }
    //    }
    //}
}
