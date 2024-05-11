using System;
using UnityEngine;

namespace Quiz
{
    public class Quiz:MonoBehaviour
    {
        public QuizScriptableObject QuizScriptableObject;
        public QuizCanvasManager QuizCanvasManager;
        
        private int _correctAnswerIndex;
        private QuizScriptableObject _currentQuizData;

        private void Start()
        {
            Initialize(QuizScriptableObject);
        }

        public void Initialize(QuizScriptableObject quiz)
        {
            Debug.Log($"Quiz initialized with question: {quiz.Question}");
            QuizCanvasManager.Initialize(quiz);
            
            QuizCanvasManager.OnAnswerButtonClicked += OnAnswerButtonClicked;
            QuizCanvasManager.OnHintButtonClicked += OnHintButtonClicked;
            QuizCanvasManager.OnBackButtonClicked += OnBackButtonClicked;
            
            _correctAnswerIndex = quiz.CorrectAnswerIndex;
            _currentQuizData = quiz;
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("Back button clicked");
        }

        private void OnHintButtonClicked()
        {
            Debug.Log("Hint button clicked");
            Debug.Log($"Hint is: {_currentQuizData.Hint}");
        }

        private void OnAnswerButtonClicked(int answerindex)
        {
            Debug.Log($"Answer button clicked with index: {answerindex}");
            Debug.Log(answerindex == _correctAnswerIndex ? "Correct answer!" : "Wrong answer!");
        }
    }
}