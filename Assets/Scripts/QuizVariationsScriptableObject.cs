using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizScriptableObject", menuName = "ScriptableObjects/QuizVariations", order = 1)]
public class QuizVariationsScriptableObject: ScriptableObject
{
    public List<QuizDataScriptableObject> QuizzVariations;
}