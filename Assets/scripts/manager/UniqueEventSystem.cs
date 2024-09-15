using UnityEngine;
using UnityEngine.EventSystems;

public class UniqueEventSystem : MonoBehaviour
{
    private static EventSystem instance;

    void Awake()
    {
        EventSystem current = GetComponent<EventSystem>();

        // ����Ƿ��Ѵ���һ��EventSystemʵ��
        if (instance == null)
        {
            // ��������ڣ����õ�ǰʵ��Ϊȫ��ʵ���������Ϊ������
            instance = current;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �����ǰ������EventSystem����ȫ��ʵ����������
            if (instance != current)
            {
                Destroy(gameObject);
            }
        }
    }
}
