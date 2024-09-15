using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bed : MonoBehaviour,IInteractable
{
    public GameObject sleepPic;
    public GameObject HeadSleepPic;
    public GameObject newdayText;
    public GameObject Player;
    //Ω·À„ui
    public GameObject settlementUI;
    public Bar powerbar;
    private void Awake()
    {
      
    }
    public void PerformInteraction(GameObject Player)
    {
        sleep();
    }

    public void PerformInteractionClick(GameObject Player)
    {
       
    }

    public void PerformInteractionF(GameObject Player)
    {
       
    }

    public void PerformInteractionLongPress(GameObject Player)
    {
       
    }
    private IEnumerator DelayedAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    public void sleep()
    {
        GameManager.Instance.setPlayerState(PlayerState.doNothing);
        Player = GameObject.Find("Player");
        GameObject.Find("Player").SetActive(false);
       
        SpriteRenderer spriteRenderer = HeadSleepPic.GetComponent<SpriteRenderer>();


        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; 
        }
        powerbar.setAndUpdateValue(100);
        StartCoroutine(DelayedAction(() =>
        {
            sleepPic.SetActive(true);
            sleepPic.GetComponent<ImageFadeIn>().beginFade();
        }, 0.5f));
        StartCoroutine(DelayedAction(() =>
        {
            settlementUI.SetActive(true);
             settlementUI.GetComponent<TypewriterEffect>().setOrderText(OrderManager.Instance.todayRightOrder);
    
            settlementUI.GetComponent<TypewriterEffect>().StartTyping();
            OrderManager.Instance.todayRightOrder = 0;
        }, 1.1f));
      
      
       


       
    }
    public void settlementViewClosed()
    {
        StartCoroutine(DelayedAction(() =>
        {
            newdayText.SetActive(true);
            newdayText.GetComponent<TypewriterEffect>().StartTyping();
        }, 0.2f));
        StartCoroutine(DelayedAction(() =>
        {
            wakeUp();
        }, 2.8f));
    }
    public void wakeUp()
    {
        Player.SetActive(true);
        GameManager.Instance.setPlayerState(PlayerState.walk);

        SpriteRenderer spriteRenderer = HeadSleepPic.GetComponent<SpriteRenderer>();


        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Ω˚”√ SpriteRenderer£¨
        }
        sleepPic.SetActive(false);
        newdayText.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
       
        SpriteRenderer spriteRenderer = HeadSleepPic.GetComponent<SpriteRenderer>();

        
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Ω˚”√ SpriteRenderer£¨
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
