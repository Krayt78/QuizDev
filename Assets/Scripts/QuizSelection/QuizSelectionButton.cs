using UnityEngine;
using UnityEngine.UI;

namespace QuizSelection
{
    public class QuizSelectionButton: MonoBehaviour
    {
        public delegate void QuizSelected(int quizIndex);
        public event QuizSelected OnQuizSelected;
        
        public Text quizNameText;
        public Button button;
        
        private int _quizIndex;

        public void Initialize(QuizScriptableObject quiz, int index)
        {
            quizNameText.text = quiz.name;
            _quizIndex = index;
            button.onClick.AddListener(_OnButtonClicked);
        }

        private void _OnButtonClicked()
        {
            Debug.Log($"Quiz {_quizIndex} selected");
            OnQuizSelected?.Invoke(_quizIndex);
        }
    }
}