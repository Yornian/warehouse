using UnityEngine;

public class Door : MonoBehaviour
{
    // 子对象 doorPic 的名称
    public string doorPicName = "doorPic";
    public GameObject doorPic;
    // 动画控制器
    public string sceneName ;
    private Animator doorAnimator;

    void Start()
    {
        
        // 通过名称获取 doorPic 对象
        doorPic = GameObject.Find(doorPicName);

        if (doorPic != null)
        {
            doorAnimator = doorPic.GetComponent<Animator>();
        }

        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on doorPic!");
        }
    }

    // 开门方法
    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("ifOpen", true);
        }
    }

    // 关门方法
    public void CloseDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("ifOpen", false);
        }
    }
}
