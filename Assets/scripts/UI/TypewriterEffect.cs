using System.Collections;
using UnityEngine;
using TMPro; // 确保引入 TMPro 命名空间

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro 组件
    public float typingSpeed = 0.05f; // 输入速度，可以根据需要调整

    public string fullText;
    private Coroutine typingCoroutine;

    private void Start()
    {
      
       // StartTyping();
    }
    public void setOrderText(int orderCount)
    {
        fullText = "Today, we successfully completed " + orderCount + " orders, with a total income of " + orderCount + "×100="+ orderCount*100+".If there are any problematic orders, they will be returned tomorrow.Please address them promptly.";
    }
    public void setText(string  txt)
    {
        fullText = txt;
    }
    
        public void StartTyping()
    {
        // 如果之前有正在进行的协程，先停止
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 启动新的打字机效果协程
        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string text)
    {
        textMeshPro.text = ""; // 清空文本
        foreach (char letter in text.ToCharArray())
        {
            textMeshPro.text += letter; // 添加字符到文本中
            yield return new WaitForSeconds(typingSpeed); // 等待指定时间
        }
    }
}
