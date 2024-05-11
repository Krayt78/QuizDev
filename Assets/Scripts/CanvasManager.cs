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
    
    [Header("Canvases")]
    public Canvas SplashScreenCanvas;
    public Canvas QuizCanvas;
    public Canvas QuizSelectionCanvas;
    
    private SplashScreenManager _splashScreenManager;
    private QuizCanvasManager _quizCanvasManager;

    public void Initialize(List<QuizScriptableObject> quizes)
    {
        _splashScreenManager = FindObjectOfType<SplashScreenManager>();
        _splashScreenManager.OnSplashScreenButtonClicked += () => OnSplashScreenButtonClicked?.Invoke();
        
        var selectQuizManager = FindObjectOfType<SelectQuizManager>();
        selectQuizManager.Initialize(quizes);
        selectQuizManager.OnQuizSelected += _OnQuizSelected;
        
        var quizCanvasManager = FindObjectOfType<QuizCanvasManager>();
        quizCanvasManager.OnAnswerButtonClicked += _OnAnswerButtonClicked;
        quizCanvasManager.OnHintButtonClicked += _OnHintButtonClicked;
        quizCanvasManager.Initialize(quizes[0]);
    }
    
    private void _OnQuizSelected(int quizIndex)
    {
        
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
