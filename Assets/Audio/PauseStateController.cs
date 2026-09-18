using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseStateController : MonoBehaviour
{
    public static PauseStateController Instance { get; private set; }
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private InputActionMap uiMap;
    private InputAction _pauseAction;
    public bool Paused { get; private set; }
    public Action OnPause, OnUnpause;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            uiMap.Enable();
            _pauseAction = uiMap.FindAction("Pause");
            _pauseAction.performed += context => HandlePauseInput();
            OnPause += () => pauseUI.SetActive(true);
            OnUnpause += () => pauseUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Multiple PauseStateHandlers detected");
        }
    }

    private void Start()
    {

    }

    private void HandlePauseInput()
    {
        // Debug.Log("HandlePauseInput called");
        if (Paused)
        {
            HandleUnpause();
        }
        else
        {
            HandlePause();
        }
        Paused = !Paused;
    }

    private void HandlePause()
    {
        OnPause?.Invoke();
        Time.timeScale = 0;
    }

    private void HandleUnpause()
    {
        OnUnpause?.Invoke();
        Time.timeScale = 1;
    }
}
