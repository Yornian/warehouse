using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        // 通过访问 Instance 属性来确保 GameManager 被创建
        var manager = GameManager.Instance;
        Debug.Log("GameManager has been initialized");
    }
}
