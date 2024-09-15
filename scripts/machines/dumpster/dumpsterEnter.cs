using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dumpsterEnter : MonoBehaviour
{
    private Animator animator; // 动画控制器
    // Start is called before the first frame update
    public BoxCollider2D AnimCollider;
    void Start()
    {
        animator = GetComponent<Animator>(); // 获取Animator组件
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDumpster()
    {
        animator.SetBool("IsOpen", true); // 设置动画状态为打开


    }
    public void CloseDumpster()
    {
        animator.SetBool("IsOpen", false); // 设置动画状态为关闭

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 检测标签为Player的对象
        {

            OpenDumpster();

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            CloseDumpster();

        }
    }
}
