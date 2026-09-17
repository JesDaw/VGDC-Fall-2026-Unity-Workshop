using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseStateController : MonoBehaviour
{
    public static PauseStateController Instance { get; private set; }
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private InputActionMap uiMap;
    public InputAction PauseAction { get; private set; }
    public bool Paused { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            uiMap.Enable();
            PauseAction = uiMap.FindAction("Pause");
            PauseAction.performed += context => HandlePauseInput();
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
        Debug.Log("HandlePauseInput called");
        if (Paused)
        {
            OnUnpause();
        }
        else
        {
            OnPause();
        }
        Paused = !Paused;
    }

    private void OnPause()
    {
        pauseUI.SetActive(true);
    }

    private void OnUnpause()
    {
        pauseUI.SetActive(false);
    }
}
