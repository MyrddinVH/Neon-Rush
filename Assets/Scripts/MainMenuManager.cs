using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadGameScene(){
        SceneManager.LoadScene("Main Game");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
