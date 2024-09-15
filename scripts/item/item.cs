using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class  item : MonoBehaviour, IInteractable
{
    public string name;
    public bool startCollision=false;
    public bool canFix;
    public bool ifBroken;
    public float BrokenProbability = 0.38f;
    public Sprite GoodSprite;
    public Sprite BrokenSprite;
    private Transform itemSpriteTransform;
    private SpriteRenderer spriteRenderer;
    public bool beHold=false;
    public bool ifFood;
    private void Start()
    {
        
        itemSpriteTransform = transform.Find("itemSprite");
        spriteRenderer= itemSpriteTransform.GetComponent<SpriteRenderer>();
        GoodSprite = spriteRenderer.sprite;
        if(startCollision)
        {
            setSelfCollision(true);
        }
        else if(!startCollision)
        {
            setSelfCollision(false);
        }
       setIfBroken(ifBroken);
    }
    public void switchPic()
    {
        
   
        if (ifBroken)
        {
            spriteRenderer.sprite = BrokenSprite;
        }
        else
        {

            spriteRenderer.sprite = GoodSprite;
        }
        

    }
    public void setIfBroken(bool  isBroken)
    {


            ifBroken = isBroken;
       if(!ifFood)
        { switchPic(); }
           
    }
    public void PerformInteractionF(GameObject Player)
    {
        
    }
    public void PerformInteractionClick(GameObject Player)
    {

    }
    public void PerformInteractionLongPress(GameObject Player)
    {


    }
    public void setSelfCollision(bool ifCollision)
    {

        if (ifCollision)
        {
            //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
               
            //    rb.isKinematic = false;  
            //}
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = false;  
            }
        }
        else
        {
            //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    rb.velocity = Vector2.zero;  // 清除所有现有动量
            //    rb.angularVelocity = 0;  // 清除旋转动量
            //    rb.isKinematic = true;  // 停止物理计算

            //}
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = true;  // 将碰撞体设置为触发器
            }

        }
      
    }
    public void PerformInteraction(GameObject Player)

    {
        
        PlayerControler PlayerControlerScript = Player.GetComponent<PlayerControler>();
       if( PlayerControlerScript.pickedItem == null)
        
       {
            PlayerControlerScript.pickItemToHold(this.gameObject);
       }
        else
        {
          
            PlayerControlerScript.dropItem();

        }
       

    }

}
[System.Serializable]
public class itemData
{
    public string itemName;
  
    public float itemCost { get; set; }
    public string problem { get; set; }
    public float badProbability
    { 
        get {  return badProbability;  }
        set
        {
            if (value < 0.0f)
            {
                badProbability = 0.0f;
            }
            else if (value > 1.0f)
            {
                badProbability = 1.0f;
            }
            else
            {
                badProbability = value;
            }
        }
    }
    public string badResult { get; set; }
    // Start is called before the first frame update
    void Start()
    {

    }
    private bool ShouldTriggerBadEvent()
    {
        // 生成一个0到1之间的随机数
        float randomValue = Random.Range(0f, 1f);
        // 如果随机数小于等于设定的概率，则触发事件
        return randomValue <= badProbability;
    }

    // Update is called once per frame
    void Update()
    {

    }





}
