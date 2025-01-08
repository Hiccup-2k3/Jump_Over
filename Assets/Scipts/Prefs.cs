using UnityEngine;

public static class Prefs 
{
    public static int BestScore
    {
        set
        {
            if(PlayerPrefs.GetInt(PreConst.BEST_SCORE,0) < value)
                PlayerPrefs.SetInt(PreConst.BEST_SCORE , value);
        }
        get => PlayerPrefs.GetInt(PreConst.BEST_SCORE, 0);
        

    }
}
