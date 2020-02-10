using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _player;
    public GameObject _aMazeBot_Blue;
    public GameObject _aMazeBot_Red;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;

    RaycastPlacement _raycastPlacement;
    PlayModeManager _playModeManager;

    public float restartDelay = 2f;

    private bool gameHasEnded = false;
    private bool gameIsWon = false;

    public enum GameState { placement, play}
    public GameState _currentState = GameState.placement;

    private void Start()
    {
        _currentState = GameState.placement;
    }

    public void ChangeState()
    {
        //_currentState = GameState.play;

        //_currentState = newState;
        switch (_currentState)
        {
            case GameState.placement:
                {
                    if (true)
                    {
                        _currentState = GameState.play;
                        StateChangeToPLay();
                    }
                    break;
                }
            case GameState.play:
                {
                    if (true)
                    {
                        _raycastPlacement.StopAllCoroutines();
                    }
                    break;
                }
        }

    }

    public void GameEnded()
    {
        Debug.Log("GameOver");
        gameOverUI.SetActive(true);

        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    public void GameWon()
    {
        Debug.Log("GAME WON");
        completeLevelUI.SetActive(true);


        if (gameIsWon == false)
        {
            gameIsWon = true;
            Debug.Log("YOU WIN");
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Här ska jag lägga in menuscenen!<<<<<<<<<<<<<<<<<<<<<<<<
    }

    private void StateChangeToPLay()
    {
        // StopPlacement = true; 
        _raycastPlacement.StopAllCoroutines();
        _currentState = GameState.play;
        _playModeManager.PlayMode(); // varför funkar detta utan att defeniera i start()?
    }
}
