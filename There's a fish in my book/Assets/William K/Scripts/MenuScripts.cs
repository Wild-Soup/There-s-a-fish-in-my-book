using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuScripts
{

    public static void StartMainScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void QuitGame()
    {
        Application.Quit(0);
    }

}
