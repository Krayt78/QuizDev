using System.Collections.Generic;
using QuizSelection;
using UnityEngine;
using UnityEngine.UI;

public class QuizSelectionCanvasManager: MonoBehaviour
{
    public delegate void QuizSelected(int quizIndex);
    public event QuizSelected OnQuizSelected;
    
    public delegate void LevelCategorySelected(int levelCategory);
    public event LevelCategorySelected OnLevelCategorySelected;
    
    public List<Button> FirstLevelSelectionButtons;
    public List<Button> SecondLevelSelectionButtons;
    public List<Button> ThirdLevelSelectionButtons;
    
    public LevelSelectionPanel firstLevelPanel;
    public LevelSelectionPanel secondLevelPanel;
    public LevelSelectionPanel thirdLevelPanel;
    
    public Transform firstLevelContainer;
    public Transform secondLevelContainer;
    public Transform thirdLevelContainer;
    
    public GameObject quizSelectionButtonPrefab;
    public Transform quizSelectionButtonContainer;
    
    public void Initialize(List<QuizScriptableObject> firstLevelQuizes, List<QuizScriptableObject> secondLevelQuizes,
        List<QuizScriptableObject> thirdLevelQuizes)
    {
        for (var i = 0; i < firstLevelQuizes.Count; i++)
        {
            var quizSelectionButton = Instantiate(quizSelectionButtonPrefab, firstLevelContainer);
            var quizComponent = quizSelectionButton.GetComponent<QuizSelectionButton>();
            
            quizComponent.Initialize(firstLevelQuizes[i], i);
            quizComponent.OnQuizSelected += _OnQuizSelected;
        }
        
        for (var i = 0; i < secondLevelQuizes.Count; i++)
        {
            var quizSelectionButton = Instantiate(quizSelectionButtonPrefab, secondLevelContainer);
            var quizComponent = quizSelectionButton.GetComponent<QuizSelectionButton>();
            
            quizComponent.Initialize(secondLevelQuizes[i], i+firstLevelQuizes.Count);
            quizComponent.OnQuizSelected += _OnQuizSelected;
        }
        
        for (var i = 0; i < thirdLevelQuizes.Count; i++)
        {
            var quizSelectionButton = Instantiate(quizSelectionButtonPrefab, thirdLevelContainer);
            var quizComponent = quizSelectionButton.GetComponent<QuizSelectionButton>();
            
            quizComponent.Initialize(thirdLevelQuizes[i], i+firstLevelQuizes.Count+secondLevelQuizes.Count);
            quizComponent.OnQuizSelected += _OnQuizSelected;
        }
        
        firstLevelPanel.OnNextButtonPressed += () =>
        {
            OnLevelCategorySelected?.Invoke(1);
            firstLevelPanel.gameObject.SetActive(false);
            secondLevelPanel.gameObject.SetActive(true);
        };
        
        secondLevelPanel.OnBeforeButtonPressed += () =>
        {
            OnLevelCategorySelected?.Invoke(0);
            firstLevelPanel.gameObject.SetActive(true);
            secondLevelPanel.gameObject.SetActive(false);
        };
        secondLevelPanel.OnNextButtonPressed += () =>
        {
            OnLevelCategorySelected?.Invoke(2);
            secondLevelPanel.gameObject.SetActive(false);
            thirdLevelPanel.gameObject.SetActive(true);
        };
        
        thirdLevelPanel.OnBeforeButtonPressed += () =>
        {
            OnLevelCategorySelected?.Invoke(1);
            secondLevelPanel.gameObject.SetActive(true);
            thirdLevelPanel.gameObject.SetActive(false);
        };
        
        OnLevelCategorySelected?.Invoke(0);
        firstLevelPanel.gameObject.SetActive(true);
        secondLevelPanel.gameObject.SetActive(false);
        thirdLevelPanel.gameObject.SetActive(false);
    }
    
    private void _OnQuizSelected(int index)
    {
        OnQuizSelected?.Invoke(index);
    }
}