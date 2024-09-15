using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    public string bodyPath;
    public string hairPath;
    public string outfitPath;
    public string accessoryPath;
    public SpriteRenderer body;
    public SpriteRenderer hair;
    public SpriteRenderer outfit;
    public SpriteRenderer accessory;

    // ���ڲ��Եľ������飬���滻Ϊ��ľ���
    public Sprite[] bodySprites;
    public Sprite[] hairSprites;
    public Sprite[] outfitSprites;
    public Sprite[] accessorySprites;

    private Animator animator;
    private Vector3 vector;

    //
    private Transform accessoryTransform ;
    private SpriteRenderer accessorySpriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();

        init();
        // LoadSpritesIntoArray(bodyPath, 4, out bodySprites);
        // LoadSpritesIntoArray(hairPath, 4, out hairSprites);
        // LoadSpritesIntoArray(outfitPath, 4, out outfitSprites);
        // LoadSpritesIntoArray(accessoryPath, 4, out accessorySprites);

    }
    void init ()
    {
        Transform BodyTransform = transform.Find("Body");
        accessoryTransform = BodyTransform.Find("Accessory");
        if (accessoryTransform != null)
        {
           
            accessorySpriteRenderer = accessoryTransform.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.Log("none");
        }
    }
    void SetAccessoryLayer(int i)
    {
        if (accessorySpriteRenderer != null)
        {
            // ����orderInLayer
            accessorySpriteRenderer.sortingOrder = i;
            Debug.Log("i");
        }
       
    }
    private void Update()
    {
        vector = Vector3.zero;
        vector.x = Input.GetAxis("Horizontal");
        vector.y = Input.GetAxis("Vertical");
        UpdateAnimationAndMove();
    }
    void UpdateAnimationAndMove()
    {
        if (vector != Vector3.zero)
        {
            // Move();
            if(vector.y>0)
            {
                SetAccessoryLayer(2);
                
            }
            if (vector.y < 0)
            {
                SetAccessoryLayer(-1);
                
            }

            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            //animator.SetBool("moving", true);
            
        }
      //  else
       // {
        //    animator.SetBool("moving", false);
        //    if (FindObjectOfType<SoundManager>().SoundIsPlaying("Walk"))
        //    {
         //       FindObjectOfType<SoundManager>().Stop("Walk");
         //   }
       // }
    }
    void LoadSpritesIntoArray(string resourcePath, int maxSprites, out Sprite[] spriteArray)
    {
        // ��Resources�ļ����м������о���
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(resourcePath);

        // ���·���Ƿ���ȷ�Լ��Ƿ��о��鱻����
        if (loadedSprites.Length == 0)
        {
            Debug.LogWarning($"No sprites found at path: {resourcePath}. Please check the path correctness.");
            spriteArray = new Sprite[0]; // û���ҵ�����ʱ����һ��������
            return;
        }

        // ȷ�������СΪ��Сֵ���ҵ��ľ���������maxSprites�еĽ�С��
        int arraySize = Mathf.Min(loadedSprites.Length, maxSprites);
        spriteArray = new Sprite[arraySize];

        // �������
        for (int i = 0; i < arraySize; i++)
        {
            spriteArray[i] = loadedSprites[i];
        }
    }

    Character(string bodyName, string hairName, string outfitName, string accessoryName)
    {
        Sprite body = Resources.Load<Sprite>("Body_01");


    }

   


    // ��������
    public void ChangeBody(int index)
    {
        if (index >= 0 && index < bodySprites.Length)
        {
            body.sprite = bodySprites[index];
        }
    }

    // ����ͷ��
    public void ChangeHair(int index)
    {
        if (index >= 0 && index < hairSprites.Length)
        {
            hair.sprite = hairSprites[index];
        }
    }

    // �����·�
    public void ChangeClothes(int index)
    {
        if (index >= 0 && index < outfitSprites.Length)
        {
            outfit.sprite = outfitSprites[index];
        }
    }

    // ��������
    public void ChangeAccessory(int index)
    {
        if (index >= 0 && index < accessorySprites.Length)
        {
            accessory.sprite = accessorySprites[index];
        }
    }
}
