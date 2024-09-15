using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum PlayerState
{
    doNothing,
    walk,
    usingPhone,
    pick,
    lift,
    reading,
    sit,
    building


}
public class PlayerControler : MonoBehaviour
{
    private Animator[] allChildAnimators;//Ǳ������ ���ܶ������Ϊ��
    private Vector2 MoveVector;
    public Vector2 lastMotionVector;
    public PlayerState currentState;
    private float speed = 2.2f;
    Rigidbody2D rigidbody2d;
    private List<Transform> childTransforms;
    public string[] childNames = {"body", "outfit" };
    public BoxCollider2D boxCollider2D;


    //pick
    private pick pickScript;
  
    public GameObject pickedItem = null; //�Ѿ�����������Ʒ 
    public Transform HoldPos;
    float pickupRange =0.5f; //ʰȡ��Ʒ�ķ�Χ 



    //drop
    private float throwDis = 0.2f; // Ͷ������
   



    //����E��
    private bool isPressingE = false;
    private float pressStartTime;
    private float longPressDuration = 0.5f;  // ����ʱ����ֵ

    //����ֵ
    private Bar PowerBar;
    private int powerExpend=5;

    private string powerWarn= "Your power is low ,go to bed.";
    // Start is called before the first frame update

    public enum interactWay
    {
         shortE,
        longE,
        shortF,
        leftClick


    }

