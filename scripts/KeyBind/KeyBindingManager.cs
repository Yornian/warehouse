using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBinding
{
    public string actionName;
    public KeyCode key;
}

public class KeyBindingManager : MonoBehaviour
{
    public List<KeyBinding> keyBindings;

    void Start()
    {
        // 初始化默认按键绑定
        // keyBindings.Add(new KeyBinding { actionName = "Jump", key = KeyCode.Space });
    }

    public void SetKeyBinding(string actionName, KeyCode newKey)
    {
        KeyBinding binding = keyBindings.Find(b => b.actionName == actionName);
        if (binding != null)
        {
            binding.key = newKey;
        }
    }

    public KeyCode GetKeyForAction(string actionName)
    {
        KeyBinding binding = keyBindings.Find(b => b.actionName == actionName);
        return binding != null ? binding.key : KeyCode.None;
    }
}
