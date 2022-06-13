using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiControl : MonoBehaviour
{
    public void SceneSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SceneGiraffe()
    {
        SceneManager.LoadScene("Giraffe");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void InstructionGiraffe()
    {
        Application.OpenURL("https://drive.google.com/file/d/1SGDF0PAxUhWIYtsG83w55ni_mP4lbCty/view?usp=sharing");
    }
}
