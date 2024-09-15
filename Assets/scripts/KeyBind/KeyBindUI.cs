using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingUI : MonoBehaviour
{
    public KeyBindingManager keyBindingManager;

    public void OnRebindButtonClicked(string actionName)
    {
        StartCoroutine(WaitForKeyPress(actionName));
    }

    private IEnumerator WaitForKeyPress(string actionName)
    {
        bool keyAssigned = false;
        while (!keyAssigned)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    keyBindingManager.SetKeyBinding(actionName, keyCode);
                    keyAssigned = true;
                    break;
                }
            }
            yield return null;
        }
    }
}
