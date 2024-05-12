using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if(hex.Length == 8){
            a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r,g,b,a);
    }
    
    public static QuizDataScriptableObject SelectRandomQuizVariation(QuizVariationsScriptableObject quizVariations, int excludeIndex = -1)
    {
        List<QuizDataScriptableObject> tempList = new List<QuizDataScriptableObject>();
        for (int i = 0; i < quizVariations.QuizzVariations.Count; i++)
        {
            if (i != excludeIndex)
                tempList.Add(quizVariations.QuizzVariations[i]);
        }
        
        return tempList[Random.Range(0, tempList.Count)];
    }
}