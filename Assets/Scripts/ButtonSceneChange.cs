using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
    public String sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
