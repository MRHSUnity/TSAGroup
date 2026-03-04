using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Howtoplaybuttton : MonoBehaviour
{
    public String sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited.");
        Application.Quit();
    }
}
