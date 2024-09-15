using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSlot : MonoBehaviour
{
    public Sprite chosenButtonPic;
    public Sprite unChosenButtonPic;
    public GameObject chosenImage;
    public GameObject unChosenImage;
   
    public Button Button;
    public bool ifChosen;
    public SwitchSlot otherSlot;
    public string switchToWhat;
 
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
        if(ifChosen)
        {
            choose();
            ReduceButtonYPosition();

        }
        else
        { unchoose(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void choose()
    {
        Image buttonImage = Button.GetComponent<Image>();
        buttonImage.sprite = chosenButtonPic;
      
        chosenImage.SetActive(true);
        unChosenImage.SetActive(false);
        otherSlot.unchoose();
        addButtonYPosition();
        if(switchToWhat=="item")
        {
            switchToItem();

        }
        else if(switchToWhat == "building")
        {
            switchToBuilding();

        }

        ifChosen = false;
        ifChosen = true;
    }
    public void unchoose()
    {
        Image buttonImage = Button.GetComponent<Image>();
        buttonImage.sprite = unChosenButtonPic;
        chosenImage.SetActive(false);
        unChosenImage.SetActive(true);

        ReduceButtonYPosition();
        ifChosen = false;
    }
    public void ReduceButtonYPosition()
    {
        Vector2 currentPosition = Button.transform.position;

        currentPosition.y -= 10;

        Button.transform.position = currentPosition;
    }
    public void addButtonYPosition()
    {
        Vector2 currentPosition = Button.transform.position;
        currentPosition.y += 10;
        Button.transform.position = currentPosition;
    }

    public void  switchToBuilding()
    {
        BuildingManager.Instance.updateMarketBuidingUI();
    }
    public void switchToItem()
    {
        OrderManager.Instance.updateMarketItemUI();
    }
    public void OnButtonClick()
    {
        
        if(!ifChosen)
        {
            choose();
        }
        
       
    }
}
