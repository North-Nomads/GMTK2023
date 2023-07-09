using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
