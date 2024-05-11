using System;
using UnityEngine;

namespace Quiz
{
    public class Quiz:MonoBehaviour
    {
        public delegate void CorrectAnswerClicked();
        public event CorrectAnswerClicked OnCorrectAnswerClicked;
        
        public QuizScriptableObject QuizScriptableObject;
        public QuizCanvasManager QuizCanvasManager;
        
        private int _correctAnswerIndex;
        private QuizScriptableObject _currentQuizData;

        public void Initialize(QuizScriptableObject quiz)
        {
            Debug.Log($"Quiz initialized with question: {quiz.Question}");
            QuizCanvasManager.Initialize(quiz);
            
            QuizCanvasManager.OnAnswerButtonClicked += OnAnswerButtonClicked;
            QuizCanvasManager.OnHintButtonClicked += OnHintButtonClicked;
            QuizCanvasManager.OnBackButtonClicked += OnBackButtonClicked;
            
            _correctAnswerIndex = quiz.CorrectAnswerIndex;
            _currentQuizData = quiz;
            
            QuizCanvasManager.HintPopup.HideHint();
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("Back button clicked");
        }

        private void OnHintButtonClicked()
        {
            Debug.Log("Hint button clicked");
            QuizCanvasManager.HintPopup.ShowHint(_currentQuizData.Hint);
        }

        private void OnAnswerButtonClicked(int answerindex)
        {
            Debug.Log($"Answer button clicked with index: {answerindex}");
            if(answerindex == _correctAnswerIndex)
            {
                Debug.Log("Correct answer!");
                //do somethimg here
                
                OnCorrectAnswerClicked?.Invoke();
            }
            else
            {
                Debug.Log("Wrong answer!");
            }
        }
    }
}