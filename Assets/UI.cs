using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public string nextScene;
    public string prevScene;

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextScene))
            SceneManager.LoadScene(nextScene);
    }

    public void LoadPrevScene()
    {
        if (!string.IsNullOrEmpty(prevScene))
            SceneManager.LoadScene(prevScene);
    }
}
