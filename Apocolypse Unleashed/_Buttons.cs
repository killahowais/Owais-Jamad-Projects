using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Buttons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartButton() 
    {
        SceneManager.LoadScene("_DokterScene");
    }

    public void QuitButton()
    {
       Application.Quit();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("_DokterretryScene");
    }

    public void Playbutton()
    {
        SceneManager.LoadScene("_GameScene");    
    }

}
