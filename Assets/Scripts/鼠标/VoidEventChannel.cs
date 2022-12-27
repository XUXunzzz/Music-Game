using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/EventChannel/VoidEventChannel",fileName ="VoidEventChannel_")]
public  class VoidEventChannel : ScriptableObject
{
    event System.Action Delegate;//事件频道中存在的委托

    public void Broadcast()//委托的广播
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
