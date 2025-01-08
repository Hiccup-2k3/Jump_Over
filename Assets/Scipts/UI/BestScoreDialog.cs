using UnityEngine;
using UnityEngine.UI;

public class BestScoreDialog : Dialog
{
    public Text ScoreText;


    public override void Show(bool isShow)
    {
        
        if(ScoreText ) 
            ScoreText.text = Prefs.BestScore.ToString();
        else
            ScoreText.color = Color.white;
        gameObject.SetActive(true);
    }
}
