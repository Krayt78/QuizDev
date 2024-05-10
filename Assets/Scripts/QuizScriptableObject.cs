using UnityEngine;

[CreateAssetMenu(fileName = "QuizScriptableObject", menuName = "ScriptableObjects/", order = 1)]
public class QuizScriptableObject: ScriptableObject
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswerIndex;
    public string Hint;
    public string ImagePath;
    public string AudioPath;
    public string VideoPath;
    public string Explanation;
    public bool IsAnswered;
    public bool IsHintUsed;
}