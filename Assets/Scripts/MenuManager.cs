using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Za³aduj scenê gry
        Debug.Log("Start Gry");
        SceneManager.LoadScene("Game"); // Upewnij siê, ¿e nazwa sceny jest poprawna
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Zakoñcz symulacjê w edytorze Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // WyjdŸ z gry w buildzie
    Application.Quit();
#endif
        Debug.Log("Game is exiting");
    }
}
