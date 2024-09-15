using UnityEngine;
using UnityEngine.UI;

public class ImageFadeIn : MonoBehaviour
{
    public Image targetImage;
    private float alpha = 0f;
    public float fadeSpeed = 0.5f;
    private bool isFading = false;
    private void Start()
    {
       
    }

    void Update()
    {
        if (alpha < 1f && isFading)
        {
            alpha += fadeSpeed * Time.deltaTime;
            Color currentColor = targetImage.color;
            currentColor.a = alpha;
            targetImage.color = currentColor;
        }
    }

    public void beginFade()
    {
        Color initialColor = targetImage.color;
        initialColor.a = 0f;
        targetImage.color = initialColor;
        isFading = true;
    }

   
}