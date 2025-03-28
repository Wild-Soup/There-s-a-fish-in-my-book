using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        MenuScripts.StartMainScene(sceneName);
    }

    public void Restart(int day)
    {
        GameManager.instance.ResetScene(day);
    }

    public void QuitGame()
    {
        Application.Quit(1);
    }

}
