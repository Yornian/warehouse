using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public GameObject warn;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("UIManager");
                _instance = obj.AddComponent<UIManager>();
            }
            return _instance;
        }
    }

    public GameObject BuildingBackpack;

    private void Start()
    {

    }
    
    public void showWarn(string txt)
    {
       // Debug.Log(warn == null);
       // warn.SetActive(true);
       //// warn.GetComponent<TypewriterEffect>().setText(txt);
       //// warn.GetComponent<TypewriterEffect>().StartTyping();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            openOrCloseBuildingBackpack();
            Debug.Log("s");
        }
    }

    public void openOrCloseBuildingBackpack()
    {
        if (BuildingBackpack.activeSelf)
        {
            BuildingBackpack.SetActive(false);
        }
        else
        {
            BuildingBackpack.SetActive(true);
        }
    }
}