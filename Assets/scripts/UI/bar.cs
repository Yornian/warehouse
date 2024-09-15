using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image BarFill;
    public int currentValue;
    public int maxValue;
    public TextMeshProUGUI ValueText;
    private void Start()
    {

        initHealthBar();
    }

    public void UpdateHealth(int newHealth)
    {
        currentValue = newHealth;
        UpdateHealthBar();
    }

    private void initHealthBar()
    {
        currentValue = GameManager.Instance.powerValue;
        float fillAmount = (float)currentValue / maxValue;
        RectTransform fillRectTransform = BarFill.GetComponent<RectTransform>();
        fillRectTransform.localScale = new Vector3(fillAmount, 1, 1);
        ValueText.text = currentValue.ToString()+"/"+ maxValue.ToString();
    }
    private void UpdateHealthBar()
    {
       
        float fillAmount = (float)currentValue / maxValue;
        RectTransform fillRectTransform = BarFill.GetComponent<RectTransform>();
        fillRectTransform.localScale = new Vector3(fillAmount, 1, 1);
        ValueText.text = currentValue.ToString() + "/" + maxValue.ToString();
    }
    public void setAndUpdateValue(int value)
    {
        GameManager.Instance.powerValue= value;
        currentValue = value;
        UpdateHealthBar();

    }
    public void changeAndUpdateValue(int change)
    {
        currentValue += change;
        if (currentValue < 0)
        {
            currentValue = 0;
        }
        else if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        GameManager.Instance.powerValue = currentValue;
        UpdateHealthBar();

    }
}