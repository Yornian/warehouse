using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAnimControllor : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update

    void Start()
    {

        animator = GetComponent<Animator>();
       // animator.enabled = false;
   
    }
   
  
  
    public void setMoveXAndMoveY(Vector2 MoveVector)
    {
        if(animator != null)
        {
            animator.SetFloat("moveX", MoveVector.x);
            animator.SetFloat("moveY", MoveVector.y);
        }
       


    }
    public void setBeHold(bool ifHold)
    {
        if (animator != null)
        {
            animator.SetBool("beHold", ifHold);
        }


    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
