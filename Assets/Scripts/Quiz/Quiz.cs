using UnityEngine;
using UnityEngine.Serialization;

namespace Quiz
{
    public class Quiz:MonoBehaviour
    {
        enum difficultySetting
        {
        Easy,
        Medium,
        Hard,
        }
        private difficultySetting difficulty;
        public delegate void CorrectAnswerClicked();
        public delegate void WrongAnswerClicked();
        public event CorrectAnswerClicked OnCorrectAnswerClicked;
        public event WrongAnswerClicked OnWrongAnswerClicked;
        
        [FormerlySerializedAs("QuizScriptableObject")] public QuizDataScriptableObject quizDataScriptableObject;
        public QuizCanvasManager QuizCanvasManager;
        
        private int _correctAnswerIndex;
        private QuizDataScriptableObject _currentQuizDataData;
        private int currentQuizDataIndex = -1;
        private QuizVariationsScriptableObject _quizVariations;

        public void Initialize(QuizVariationsScriptableObject quizVariations)
        {
            _quizVariations = quizVariations;
            var quizData = Utils.SelectRandomQuizVariation(quizVariations);
            
            CleanupSubscriptions();
            
            Debug.Log($"Quiz initialized with question: {quizData.Question}");
            QuizCanvasManager.Initialize(quizData);
            _correctAnswerIndex = quizData.CorrectAnswerIndex;
            _currentQuizDataData = quizData;
            
            QuizCanvasManager.OnAnswerButtonClicked += OnAnswerButtonClicked;
            QuizCanvasManager.OnHintButtonClicked += OnHintButtonClicked;
            QuizCanvasManager.OnBackButtonClicked += OnBackButtonClicked;
            QuizCanvasManager.OnTimerRanOut += OnTimerRanOut;
            
            QuizCanvasManager.HintPopup.HideHint();
            DetermineDifficulty();
            SetTimer();
        }

        private void CleanupSubscriptions()
        {
            QuizCanvasManager.OnAnswerButtonClicked -= OnAnswerButtonClicked;
            QuizCanvasManager.OnHintButtonClicked -= OnHintButtonClicked;
            QuizCanvasManager.OnBackButtonClicked -= OnBackButtonClicked;
            QuizCanvasManager.OnTimerRanOut -= OnTimerRanOut;
        }

        private void OnTimerRanOut()
        {
            LoseGame();
        }
        
        private void LoseGame()
        {
            StartCoroutine(FindObjectOfType<CodyAnimationController>().MoveCodyWhileInAnimation(false));
            
            QuizCanvasManager.StopAllCoroutines();
            var quizData = Utils.SelectRandomQuizVariation(_quizVariations, currentQuizDataIndex);
            
            QuizCanvasManager.Initialize(quizData);
            _correctAnswerIndex = quizData.CorrectAnswerIndex;
            _currentQuizDataData = quizData;
            
            //QuizCanvasManager.ShowLosePopup();
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("Back button clicked");
        }

        void DetermineDifficulty()
        {
            if (_currentQuizDataData.name.Contains("E"))
                difficulty = difficultySetting.Easy;
            else if (_currentQuizDataData.name.Contains("M"))
            {
                difficulty = difficultySetting.Medium;
            } else if (_currentQuizDataData.name.Contains("H"))
            {
                difficulty = difficultySetting.Hard;
            } else 
                Debug.LogWarning("Difficulty not set");
        }

        void SetTimer()
        {
            if (difficulty == difficultySetting.Easy)
            {
                StartCoroutine(QuizCanvasManager.StartTimer(90));
            }
            else if (difficulty == difficultySetting.Medium)
            {
                StartCoroutine(QuizCanvasManager.StartTimer(60));
            }
            else if (difficulty == difficultySetting.Hard)
            {
                StartCoroutine(QuizCanvasManager.StartTimer(45));
            }
        }

        private void OnHintButtonClicked()
        {
            Debug.Log("Hint button clicked");
            QuizCanvasManager.HintPopup.ShowHint(_currentQuizDataData.Hint);
        }

        private void OnAnswerButtonClicked(int answerIndex)
        {
            QuizCanvasManager.StopAllCoroutines();
            
            Debug.Log($"Answer button clicked with index: {answerIndex}");
            if(answerIndex == _correctAnswerIndex)
            {
                Debug.Log("Correct answer!");
                //do somethimg here
                
                OnCorrectAnswerClicked?.Invoke();
                StartCoroutine(FindObjectOfType<CodyAnimationController>().MoveCodyWhileInAnimation(true));
            }
            else
            {
                Debug.Log("Wrong answer!");
                LoseGame();
            }
            
        }
    }
}