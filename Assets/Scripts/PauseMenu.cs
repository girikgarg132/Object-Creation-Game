using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject XRRig;
    [SerializeField] GameObject mainRoom;
    [SerializeField] GameObject pauseRoom;

    private DefaultConfiguration defaultConfiguration;
    private InputAction pauseAction;
    private bool isPaused = false;

    private void Awake()
    {
        defaultConfiguration = new DefaultConfiguration();
    }

    private void Start()
    {
        pauseAction = defaultConfiguration.Default.Pause;
        pauseAction.performed += TakeInput;
        pauseAction.Enable();
    }

    private void TakeInput(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPaused = true;
        XRRig.transform.Rotate(-30f, 0f, 0f) ;
        mainRoom.SetActive(false);
        pauseRoom.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        XRRig.transform.Rotate(30f, 0f, 0f);
        mainRoom.SetActive(true);
        pauseRoom.SetActive(false);
    }
}
