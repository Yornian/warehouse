using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private bool canBuild = false;
    private Building building;
    public bool onFloor = false;
    public bool haveBuilding = false;
    public bool IsEmpty()
    {
        return building == null;
    }

    public void PlaceBuilding(Building building)
    {
        this.building = building;
    }

    void Start()
    {

    }
    public bool IfCanBuild()
    {

        if(onFloor&&!haveBuilding)
        {
            canBuild = true;

        }
        else
        {
            canBuild = false;

        }
        return canBuild;


    }
  

    public bool IsOverlappingWithFloor()
    {
        int overlapB = 0;
        // Get all colliders overlapping with this object's collider
        Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size*0.6f, 0f);
        Collider2D ownCollider = GetComponent<Collider2D>();
        bool isOverlapping = false;
        foreach (Collider2D collider in overlappingColliders)
        {
            
            if (collider.CompareTag("building"))
            {
                overlapB++;
                if(overlapB>=2)
                {
                    isOverlapping = false;
                    break;
                }
               
            }
            if (collider.CompareTag("floor"))
            {
                isOverlapping = true;
               
            }
        }

        SetSpriteColor(isOverlapping);
        return isOverlapping;
    }

    private void SetSpriteColor(bool isOverlappingWithFloor)
    {
        Transform gridBottomTransform =transform.Find("gridBottom");
        SpriteRenderer spriteRenderer = gridBottomTransform.GetComponent<SpriteRenderer>();
      
        if (spriteRenderer != null)
        {
            Color color;
            if (isOverlappingWithFloor)
            {
                color = new Color(0.40f, 0.99f, 0.27f, 112 / 255f);  
            }
            else
            {
                color = new Color(0.99f, 0.38f, 0.27f, 112 / 255f);
           
            }
            spriteRenderer.color = color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }

  

    private void Update()
    {
        if(!IsOverlappingWithFloor())//之后还会有别的检查条件
        {
            onFloor = false;
        }
        if (IsOverlappingWithFloor())
        {
            onFloor = true;
        }


    }
}
