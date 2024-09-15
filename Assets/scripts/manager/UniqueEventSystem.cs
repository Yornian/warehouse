using UnityEngine;
using UnityEngine.EventSystems;

public class UniqueEventSystem : MonoBehaviour
{
    private static EventSystem instance;

    void Awake()
    {
        EventSystem current = GetComponent<EventSystem>();

        // 检查是否已存在一个EventSystem实例
        if (instance == null)
        {
            // 如果不存在，设置当前实例为全局实例，并标记为不销毁
            instance = current;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果当前场景的EventSystem不是全局实例，销毁它
            if (instance != current)
            {
                Destroy(gameObject);
            }
        }
    }
}
