using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonCreator : MonoBehaviour
{
    public GameObject templateButton; // ģ�尴ťGameObject
    public string fatherCanvasTag = "Canvas"; // ��Canvas�ı�ǩ
    public float verticalMargin = 10f; // ��ֱ���
    public float horizontalMargin = 10f; // ˮƽ���
    private List<Button> buttonsList = new List<Button>(); // ��ť�б�
    public int createNum; // ������ť������
    public int buttonsPerRow; // ÿ�еİ�ť����

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


        // ���ݰ�ť������ÿ�еİ�ť���������������
        int totalRows = Mathf.CeilToInt((float)createNum / buttonsPerRow);

        // ����Canvas����Ŀ�Ⱥ͸߶ȣ�ȷ���ڱ߽籣��margin
        float requiredWidth = buttonsPerRow * (NBwidth + horizontalMargin) + horizontalMargin;
        float requiredHeight = totalRows * (NBheight + verticalMargin) + verticalMargin;

        // �����Ҫ������Canvas�ĳߴ�
        fatherCanvasRectTransform.sizeDelta = new Vector2(requiredWidth, requiredHeight);

        //fatherCanvasRectTransform.anchoredPosition = Vector2.zero;

        // ��Canvas�����Ͻǿ�ʼ���а�ť
        float currentY = fatherCanvasRectTransform.rect.height / 2 - NBheight / 2 - verticalMargin;
        float currentX = -fatherCanvasRectTransform.rect.width / 2 + NBwidth / 2 + horizontalMargin;

        for (int i = 0; i < createNum; i++)
        {
            // ��ǰ�еİ�ť����������
            if (i % buttonsPerRow == 0 && i != 0)
            {
                currentX = -fatherCanvasRectTransform.rect.width / 2 + NBwidth / 2 + horizontalMargin; // ����xλ��
                currentY -= (NBheight + verticalMargin); // ����yλ��
            }

            // ʵ�����������°�ť
            GameObject newButtonObj = Instantiate(templateButton, fatherCanvasTransform);
            newButtonObj.SetActive(true);
            Button newButton = newButtonObj.GetComponent<Button>();

            // �����°�ť��λ��
            RectTransform newButtonRectTransform = newButton.GetComponent<RectTransform>();
            newButtonRectTransform.anchoredPosition = new Vector2(currentX, currentY);

            // ��ӵ���ť�б�
            buttonsList.Add(newButton);

            // ����xλ��
            currentX += (NBwidth + horizontalMargin);
        }

        // ��հ�ť�б��Ա��´�ʹ��
        buttonsList.Clear();
    }
}
