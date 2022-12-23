using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiControl : MonoBehaviour
{
    private void Start()
    {
        //SceneManager.LoadScene("GiraffeNoHelper");
        //SceneManager.LoadScene("GiraffeWithHelper");

    }
    public void SceneSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void CrocodileWithHelper()
    {
        SceneManager.LoadScene("CrocodileWithHelper");
    }

    public void SceneGiraffeNoHelper()
    {
        SceneManager.LoadScene("GiraffeNoHelper");
    }

    public void SceneGiraffeWithHelper()
    {
        SceneManager.LoadScene("GiraffeWithHelper");
    }

    public void SceneChooseGiraffe()
    {
        SceneManager.LoadScene("ChooseGiraffe");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void InstructionGiraffe()
    {
        Application.OpenURL("https://drive.google.com/file/d/1SGDF0PAxUhWIYtsG83w55ni_mP4lbCty/view?usp=sharing");
    }

    public void InstructionCrocodile()
    {
        Application.OpenURL("https://drive.google.com/file/d/1Z8lqTqnY2yJRu4-iSARx-kXwWcRuJ4yg/view?usp=sharing");
    }
}
