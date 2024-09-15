using UnityEngine;
using System.Collections;

public class AutomaticDoor : MonoBehaviour
{
    private Animator animator; // ����������
    private int playerCount = 0; // �ڴ��������ڵ��������
    private BoxCollider2D DoorCollider;
    private BoxCollider2D doorPicCollider;
    void Start()
    {
        Transform doorPicTransform = transform.Find("doorPic");
        doorPicCollider = doorPicTransform.GetComponent<BoxCollider2D>();
        DoorCollider = GetComponent<BoxCollider2D>();
        animator = doorPicTransform.GetComponent<Animator>(); // ��ȡAnimator���
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the door object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ����ǩΪPlayer�Ķ���
        {
            playerCount++; // ������Ҽ���
            if (playerCount == 1) // ��һ����ҽ���
            {
                OpenDoor();
            }
        }
    }
    public void OpenDoor()
    {
        animator.SetBool("IsOpen", true); // ���ö���״̬Ϊ��

        StartCoroutine(setDoorCollider(true));
    }
    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false); // ���ö���״̬Ϊ�ر�
        doorPicCollider.isTrigger = false;
    }
        void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--; // ������Ҽ���
            if (playerCount == 0) // ������Ҷ��뿪
            {
                CloseDoor();
            }
        }
    }

    private IEnumerator setDoorCollider(bool trigger)
    {
        // �ȴ�1��
        yield return new WaitForSeconds(0.5f);

        // �ӳٺ�ִ�еĴ���
        doorPicCollider.isTrigger = trigger;
    }
}
