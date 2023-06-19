using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/UI Request")]
public class UIRequest : ScriptableObject
{
    public string request;

    public UnityEvent OnRequest;

    // Add addtional data to store in UI Requests here
    // ...


}
