using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas_FE : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
