using UnityEngine;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    // ָ����ͼ�İ���
    public KeyCode screenshotKey = KeyCode.P;

    // ��ͼ������ļ���
    public string screenshotFolder = "Screenshots";

    void Update()
    {
        // ����Ƿ����˽�ͼ��
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }

        // ������ͼ���ļ������������ں�ʱ����ȷ���ļ���Ψһ
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string screenshotFileName = $"screenshot_{timestamp}.png";
        string screenshotPath = Path.Combine(screenshotFolder, screenshotFileName);

        // ʹ��Unity��ScreenCapture�ಶ����Ļ������Ϊ�ļ�
        ScreenCapture.CaptureScreenshot(screenshotPath);
        Debug.Log($"Screenshot saved to: {screenshotPath}");
    }
}
