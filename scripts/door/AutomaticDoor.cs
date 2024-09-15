using UnityEngine;
using System.Collections;

public class AutomaticDoor : MonoBehaviour
{
    private Animator animator; // 动画控制器
    private int playerCount = 0; // 在触发区域内的玩家数量
    private BoxCollider2D DoorCollider;
    private BoxCollider2D doorPicCollider;
    void Start()
    {
        Transform doorPicTransform = transform.Find("doorPic");
        doorPicCollider = doorPicTransform.GetComponent<BoxCollider2D>();
        DoorCollider = GetComponent<BoxCollider2D>();
        animator = doorPicTransform.GetComponent<Animator>(); // 获取Animator组件
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the door object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 检测标签为Player的对象
        {
            playerCount++; // 增加玩家计数
            if (playerCount == 1) // 第一个玩家进入
            {
                OpenDoor();
            }
        }
    }
    public void OpenDoor()
    {
        animator.SetBool("IsOpen", true); // 设置动画状态为打开

        StartCoroutine(setDoorCollider(true));
    }
    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false); // 设置动画状态为关闭
        doorPicCollider.isTrigger = false;
    }
        void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--; // 减少玩家计数
            if (playerCount == 0) // 所有玩家都离开
            {
                CloseDoor();
            }
        }
    }

    private IEnumerator setDoorCollider(bool trigger)
    {
        // 等待1秒
        yield return new WaitForSeconds(0.5f);

        // 延迟后执行的代码
        doorPicCollider.isTrigger = trigger;
    }
}
