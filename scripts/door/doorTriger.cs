using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class doorTriger : MonoBehaviour
{
    private BoxCollider2D doorTrigerCollider;
    public string targetSceneName = "PostOffice"; // 设置目标场景的名称
    public Vector3 nextScanePos;
  
    // Start is called before the first frame update
    void Start()
    {
        doorTrigerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("PlayerX", nextScanePos.x);
            PlayerPrefs.SetFloat("PlayerY", nextScanePos.y);
            PlayerPrefs.SetFloat("PlayerZ", nextScanePos.z);
            // 加载指定的场景
            SceneManager.LoadScene(targetSceneName);
            
        }
    }
}
