using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/EventChannel/VoidEventChannel",fileName ="VoidEventChannel_")]
public  class VoidEventChannel : ScriptableObject
{
    event System.Action Delegate;//�¼�Ƶ���д��ڵ�ί��

    public void Broadcast()//ί�еĹ㲥
    {
        Delegate?.Invoke();
    }

    public void AddListener(System.Action action)
    {
        Delegate += action;
    }
    public void RemoveListener(System.Action action)
    {
        Delegate -= action;
    }
}
