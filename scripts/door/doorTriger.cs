using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ���볡�����������ռ�

public class doorTriger : MonoBehaviour
{
    private BoxCollider2D doorTrigerCollider;
    public string targetSceneName = "PostOffice"; // ����Ŀ�곡��������
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
        // ����Ƿ������
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("PlayerX", nextScanePos.x);
            PlayerPrefs.SetFloat("PlayerY", nextScanePos.y);
            PlayerPrefs.SetFloat("PlayerZ", nextScanePos.z);
            // ����ָ���ĳ���
            SceneManager.LoadScene(targetSceneName);
            
        }
    }
}
