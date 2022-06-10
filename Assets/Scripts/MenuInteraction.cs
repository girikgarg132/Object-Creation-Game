using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;

    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.parent.CompareTag("UI")) { return; }
        string uIIdentity = collision.gameObject.GetComponent<MenuColliderIdentity>().myIdentity;
        switch (uIIdentity)
        {
            case "Play": Play(); break;
            case "Exit": Exit(); break;
            case "Resume":
                pauseMenu.Resume();
                transform.position = initialPos;
                break;
            case "Restart": Restart(); break;
        }
    }

    private void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
