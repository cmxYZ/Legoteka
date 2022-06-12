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
}
