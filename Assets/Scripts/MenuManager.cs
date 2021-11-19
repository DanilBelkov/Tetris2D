using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool IsPlaying;

    [SerializeField]
    private GameObject _canvas;
    private bool _isGameOver;

    private void Start()
    {
        Pause();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        _canvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        if (IsPlaying)
        {
            _canvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Play()
    {
        if (!IsPlaying)
        {
            if (_isGameOver)
            {
                Restart();
            }
            else
            {
                IsPlaying = true;
                FindObjectOfType<SpawnBricks>().CreateBrick();
                Continue();
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _isGameOver = false;
        IsPlaying = false;
    }
    public void Exite()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        IsPlaying = false;
        _isGameOver = true;
        Pause();
    }
}
