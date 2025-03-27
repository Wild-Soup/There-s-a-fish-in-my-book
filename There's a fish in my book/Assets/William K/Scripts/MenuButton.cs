using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        MenuScripts.StartMainScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit(1);
    }

}
