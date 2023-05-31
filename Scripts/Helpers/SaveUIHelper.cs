using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class SaveUIHelper : MonoBehaviour
{    
    private GameObject OnEmptyObject;
    private GameObject OnFilledObject;
    private bool isEmpty;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        isEmpty = true;
        foreach (Transform item in transform)
        {
            if(item.gameObject.name == "On Empty")
            {
                OnEmptyObject = item.gameObject;
            }

            if(item.gameObject.name == "On Filled")
            {
                OnFilledObject = item.gameObject;
            }
        }
    }

    public void Refresh()
    {
        if(isEmpty)
        {
            OnEmptyObject.SetActive(true);
            OnFilledObject.SetActive(false);
        }
        else
        {
            OnEmptyObject.SetActive(false);
            OnFilledObject.SetActive(true);
        }
    }

    public void SetData(bool isEmpty)
    {
        this.isEmpty = isEmpty;
        Refresh();
    }

}
