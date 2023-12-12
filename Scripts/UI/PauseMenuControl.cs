using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuControl : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction menu;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        menu = playerControls.Menu.Escape;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if(isPaused )
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    private void ActivateMenu()
    {
        Time.timeScale = 0f;
        UIManager.Instance.OpenUI<UIPause>(0);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DeactivateMenu()
    {
        Time.timeScale = 1.0f;

        var animUI = pauseUI.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(pauseUI);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isPaused = false;
    }
}
