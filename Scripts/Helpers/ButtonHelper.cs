using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHelper : CustomMonoBehaviorWrapper
{
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }

    public void OnSelect()
    {
        if(eventSystem == null) return;

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
