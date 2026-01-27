using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool gameIsPaused = false;
    public GameObject menuUI;
    // Update is called once per frame
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pressed");
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1.0f;  
        gameIsPaused=false;
    }
    void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void loadMenu()
    {
        Time.timeScale = 1.0f;  
        SceneManager.LoadScene("MenuMainScene");
    }
    public void quit()
    {
        Application.Quit();
    }
}