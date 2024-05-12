using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<QuizVariationsScriptableObject> Quizzes;
    
    private List<QuizVariationsScriptableObject> FirstLevelQuizzes;
    private List<QuizVariationsScriptableObject> SecondLevelQuizzes;
    private List<QuizVariationsScriptableObject> ThirdLevelQuizzes;
    
    public int CurrentQuizIndex;
    
    private CanvasManager _canvasManager;
    private Quiz.Quiz _quiz;
    private int _quizzesCount;

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
        _quizzesCount = Quizzes.Count;
        FirstLevelQuizzes = Quizzes.GetRange(0, _quizzesCount / 3);
        SecondLevelQuizzes = Quizzes.GetRange(_quizzesCount / 3, _quizzesCount / 3);
        ThirdLevelQuizzes = Quizzes.GetRange(_quizzesCount / 3 * 2,
            _quizzesCount - FirstLevelQuizzes.Count - SecondLevelQuizzes.Count);
        
        _quiz = FindObjectOfType<Quiz.Quiz>();
        _quiz.OnCorrectAnswerClicked += OnCorrectAnswerClicked;
        
        _canvasManager = FindObjectOfType<CanvasManager>();
        
        _canvasManager.Initialize(FirstLevelQuizzes, SecondLevelQuizzes, ThirdLevelQuizzes);
        
        _canvasManager.OnSplashScreenButtonClicked += () => UpdateGameState(GameState.QuizSelection);
        _canvasManager.OnHintButtonClicked += OnHintButtonClicked;
        _canvasManager.OnAnswerButtonClicked += OnAnswerSelected;
        _canvasManager.OnQuizSelected += OnQuizSelected;
        _canvasManager.OnBackButtonClicked += OnBackButtonClicked;
        
        _gameState = GameState.SplashScreen;
        UpdateGameState(GameState.SplashScreen);
    }

    private void HandleBackgrounds(int backgroundIndex = 0)
    {
        Debug.Log("GameManager: Background Index: " + backgroundIndex);
        _canvasManager.HandleBackgrounds(backgroundIndex);
    }
    
    private int GetLevelCategory(int quizIndex)
    {
        Debug.Log("GameManager: Quiz Index: " + quizIndex);
        if (quizIndex < FirstLevelQuizzes.Count)
        {
            Debug.Log("GameManager: Level Category: 0");
            return 0;
        }
        if (quizIndex < FirstLevelQuizzes.Count + SecondLevelQuizzes.Count)
        {
            Debug.Log("GameManager: Level Category: 1");
            return 1;
        }
        
        Debug.Log("GameManager: Level Category: 2");
        return 2;
    }
    
    private void HandleLevelSelection(int levelIndex, bool isSelectedManually = false)
    {
        if (isSelectedManually)
        {
            if(_gameState != GameState.Quiz)
                UpdateGameState(GameState.Quiz);
            
            CurrentQuizIndex = levelIndex;
            var levelCategory = GetLevelCategory(CurrentQuizIndex);
            Debug.Log("GameManager: Level Category: " + levelCategory);
            switch (levelCategory)
            {
                case 0:
                    HandleBackgrounds();
                    var quizVariation = Utils.SelectRandomQuizVariation(FirstLevelQuizzes[CurrentQuizIndex]);
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(FirstLevelQuizzes[CurrentQuizIndex]));
                    break;
                case 1:
                    HandleBackgrounds(1);
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(SecondLevelQuizzes[CurrentQuizIndex - FirstLevelQuizzes.Count]));
                    break;
                case 2:
                    HandleBackgrounds(2);
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(ThirdLevelQuizzes[CurrentQuizIndex - FirstLevelQuizzes.Count - SecondLevelQuizzes.Count]));
                    break;
                default:
                    throw new Exception("Invalid level category");
            }
            return;
        }
        
        if (_gameState != GameState.Quiz) return;
        
        if(CurrentQuizIndex < _quizzesCount - 1)
        {
            CurrentQuizIndex++;
            var levelCategory = GetLevelCategory(CurrentQuizIndex);
            switch (levelCategory)
            {
                case 0:
                    HandleBackgrounds();
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(FirstLevelQuizzes[CurrentQuizIndex]));
                    break;
                case 1:
                    HandleBackgrounds(1);
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(SecondLevelQuizzes[CurrentQuizIndex - FirstLevelQuizzes.Count]));
                    break;
                case 2:
                    HandleBackgrounds(2);
                    _quiz.Initialize(Utils.SelectRandomQuizVariation(ThirdLevelQuizzes[CurrentQuizIndex - FirstLevelQuizzes.Count - SecondLevelQuizzes.Count]));
                    break;
                default:
                    throw new Exception("Invalid level category");
            }
        }
        else
        {
            UpdateGameState(GameState.QuizSelection);
        }
    }

    private void OnCorrectAnswerClicked()
    {
        HandleLevelSelection(CurrentQuizIndex);
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("GameManager: Back Button Clicked");
        UpdateGameState(GameState.QuizSelection);
    }

    private void OnQuizSelected(int quizIndex)
    {
        Debug.Log("GameManager: Quiz Selected: " + quizIndex);
        HandleLevelSelection(quizIndex, true);
    }

    private void UpdateGameState(GameState gameState)
    {
        _gameState = gameState;
        
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