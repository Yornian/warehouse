using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerX", 0f);
            float y = PlayerPrefs.GetFloat("PlayerY", 0f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);
            player.transform.position = new Vector3(x, y, z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
