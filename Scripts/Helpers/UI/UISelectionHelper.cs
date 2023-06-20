using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class UISelectionHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private GameObject objectToSetAsSelected;
    [SerializeField] private GameObjectEventChannelSO selectedObjectRequestChannel;

    public void OnSelect()
    {
        selectedObjectRequestChannel.RaiseEvent(null);
        selectedObjectRequestChannel.RaiseEvent(objectToSetAsSelected);
    }
}
