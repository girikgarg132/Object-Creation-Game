using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] RandomObjectSpawner objectSpawner;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameObject XRRig;
    [Header("Rooms")]
    [SerializeField] GameObject mainRoom;
    [SerializeField] GameObject pauseRoom;
    [SerializeField] GameObject gameOverRoom;
    [Header("Audio Clips")]
    [SerializeField] AudioClip correctPieceMessage;
    [SerializeField] AudioClip correctObjectMessage;
    [SerializeField] AudioClip wrongPieceMessage;
    [SerializeField] AudioClip wrongObjectMessage;
    [SerializeField] AudioClip continuousRightAnswerMessage;

    private int noOfAttemps = 3;
    private int noOfWrongObjects = 0;
    private int successionCorrectAnswer = 0;
    private int noOfCorrectAnswer = 0;

    private void Start()
    {
        objectSpawner.SpawnObjects();
    }

    public void RightAnswer()
    {
        scoreboard.IncrementScore();
        noOfCorrectAnswer++;
        if (noOfCorrectAnswer >= objectSpawner.GetSpawnedObject().transform.childCount - 1)
        {
            AudioSource.PlayClipAtPoint(correctObjectMessage, Vector3.zero);
            StartCoroutine(NextRound());
        }
        else
        {
            if (successionCorrectAnswer < 2)
            {
                AudioSource.PlayClipAtPoint(correctPieceMessage, Vector3.zero);
            }
            else
            {
                AudioSource.PlayClipAtPoint(continuousRightAnswerMessage, Vector3.zero);
            }
        }
        successionCorrectAnswer++;
    }

    public void WrongAnswer()
    {
        noOfAttemps--;
        successionCorrectAnswer = 0;
        if (noOfWrongObjects >= 3)
        {
            GameOver();
            return;
        }
        if (noOfAttemps <= 0)
        {
            AudioSource.PlayClipAtPoint(wrongObjectMessage, Vector3.zero);
            noOfAttemps = 3;
            StartCoroutine(NextRound());
        }
        else
        {
            AudioSource.PlayClipAtPoint(wrongPieceMessage, Vector3.zero);
        }
    }

    private void GameOver()
    {
        pauseMenu.enabled = false;
        mainRoom.SetActive(false);
        pauseRoom.SetActive(false);
        XRRig.transform.Rotate(-30f, 0f, 0f);
        gameOverRoom.SetActive(true);
    }

    private IEnumerator NextRound()
    {
        yield return new WaitForSeconds(3f);
        noOfWrongObjects++;
        noOfAttemps = 3;
        noOfCorrectAnswer = 0;
        objectSpawner.DespanwObjects();
        objectSpawner.SpawnObjects();
    }
}
