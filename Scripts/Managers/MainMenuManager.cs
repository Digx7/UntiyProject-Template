using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : Singleton<MainMenuManager>
{

    public MainMenuStates currentState;

    public List<MenuScreen> menuScreens;
    private MenuScreen findScreenForState(MainMenuStates state)
    {
        return menuScreens.Find(item => item.currentState == state);
    }

    public EventSystem eventSystem;

    public void OnConinuteClick()
    {

    }

    public void OnNewGameClick()
    {

    }

    public void OnLoadGameClick()
    {

    }

    public void OnOptionsClick()
    {

    }

    public void OnCreditsClick()
    {

    }

    public void OnQuitClick()
    {

    }
}

[System.Serializable]
public struct MenuScreen
{
    public MainMenuStates currentState;
    public MainMenuStates exitToState;
    public Button selectedButtonOnStateEnter;
    public Button selectedButtonOnStateExit;
}

public enum MainMenuStates
    {
        MainMenu, NewGame, LoadGame, Options, Credits, Quit
    }
