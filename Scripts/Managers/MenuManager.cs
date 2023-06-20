using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MenuManager : CustomMonoBehaviorWrapper
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private UIRequest turnOnRequest;
    [SerializeField] private UIRequest turnOffRequest;
    [SerializeField] private UIRequestChannelSO uIRequestChannelSO;
    [SerializeField] private VoidEventChannelSO onBackRequestChannelSO;
    [SerializeField] private GameObjectEventChannelSO setSelectedObjectChannel;
    [SerializeField] private List<MenuScreen> menuScreens;
    private MenuScreen findScreenByEnterRequest(UIRequest request)
    {
        return menuScreens.Find(screen => screen.enterRequest == request);
    }
    private bool checkIfScreenExistsByEnterRequest(UIRequest request)
    {
        if (menuScreens.Exists(screen => screen.enterRequest == request))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private MenuScreen findStartingScreen()
    {
        return menuScreens.Find(screen => screen.isStartingScreen == true);
    }
    
    private MenuScreen currentScreen;

    private void Start()
    {
        currentScreen = menuScreens[0];
    }

    private void OnEnable()
    {
        uIRequestChannelSO.OnEventRaised += (state) => OnSelect(state);
        onBackRequestChannelSO.OnEventRaised += () => OnBack();
    }

    private void OnDisable()
    {
        uIRequestChannelSO.OnEventRaised -= (state) => OnSelect(state);
        onBackRequestChannelSO.OnEventRaised -= () => OnBack();
    }

    private void OnSelect (UIRequest newRequest)
    {
        if(newRequest == turnOnRequest)
        {
            OnChangeScreen(findStartingScreen());
            isOn = true;
            Log("Recieved turn on request: " + newRequest);
        }
        else if(newRequest == turnOffRequest)
        {
            currentScreen.rootObject.SetActive(false);
            isOn = false; 
            Log("Recieved turn off request: " + newRequest);
        }
        else
        {
            if(checkIfScreenExistsByEnterRequest(newRequest))
            {
                Log("Recieved request to change screen: " + newRequest);
                OnChangeScreen(findScreenByEnterRequest(newRequest));
            }
            else
            {
                Log("Ingnored request: " + newRequest);
            }
        }
    }

    public void OnBack()
    {
        if(isOn)
        {
            currentScreen.OnBack.Invoke();
        }
    }

    private void OnChangeScreen(MenuScreen newScreen)
    {
        Log("Changing screen: " + currentScreen.name + " -> " + newScreen.name);
        currentScreen.rootObject.SetActive(false);
        currentScreen.OnExit.Invoke();

        currentScreen = newScreen;

        currentScreen.rootObject.SetActive(true);
        currentScreen.OnEnter.Invoke();

        setSelectedObjectChannel.RaiseEvent(null);
        setSelectedObjectChannel.RaiseEvent(currentScreen.selectedObjectOnStateEnter);
    }


}

[System.Serializable]
public struct MenuScreen
{
    public GameObject rootObject;
    public string name;
    public bool isStartingScreen;
    public UIRequest enterRequest;
    public GameObject selectedObjectOnStateEnter;
    public GameObject selectedObjectOnStateExit;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnBack;
}
