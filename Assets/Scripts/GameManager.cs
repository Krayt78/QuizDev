using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<QuizScriptableObject> Quizzes;
    public int CurrentQuizIndex;
    
    private CanvasManager _canvasManager;

    enum GameState
    {
        SplashScreen,
        QuizSelection,
        Quiz
    }
    
    private GameState _gameState;

    // Start is called before the first frame update
    private void Start()
    {
        _canvasManager = FindObjectOfType<CanvasManager>();
        _canvasManager.Initialize(Quizzes);
        
        _canvasManager.OnSplashScreenButtonClicked += () => UpdateGameState(GameState.QuizSelection);
        _canvasManager.OnHintButtonClicked += OnHintButtonClicked;
        _canvasManager.OnAnswerButtonClicked += OnAnswerSelected;
        
        _gameState = GameState.SplashScreen;
        UpdateGameState(GameState.SplashScreen, true);
        
        
    }
    
    private void UpdateGameState(GameState gameState, bool isInitialSetup = false)
    {
        if (isInitialSetup)
        {
            _gameState = gameState;
            return;
        }
        
        switch (gameState)
        {
            case GameState.SplashScreen:
                _canvasManager.SplashScreenCanvas.gameObject.SetActive(true);
                _canvasManager.QuizCanvas.gameObject.SetActive(false);
                _canvasManager.QuizSelectionCanvas.gameObject.SetActive(false);
                break;
            case GameState.QuizSelection:
                _canvasManager.SplashScreenCanvas.gameObject.SetActive(false);
                _canvasManager.QuizCanvas.gameObject.SetActive(false);
                _canvasManager.QuizSelectionCanvas.gameObject.SetActive(true);
                break;
            case GameState.Quiz:
                _canvasManager.SplashScreenCanvas.gameObject.SetActive(false);
                _canvasManager.QuizCanvas.gameObject.SetActive(true);
                _canvasManager.QuizSelectionCanvas.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    private void OnHintButtonClicked()
    {
        Debug.Log("GameManager: Hint Button Clicked");
    }

    private void OnAnswerSelected(int answerIndex)
    {
        Debug.Log("GameManager: Answer Selected: " + answerIndex);
    }
}