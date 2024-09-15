using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameTime gameTime;
    public GameObject shop;

    void Update()
    {
        CheckEvents();
    }

    private void CheckEvents()
    {
        // ���磬�̵�������9����ţ�����6�㿪��
        if (gameTime.hours == 21 && gameTime.minutes == 0)
        {
            CloseShop();
        }
        else if (gameTime.hours == 6 && gameTime.minutes == 0)
        {
            OpenShop();
        }
    }
    public void addOrder()
    {



    }
    private void CloseShop()
    {
        shop.SetActive(false);
        Debug.Log("Shop is now closed.");
    }

    private void OpenShop()
    {
        shop.SetActive(true);
        Debug.Log("Shop is now open.");
    }
}
