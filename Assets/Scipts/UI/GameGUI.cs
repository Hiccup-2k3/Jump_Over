using UnityEngine;
using UnityEngine.UI;

public class GameGUI : Singleton<GameGUI>
{
    public GameObject HomeGui;
    public GameObject GameGui;

    public Text Score;
    public Image JumpForce;

    public GameObject HighScoreDL;
    public GameObject GameOverDL;
    private void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if(HomeGui)
            HomeGui.SetActive(isShow);
        if(GameGui)
            GameGui.SetActive(!isShow);  
    }

    public void UpdatePowerBar(float force)
    {

        if (JumpForce)
            if(force >= 15)
                JumpForce.fillAmount = 1;
            else        
                JumpForce.fillAmount = (force / 15) ;
    }

    public void ShowHighestScoreDialog()
    {
        if (HighScoreDL)
            HighScoreDL.SetActive(true);
    }
    public void HideHighestScoreDialog()
    {
        if(HighScoreDL)
            HighScoreDL.SetActive(false);
    }
    public void ShowGameOverDLDialog()
    {
        if (GameOverDL)
            GameOverDL.SetActive(true);
    }public void HideGameOverDLDialog()
    {
        if (GameOverDL)
            GameOverDL.SetActive(false);
    }

    public void UpdateScores()
    {
        if (Score)
            Score.text = GameManager.Ins.GetScore().ToString();
    }


}
