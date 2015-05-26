using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>陷阱接收者事件介面</summary>
public interface ISnareEvent : IEventSystemHandler
{
    void OnSnareTrigger();
}