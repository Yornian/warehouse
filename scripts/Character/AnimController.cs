using UnityEngine;
using UnityEngine.U2D.Animation;

public class animController: MonoBehaviour
{
    public SpriteLibrary spriteLibrary;
    private SpriteRenderer spriteRenderer;
   
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }
     void Start()
    {
       // IdleLeftChangeSprite("1");
    }
    public void changeSpriteLibrary(string assetName)
    {
        string assetPath = $"spriteLibrary/{assetName}";
        SpriteLibraryAsset asset = Resources.Load<SpriteLibraryAsset>(assetPath);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (asset != null)
        {
            spriteLibrary.spriteLibraryAsset = asset;
           // Debug.Log($"Switched to {assetName} successfully.");
        }
      
    }
    public void IdleLeftChangeSprite( string label)
    {
        
        spriteRenderer.sprite = spriteLibrary.GetSprite("idleLeft", label);
        
    }
    public void IdleRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("idleRight", label);
        
    }
    public void IdleUpChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("idleUp", label);
       
    }
    public void IdleDownChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("idleDown", label);
       
    }
    //walk--------------------------------------
    public void WalkUpChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("walkUp", label);
        
    }
    public void WalkDownChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("walkDown", label);

    }
    public void WalkLeftChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("walkLeft", label);

    }
    public void WalkRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("walkRight", label);

    }
    // use phone
    public void startUsePhoneChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("startUsePhone", label);

    }
    public void usingPhoneChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("usingPhone", label);

    }
    //pick
    public void pickRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("pickRight", label);

    }
    public void pickLeftChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("pickLeft", label);

    }
    public void pickUpChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("pickUp", label);

    }
    public void pickDownChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("pickDown", label);

    }
    //lift

    public void liftWalkUpChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("liftWalkUp", label);

    }
    public void liftWalkDownChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("liftWalkDown", label);

    }
    public void liftWalkLeftChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("liftWalkLeft", label);

    }
    public void liftWalkRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("liftWalkRight", label);

    }
    //read
    public void readingChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("reading", label);

    }
    //sit
    public void sitLeftChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("sitLeft", label);

    }
    public void sitRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("sitRight", label);

    }
    //hurt
    public void hurtUpChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("hurtUp", label);

    }
    public void hurtDownChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("hurtDown", label);

    }
    public void hurtRightChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("hurtRight", label);

    }
    public void hurtLeftChangeSprite(string label)
    {

        spriteRenderer.sprite = spriteLibrary.GetSprite("hurtLeft", label);

    }

}
