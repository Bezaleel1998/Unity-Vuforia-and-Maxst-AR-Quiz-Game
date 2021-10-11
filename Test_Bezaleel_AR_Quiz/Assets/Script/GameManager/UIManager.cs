using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    
    public Animator aboutMeAnim;
    private bool isActive = false;

    public void ButtonActionAboutMe()
    {
        
        isActive = !isActive;

        if (isActive == true)
        {
            aboutMeAnim.SetBool("AboutMe", true);
        }
        else
        {
            aboutMeAnim.SetBool("AboutMe", false);
        }

    }

    public void SceneChange(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    
    }

    public void ExitGame()
    {

        Application.Quit();

    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
        Debug.Log("<color=blue>opening url : " + url + "</color>");
    }

    /*public void PauseSystem()
    {

        pmActive = !pmActive;

        if (pmActive == true)
        {
            Time.timeScale = 0;
            pauseMenuUI.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenuUI.gameObject.SetActive(false);
        }

    }*/

}
