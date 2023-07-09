using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject child;
    private bool _isPaused;

    private void Start()
    {
        _isPaused = false;
        
    }

    private void SetPause(bool status)
    {
        child.SetActive(status); // if paused - enable
        _isPaused = status;
        Time.timeScale = _isPaused ? 0 : 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!_isPaused);
            print($"Game is now {_isPaused}");
        }
    }

    public void ResumeGame()
    {
        SetPause(false);
    }
}
