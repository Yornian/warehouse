using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartMenuState : IGameState
{
    public void OnEnter()
    {
        GameManager.Instance.UpdateMoneyUI();//这个要取消
        
        // 初始化开始菜单
        Debug.Log("进入开始菜单");
        // 显示开始菜单 UI
    }

    public void OnExit()
    {
        // 清理开始菜单
        Debug.Log("离开开始菜单");
    }
}

public class PlayingState : IGameState
{
    public void OnEnter()
    {
        GameManager.Instance.UpdateMoneyUI();
        // 开始游戏逻辑
        Debug.Log("游戏开始");
    }

    public void OnExit()
    {
        // 游戏暂停或结束时的清理
        Debug.Log("游戏结束");
    }
}

public class PausedState : IGameState
{
    public void OnEnter()
    {
        // 暂停游戏
        Debug.Log("游戏暂停");
    }

    public void OnExit()
    {
        // 恢复游戏
        Debug.Log("继续游戏");
    }
}

public class GameOverState : IGameState
{
    public void OnEnter()
    {
        // 游戏结束处理
        Debug.Log("游戏结束");
    }

    public void OnExit()
    {
        // 可能会清理游戏或返回主菜单
        Debug.Log("离开游戏结束状态");
    }
}
