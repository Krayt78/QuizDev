using System.Collections.Generic;
using Quiz;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    public delegate void SplashScreenButtonClicked();
    public event SplashScreenButtonClicked OnSplashScreenButtonClicked;
    
    public delegate void HintButtonClicked();
    public event HintButtonClicked OnHintButtonClicked;

    public delegate void AnswerButtonClicked(int answerIndex);
    public event AnswerButtonClicked OnAnswerButtonClicked;
    
    public delegate void QuizSelected(int quizIndex);
    public event QuizSelected OnQuizSelected;
    
    public delegate void BackButtonClicked();
    public event BackButtonClicked OnBackButtonClicked;
    
    [Header("Canvases")]
    public Canvas SplashScreenCanvas;
    public Canvas QuizSelectionCanvas;
    public Canvas QuizCanvas;
    
    private SplashScreenCanvasManager _splashScreenCanvasManager;
    private QuizCanvasManager _quizCanvasManager;

    public void Initialize(List<QuizScriptableObject> quizes)
    {
        _splashScreenCanvasManager = FindObjectOfType<SplashScreenCanvasManager>();
        _splashScreenCanvasManager.OnSplashScreenButtonClicked += () => OnSplashScreenButtonClicked?.Invoke();
        
        var selectQuizManager = FindObjectOfType<QuizSelectionCanvasManager>();
        selectQuizManager.Initialize(quizes);
        selectQuizManager.OnQuizSelected += _OnQuizSelected;
        
        var quizCanvasManager = FindObjectOfType<QuizCanvasManager>();
        quizCanvasManager.OnAnswerButtonClicked += _OnAnswerButtonClicked;
        quizCanvasManager.OnHintButtonClicked += _OnHintButtonClicked;
        quizCanvasManager.OnBackButtonClicked += _OnBackButtonClicked;
        quizCanvasManager.Initialize(quizes[0]);
    }
    
    private void _OnBackButtonClicked()
    {
        OnBackButtonClicked?.Invoke();
    }
    
    private void _OnQuizSelected(int quizIndex)
    {
        OnQuizSelected?.Invoke(quizIndex);
    }

    private void _OnAnswerButtonClicked(int answerIndex)
    {
        OnAnswerButtonClicked?.Invoke(answerIndex);
    }

    private void _OnHintButtonClicked()
    {
        OnHintButtonClicked?.Invoke();
    }
}