    public Vector3 getReallyPos()
    {

        Vector3 pos = transform.position;
        pos.y -= 0.45f;
        pos.x += 0.15f;
        return pos;
    }
    public void switchSpriteLibrary(string part, string spriteLibraryName)
    {
       
            foreach (Transform childTransform in childTransforms)
            {
                if (childTransform.name == part)
                {
                  animController childScript = childTransform.GetComponent<animController>();
                    if (childScript != null)
                    {
                        childScript.changeSpriteLibrary(spriteLibraryName);
                    }
                }
            }
        

    }
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
          pickScript = GetComponent<pick>();
        currentState = PlayerState.walk;
        allChildAnimators = GetComponentsInChildren<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        lastMotionVector = Vector2.zero;
        childTransforms = new List<Transform>();
        foreach (string childName in childNames)
        {
            Transform childTransform = transform.Find(childName);
            if (childTransform != null)
            {
                childTransforms.Add(childTransform);
            }
        }
        PowerBar = GameObject.Find("PowerBar").GetComponent<Bar>();
       
    }
   
  
    // Update is called once per frame
    private void Update()  //��Ҫ�Ż����������߼���Э������״̬ ע��doNothing���״̬ 
    {
        MoveVector.x = Input.GetAxisRaw("Horizontal");
        MoveVector.y = Input.GetAxisRaw("Vertical");

        if (currentState != PlayerState.doNothing)
        {

            updateHoldItemPos();//����������Ʒ��λ��

            if (Input.GetMouseButtonDown(0))
            {
                //if û�н���
                interact(interactWay.leftClick);
            }

            // readingû�е���Ŷ

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                isPressingE = true;
                pressStartTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
              
                isPressingE = false;
                float pressDuration = Time.time - pressStartTime;

                if (pressDuration >= longPressDuration)  //����
                {
                    interact(interactWay.longE);
                }
                else                             //�̰� 
                {
                  
                    interact(interactWay.shortE);
                 
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {

                interact(interactWay.shortF);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //if (currentState != PlayerState.usingPhone && currentState != PlayerState.pick && currentState != PlayerState.reading && currentState != PlayerState.sit)
                //{

                //    StartCoroutine(reading());
                //}
                //else if (currentState == PlayerState.reading)
                //{
                //    StartCoroutine(stopReading());
                //}


            }
        }
    }
    void FixedUpdate()
        {


        if (currentState != PlayerState.doNothing)
        {

            if (currentState == PlayerState.walk || currentState == PlayerState.lift)
            {
                UpdateAnimationAndMove();
            }
        }




    }
    public void dropItemInPos(Vector3 pos,bool ifOpenCollider,bool IfDistroy)
    {
        pickScript.dropItemToSpecifiedPosition(pickedItem, pos, ifOpenCollider);



        StartCoroutine(stopLift());
        pickedItem.GetComponent<item>().beHold = false;
        if (IfDistroy)
        {
            StartCoroutine(destroyObjectAfterDelay(pickedItem));
        }
        pickedItem = null;

    }
    private IEnumerator destroyObjectAfterDelay(GameObject item)
    {
        
        yield return new WaitForSeconds(0.1f);

       
       Destroy(item);
        pickedItem = null;
    }
    private GameObject getMouseDownObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {

            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

  
    private void interact(interactWay way)
        
    {
         if (way == interactWay.leftClick)
          {
            GameObject targetObject = getMouseDownObject();
            if (targetObject != null)
            {
                IInteractable interactable = targetObject.GetComponent<IInteractable>();
                if (interactable != null)
                {

                    interactable.PerformInteractionClick(this.gameObject);


                }
            }
         }
        else
        {
            GameObject interactedObject;
            HoldPos = transform.Find("hold");

            if (!Mathf.Approximately(MoveVector.x, 0.0f) || !Mathf.Approximately(MoveVector.y, 0.0f))
            {
                interactedObject = pickScript.tryPick(getReallyPos(), MoveVector,pickedItem);
            }
            else
            {
                interactedObject = pickScript.tryPick(getReallyPos(), lastMotionVector, pickedItem);

            }
            Debug.Log(interactedObject);

            if (interactedObject != null)
            {
               
                IInteractable interactable = interactedObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (way == interactWay.longE)
                    {
                        interactable.PerformInteractionLongPress(this.gameObject);

                    }
                    else if (way == interactWay.shortE)
                    {
                        interactable.PerformInteraction(this.gameObject);
                    }
                    else if (way == interactWay.shortF)
                    {
                        interactable.PerformInteractionF(this.gameObject);
                    }


                }
            }
            else
            {
                if (pickedItem != null)
                {
                    IInteractable interactable = pickedItem.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.PerformInteraction(this.gameObject);
                    }
                }


            }

        }

    }
    public void openDoor()
    {
        // ��ȡ detectionBox �����е���ײ��
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f);
        GameObject nearestDoor = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("door"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestDoor = collider.gameObject;
                }
            }
        }

        if (nearestDoor != null)
        {
            currentState = PlayerState.doNothing;
            // �����Ŷ���� OpenDoor() ����
            nearestDoor.SendMessage("OpenDoor");
            Door doorComponent = nearestDoor.GetComponent<Door>();
            string sceneName = doorComponent.sceneName;
            // ֹͣ�����ƶ����κβ���
     

            // ֹͣ�����ƶ��������߼�����������ƶ�����ȣ�
            // ���磺GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // ���磺GetComponent<Rigidbody2D>().isKinematic = true;

            // 1 �����ת������ "firstScene"
            if(sceneName!=null)
            {
                StartCoroutine(LoadSceneAfterDelay(sceneName, 1f));
            }
          
        }
    }
    private System.Collections.IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void Move()
    {
            Vector2 position = rigidbody2d.position;
            position.x += speed * MoveVector.x * Time.deltaTime;
            position.y += speed * MoveVector.y * Time.deltaTime;

            rigidbody2d.MovePosition(position);
    }
     
    
    void UpdateAnimationAndMove()
        {

            if (!Mathf.Approximately(MoveVector.x, 0.0f) || !Mathf.Approximately(MoveVector.y, 0.0f))
            {


                Move();

                MoveVector.Normalize();
                for (int i = 0; i < allChildAnimators.Length; i++)
                {
                    Animator animator = allChildAnimators[i];
                    animator.SetFloat("moveX", MoveVector.x);
                    animator.SetFloat("moveY", MoveVector.y);
                    animator.SetBool("moving", true);

                }

                lastMotionVector = new Vector2(MoveVector.x, MoveVector.y);

            }
            else
            {
                for (int i = 0; i < allChildAnimators.Length; i++)
                {
                    Animator animator = allChildAnimators[i];
                    animator.SetFloat("moveX", lastMotionVector.x);
                    animator.SetFloat("moveY", lastMotionVector.y);

                    animator.SetBool("moving", false);

                }




            }
        }
    public void spawnItemToHold(GameObject ItemToPick )
    {

        StartCoroutine(lift());
        pickedItem = ItemToPick;
        HoldPos = transform.Find("hold");
        pickScript.pickItemToHold(ItemToPick, HoldPos, lastMotionVector);
       
    }
    public void pickItemToHold(GameObject item)
    {

        if (currentState != PlayerState.lift && currentState != PlayerState.pick && currentState != PlayerState.usingPhone && currentState != PlayerState.reading && currentState != PlayerState.sit)
        {


            if (pickScript != null)
            {

                if (PowerBar.currentValue < powerExpend)
                {
                   OrderManager.Instance.showWarn(powerWarn);
                }
                else
                {
                    pickedItem = item;
                    if (pickedItem != null)
                    {



                        StartCoroutine(lift());
                        HoldPos = transform.Find("hold");
                        pickScript.pickItemToHold(pickedItem, HoldPos, lastMotionVector);
                        pickedItem.GetComponent<item>().beHold = true;
                        PowerBar.changeAndUpdateValue(-powerExpend);






                    }
                }
              
             //  pickedItem = pickScript.tryPick(transform.position, pickupRange, MoveVector);
               
            }

        }

    }//������һֱ������Ʒ
    public void updateHoldItemPos()//����������Ʒ��λ��
    {
        if (pickedItem != null)
        {
           
            if (!Mathf.Approximately(MoveVector.x, 0.0f) || !Mathf.Approximately(MoveVector.y, 0.0f))
            {

                pickScript.updateHoldItemPos(pickedItem, MoveVector);

            }
            else
            {
                pickScript.updateHoldItemPos(pickedItem, lastMotionVector);

            }

        }


    }
    public void dropItem()
    {
        pickedItem.GetComponent<item>().beHold = false;
        if (!Mathf.Approximately(MoveVector.x, 0.0f) || !Mathf.Approximately(MoveVector.y, 0.0f))
        {
            pickScript.dropItemToGround(pickedItem, MoveVector,throwDis);

        }
        else
        {
        
            pickScript.dropItemToGround(pickedItem, lastMotionVector, throwDis);


        }
        pickedItem = null;
      
        StartCoroutine(stopLift());




    }
    private IEnumerator usePhone()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("usingPhone", true);

        }

        currentState = PlayerState.usingPhone;
        yield return null;


    }
    private IEnumerator stopUsePhone()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("usingPhone", false);

        }

        currentState = PlayerState.walk;
        yield return null;


    }
    private IEnumerator pick()
    {
        if (currentState == PlayerState.pick)
        {
            
        }
        else
        {
            for (int i = 0; i < allChildAnimators.Length; i++)
            {
                Animator animator = allChildAnimators[i];
                animator.SetBool("pick", true);

            }
            currentState = PlayerState.pick;
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < allChildAnimators.Length; i++)
            {
                Animator animator = allChildAnimators[i];
                animator.SetBool("pick", false);

            }
            currentState = PlayerState.walk;
        }
      

       
    }
    private IEnumerator lift()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("lift", true);

        }

        currentState = PlayerState.lift;
        yield return null;


    }
    private IEnumerator Stoplift()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("lift", false);

        }

        currentState = PlayerState.walk;
        yield return null;


    }
    private IEnumerator stopLift()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("lift", false);

        }

        currentState = PlayerState.walk;
        yield return null;


    }
    private IEnumerator reading()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("reading", true);

        }

        currentState = PlayerState.reading;
        yield return null;


    }
    private IEnumerator stopReading()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("reading", false);

        }

        currentState = PlayerState.walk;
        yield return null;


    }
    private IEnumerator sitLeft()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("sitLeft", true);

        }

        currentState = PlayerState.sit;
        yield return null;

    }
    private IEnumerator sitRight()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("sitRight", true);

        }

        currentState = PlayerState.sit;
        yield return null;

    }
    private IEnumerator stopSit()
    {
        for (int i = 0; i < allChildAnimators.Length; i++)
        {
            Animator animator = allChildAnimators[i];
            animator.SetBool("sitRight", false);
            animator.SetBool("sitLeft", true);

        }

        currentState = PlayerState.walk;
        yield return null;

    }
    bool IsParentTarget(GameObject child)
    {
        // ��ȡ������
        Transform parentTransform = child.transform.parent;

        // ��鸸�����Ƿ�������Ƿ���Ŀ�길����
        return parentTransform != null && parentTransform.gameObject == transform;
    }
   
}

//if (currentState != PlayerState.usingPhone && currentState != PlayerState.pick && currentState != PlayerState.reading && currentState != PlayerState.sit)
//{

//    StartCoroutine(usePhone());
//}
//else if (currentState == PlayerState.usingPhone)
//{
//    StartCoroutine(stopUsePhone());
//}
