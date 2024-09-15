using UnityEngine;

public class Door : MonoBehaviour
{
    // �Ӷ��� doorPic ������
    public string doorPicName = "doorPic";
    public GameObject doorPic;
    // ����������
    public string sceneName ;
    private Animator doorAnimator;

    void Start()
    {
        
        // ͨ�����ƻ�ȡ doorPic ����
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

    // ���ŷ���
    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("ifOpen", true);
        }
    }

    // ���ŷ���
    public void CloseDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("ifOpen", false);
        }
    }
}
