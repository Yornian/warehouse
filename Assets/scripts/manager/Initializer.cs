using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        // ͨ������ Instance ������ȷ�� GameManager ������
        var manager = GameManager.Instance;
        Debug.Log("GameManager has been initialized");
    }
}
