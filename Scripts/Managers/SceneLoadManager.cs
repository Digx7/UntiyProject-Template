using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public SceneRequestChannel loadRequestChannel;
    public SceneRequestChannel unloadRequestChannel;
    public VoidEventChannelSO quitGameRequestChannel;

    public void OnEnable()
    {
        loadRequestChannel.OnEventRaised += (sceneSO => LoadScene(sceneSO));
        unloadRequestChannel.OnEventRaised += (sceneSo => UnloadScene(sceneSo));
        quitGameRequestChannel.OnEventRaised += QuitGame;
    }

    public void OnDisable()
    {
        loadRequestChannel.OnEventRaised -= (sceneSO => LoadScene(sceneSO));
        unloadRequestChannel.OnEventRaised -= (sceneSo => UnloadScene(sceneSo));
        quitGameRequestChannel.OnEventRaised -= QuitGame;
    }

    private void LoadScene(SceneSO sceneSO)
    {
        if(sceneSO.loadType == SceneLoadType.normal)
        {
            Log("Loading the scene normally from sceneSO " + sceneSO.name);
            SceneManager.LoadScene(sceneSO.sceneReference, sceneSO.loadMode);
        }
        else
        {
            Log("Loading the scene asynchronously from sceneSO " + sceneSO.name);
            SceneManager.LoadSceneAsync(sceneSO.sceneReference, sceneSO.loadMode);
        }
        sceneSO.OnSceneStart.Invoke();
    }

    private void UnloadScene(SceneSO sceneSO)
    {
        Log("Unloading the scene from sceneSO " + sceneSO.name);
        SceneManager.UnloadSceneAsync(sceneSO.sceneReference);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #else
         Application.Quit();
         #endif
    }
}
