using System.Collections;
using UnityEngine;
using TMPro; // ȷ������ TMPro �����ռ�

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro ���
    public float typingSpeed = 0.05f; // �����ٶȣ����Ը�����Ҫ����

    public string fullText;
    private Coroutine typingCoroutine;

    private void Start()
    {
      
       // StartTyping();
    }
    public void setOrderText(int orderCount)
    {
        fullText = "Today, we successfully completed " + orderCount + " orders, with a total income of " + orderCount + "��100="+ orderCount*100+".If there are any problematic orders, they will be returned tomorrow.Please address them promptly.";
    }
    public void setText(string  txt)
    {
        fullText = txt;
    }
    
        public void StartTyping()
    {
        // ���֮ǰ�����ڽ��е�Э�̣���ֹͣ
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // �����µĴ��ֻ�Ч��Э��
        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string text)
    {
        textMeshPro.text = ""; // ����ı�
        foreach (char letter in text.ToCharArray())
        {
            textMeshPro.text += letter; // ����ַ����ı���
            yield return new WaitForSeconds(typingSpeed); // �ȴ�ָ��ʱ��
        }
    }
}
