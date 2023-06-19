using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class MainMenuManager : CustomMonoBehaviorWrapper
{

    public MainMenuStates currentState;
    [SerializeField] private StringEventChannelSO mainMenuRequestChannelSO;
    [SerializeField] private TextMeshProUGUI buildTMP;
    [SerializeField] private TextMeshProUGUI legalTMP;

    public List<MenuScreen> menuScreens;
    private MenuScreen findScreenByState(MainMenuStates state)
    {
        return menuScreens.Find(item => item.currentState == state);
    }
    private MenuScreen findScreenByName(string name)
    {
        return menuScreens.Find(screen => screen.name == name);
    }

    public EventSystem eventSystem;
    
    private MenuScreen currentScreen;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        mainMenuRequestChannelSO.OnEventRaised += (state) => OnSelect(state);
    }

    private void OnDisable()
    {
        mainMenuRequestChannelSO.OnEventRaised -= (state) => OnSelect(state);
    }

    private void Initialize()
    {
        currentScreen = findScreenByState(currentState);
        currentScreen.rootObject.SetActive(true);

        buildTMP.text = Application.version;
        legalTMP.text = "Made by " +  Application.companyName;

        Log("Initialized");
    }

    public void OnSelect (string newState)
    {
        OnSelect(findScreenByName(newState).currentState);
    }

    public void OnSelect(MainMenuStates newState)
    {
        Log("Going to new state: " + newState);
        OnChangeToState(newState, findScreenByState(newState).selectedButtonOnStateEnter);
    }

    public void OnBack()
    {
        Log("Went back to state: " + currentScreen.exitToState);
        OnChangeToState(currentScreen.exitToState, currentScreen.selectedButtonOnStateExit);
    }

    private void OnChangeToState(MainMenuStates newState, Button selectedButton)
    {
        MenuScreen newScreen = findScreenByState(newState);
        currentState = newState;

        currentScreen.rootObject.SetActive(false);
        currentScreen.OnExit.Invoke();
        currentScreen = newScreen;
        currentScreen.rootObject.SetActive(true);
        currentScreen.OnEnter.Invoke();

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(selectedButton.gameObject);
    }


}

[System.Serializable]
public struct MenuScreen
{
    public GameObject rootObject;
    public string name;
    public MainMenuStates currentState;
    public MainMenuStates exitToState;
    public Button selectedButtonOnStateEnter;
    public Button selectedButtonOnStateExit;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
}

public enum MainMenuStates
    {
        None = -1, MainMenu = 0, NewGame = 1, LoadGame = 2, Options = 3, Credits = 4, Quit = 5
    }
