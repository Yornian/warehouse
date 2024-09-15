using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons 
{
    public List<Button> buttonsList;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public Buttons()
    {


    }
    public Buttons(List<Button> buttonsList)
    {

        this.buttonsList = buttonsList;
    }
    public void Initialize(List<Button> buttonsList)
    {
        this.buttonsList = buttonsList;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
