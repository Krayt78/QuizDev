using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
namespace Quiz
{
    public class Quiz : MonoBehaviour
    {
        public delegate void CorrectAnswerClicked();

        public delegate void WrongAnswerClicked();

        [FormerlySerializedAs("TopPanel")] public TopPanel topPanel;

        [FormerlySerializedAs("QuizScriptableObject")]
        public QuizDataScriptableObject quizDataScriptableObject;

        public QuizCanvasManager QuizCanvasManager;

        private int _correctAnswerIndex;
        private QuizDataScriptableObject _currentQuizDataData;
        private QuizVariationsScriptableObject _quizVariations;
        private readonly int currentQuizDataIndex = -1;
        private difficultySetting difficulty;

        public List<Color> colors;
        public event CorrectAnswerClicked OnCorrectAnswerClicked;
        public event WrongAnswerClicked OnWrongAnswerClicked;

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

        private async Task LoseGame()
        {
            if (QuizCanvasManager.Background1.activeSelf)
                await FindObjectOfType<CodyAnimationController>().MoveCodyWhileInAnimationAsync(false);
            if (QuizCanvasManager.Background2.activeSelf) await ShowSpriteAndHide(false, 1);
            if (QuizCanvasManager.Background3.activeSelf) await ShowSpriteAndHide(false, 2);

            QuizCanvasManager.ToggleButtonInteractibility(false);
            QuizCanvasManager.StopAllCoroutines();

            var quizData = Utils.SelectRandomQuizVariation(_quizVariations, currentQuizDataIndex);

            QuizCanvasManager.Initialize(quizData);
            _correctAnswerIndex = quizData.CorrectAnswerIndex;
            _currentQuizDataData = quizData;
            ResetTimer();
        }

        private void ResetTimer()
        {
            DetermineDifficulty();
            SetTimer();
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("Back button clicked");
        }

        private void DetermineDifficulty()
        {
            if (_currentQuizDataData.name.Contains("E"))
                difficulty = difficultySetting.Easy;
            else if (_currentQuizDataData.name.Contains("M"))
                difficulty = difficultySetting.Medium;
            else if (_currentQuizDataData.name.Contains("H"))
                difficulty = difficultySetting.Hard;
            else
                Debug.LogWarning("Difficulty not set");
        }

        private void SetTimer()
        {
            StopAllCoroutines();
            if (difficulty == difficultySetting.Easy)
                StartCoroutine(QuizCanvasManager.StartTimer(90));
            else if (difficulty == difficultySetting.Medium)
                StartCoroutine(QuizCanvasManager.StartTimer(60));
            else if (difficulty == difficultySetting.Hard) StartCoroutine(QuizCanvasManager.StartTimer(45));
        }

        private void OnHintButtonClicked()
        {
            Debug.Log("Hint button clicked");
            QuizCanvasManager.HintPopup.ShowHint(_currentQuizDataData.Hint);
        }

        private void OnAnswerButtonClicked(int answerIndex)
        {
            OnAnswerButtonClickedAsync(answerIndex);
        }

        private async Task OnAnswerButtonClickedAsync(int answerIndex)
        {
            QuizCanvasManager.StopAllCoroutines();
            QuizCanvasManager.ToggleButtonInteractibility(true);

            Debug.Log($"Answer button clicked with index: {answerIndex}");
            if (answerIndex == _correctAnswerIndex)
            {
                Debug.Log("Correct answer!");
                //do somethimg here

                if (QuizCanvasManager.Background1.activeSelf)
                {
                    await FindObjectOfType<CodyAnimationController>().MoveCodyWhileInAnimationAsync(true);
                    QuizCanvasManager.ToggleButtonInteractibility(false);
                    OnCorrectAnswerClicked?.Invoke();
                }

                if (QuizCanvasManager.Background2.activeSelf)
                {
                    await ShowSpriteAndHide(true, 1);
                    OnCorrectAnswerClicked?.Invoke();
                }

                if (QuizCanvasManager.Background3.activeSelf)
                {
                    await ShowSpriteAndHide(true, 2);
                    OnCorrectAnswerClicked?.Invoke();
                }
            }
            else
            {
                Debug.Log("Wrong answer!");
                await LoseGame();
            }
        }

        private async Task ShowSpriteAndHide(bool success, int sprite)
        {
            if (sprite == 1)
            {
                if (success)
                    topPanel.BreadSuccess.SetActive(true);
                else
                    topPanel.BreadFail.SetActive(true);
            }
            else
            {
                if (success)
                    topPanel.PoliceSuccess.SetActive(true);
                else
                    topPanel.PoliceFail.SetActive(true);
            }

            await Task.Delay(2000);

            topPanel.BreadSuccess.SetActive(false);
            topPanel.BreadFail.SetActive(false);
            topPanel.PoliceSuccess.SetActive(false);
            topPanel.PoliceFail.SetActive(false);
        }

        private enum difficultySetting
        {
            Easy,
            Medium,
            Hard
        }
    }
}