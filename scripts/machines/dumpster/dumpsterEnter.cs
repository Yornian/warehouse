using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dumpsterEnter : MonoBehaviour
{
    private Animator animator; // ����������
    // Start is called before the first frame update
    public BoxCollider2D AnimCollider;
    void Start()
    {
        animator = GetComponent<Animator>(); // ��ȡAnimator���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDumpster()
    {
        animator.SetBool("IsOpen", true); // ���ö���״̬Ϊ��


    }
    public void CloseDumpster()
    {
        animator.SetBool("IsOpen", false); // ���ö���״̬Ϊ�ر�

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ����ǩΪPlayer�Ķ���
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
