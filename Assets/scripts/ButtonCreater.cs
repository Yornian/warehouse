using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonCreator : MonoBehaviour
{
    public GameObject templateButton; // 模板按钮GameObject
    public string fatherCanvasTag = "Canvas"; // 父Canvas的标签
    public float verticalMargin = 10f; // 垂直间距
    public float horizontalMargin = 10f; // 水平间距
    private List<Button> buttonsList = new List<Button>(); // 按钮列表
    public int createNum; // 创建按钮的数量
    public int buttonsPerRow; // 每行的按钮数量

    private void Start()
    {
        CreateButtons(createNum);
    }

 

    public void CreateButtons(int createNum)
    {
        if (buttonsPerRow > createNum)
        {

            buttonsPerRow = createNum;
        }
        RectTransform templateButtonRectTransform = templateButton.GetComponent<RectTransform>();
        Transform fatherCanvasTransform = GameObject.FindGameObjectWithTag(fatherCanvasTag).transform;
        RectTransform fatherCanvasRectTransform = fatherCanvasTransform.GetComponent<RectTransform>();

        float NBwidth = templateButtonRectTransform.rect.width;
        float NBheight = templateButtonRectTransform.rect.height;


        // 根据按钮总数和每行的按钮数计算所需的行数
        int totalRows = Mathf.CeilToInt((float)createNum / buttonsPerRow);

        // 计算Canvas所需的宽度和高度，确保在边界保留margin
        float requiredWidth = buttonsPerRow * (NBwidth + horizontalMargin) + horizontalMargin;
        float requiredHeight = totalRows * (NBheight + verticalMargin) + verticalMargin;

        // 如果需要，增加Canvas的尺寸
        fatherCanvasRectTransform.sizeDelta = new Vector2(requiredWidth, requiredHeight);

        //fatherCanvasRectTransform.anchoredPosition = Vector2.zero;

        // 从Canvas的左上角开始排列按钮
        float currentY = fatherCanvasRectTransform.rect.height / 2 - NBheight / 2 - verticalMargin;
        float currentX = -fatherCanvasRectTransform.rect.width / 2 + NBwidth / 2 + horizontalMargin;

        for (int i = 0; i < createNum; i++)
        {
            // 当前行的按钮已满，换行
            if (i % buttonsPerRow == 0 && i != 0)
            {
                currentX = -fatherCanvasRectTransform.rect.width / 2 + NBwidth / 2 + horizontalMargin; // 重置x位置
                currentY -= (NBheight + verticalMargin); // 下移y位置
            }

            // 实例化并设置新按钮
            GameObject newButtonObj = Instantiate(templateButton, fatherCanvasTransform);
            newButtonObj.SetActive(true);
            Button newButton = newButtonObj.GetComponent<Button>();

            // 设置新按钮的位置
            RectTransform newButtonRectTransform = newButton.GetComponent<RectTransform>();
            newButtonRectTransform.anchoredPosition = new Vector2(currentX, currentY);

            // 添加到按钮列表
            buttonsList.Add(newButton);

            // 更新x位置
            currentX += (NBwidth + horizontalMargin);
        }

        // 清空按钮列表以备下次使用
        buttonsList.Clear();
    }
}
