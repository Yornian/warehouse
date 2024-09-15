using UnityEngine;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    // 指定截图的按键
    public KeyCode screenshotKey = KeyCode.P;

    // 截图保存的文件夹
    public string screenshotFolder = "Screenshots";

    void Update()
    {
        // 检查是否按下了截图键
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

        // 创建截图的文件名，包含日期和时间以确保文件名唯一
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string screenshotFileName = $"screenshot_{timestamp}.png";
        string screenshotPath = Path.Combine(screenshotFolder, screenshotFileName);

        // 使用Unity的ScreenCapture类捕获屏幕并保存为文件
        ScreenCapture.CaptureScreenshot(screenshotPath);
        Debug.Log($"Screenshot saved to: {screenshotPath}");
    }
}
