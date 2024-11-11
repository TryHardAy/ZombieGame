using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Za�aduj scen� gry
        Debug.Log("Start Gry");
        SceneManager.LoadScene("Game"); // Upewnij si�, �e nazwa sceny jest poprawna
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Zako�cz symulacj� w edytorze Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // Wyjd� z gry w buildzie
    Application.Quit();
#endif
        Debug.Log("Game is exiting");
    }
}
