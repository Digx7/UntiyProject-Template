using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIRequestListener : CustomMonoBehaviorWrapper
{
    [SerializeField] private UIRequest requestToListenFor;
    [SerializeField] private UIRequestChannelSO uIRequestChannelSO;
    public UnityEvent OnHearRequestToListenFor;
    public UnityEvent OnHearAnyOtherRequest;

    private void OnEnable()
    {
        uIRequestChannelSO.OnEventRaised += (uIRequest) => ProcessRequest(uIRequest);
    }

    private void OnDisable()
    {
        uIRequestChannelSO.OnEventRaised -= (uIRequest) => ProcessRequest(uIRequest);
    }

    private void ProcessRequest(UIRequest uIRequest)
    {
        if(uIRequest == requestToListenFor)
        {
            OnHearRequestToListenFor.Invoke();
        }
        else
        {
            OnHearAnyOtherRequest.Invoke();
        }
    }
}
