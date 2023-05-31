using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMonoBehaviorWrapper : MonoBehaviour
{
    [SerializeField]
    private bool ShowLogging = false;
    
    protected void Log(string message)
    {
        if(ShowLogging) Debug.Log(this.GetType().Name + " : " + message);
    }

    protected void Warning(string message)
    {
        if(ShowLogging) Debug.LogWarning(this.GetType().Name + " : " + message);
    }

    protected void Error(string message)
    {
        if(ShowLogging) Debug.LogError(this.GetType().Name + " : " + message);
    }
}
