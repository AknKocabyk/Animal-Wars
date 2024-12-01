using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        print("Çıkış Yapıldı...");
        Application.Quit();
    }
}
