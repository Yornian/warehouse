using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartMenuState : IGameState
{
    public void OnEnter()
    {
        GameManager.Instance.UpdateMoneyUI();//���Ҫȡ��
        
        // ��ʼ����ʼ�˵�
        Debug.Log("���뿪ʼ�˵�");
        // ��ʾ��ʼ�˵� UI
    }

    public void OnExit()
    {
        // ����ʼ�˵�
        Debug.Log("�뿪��ʼ�˵�");
    }
}

public class PlayingState : IGameState
{
    public void OnEnter()
    {
        GameManager.Instance.UpdateMoneyUI();
        // ��ʼ��Ϸ�߼�
        Debug.Log("��Ϸ��ʼ");
    }

    public void OnExit()
    {
        // ��Ϸ��ͣ�����ʱ������
        Debug.Log("��Ϸ����");
    }
}

public class PausedState : IGameState
{
    public void OnEnter()
    {
        // ��ͣ��Ϸ
        Debug.Log("��Ϸ��ͣ");
    }

    public void OnExit()
    {
        // �ָ���Ϸ
        Debug.Log("������Ϸ");
    }
}

public class GameOverState : IGameState
{
    public void OnEnter()
    {
        // ��Ϸ��������
        Debug.Log("��Ϸ����");
    }

    public void OnExit()
    {
        // ���ܻ�������Ϸ�򷵻����˵�
        Debug.Log("�뿪��Ϸ����״̬");
    }
}
