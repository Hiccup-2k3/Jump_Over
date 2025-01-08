using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    public Platform platformPrefab;
    
    
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    Player P_player;
    int P_Score;
    

    public override void Start()
    {
        base.Start();
        P_Score = 0;
        GameGUI.Ins.UpdateScores();
        GameGUI.Ins.UpdatePowerBar(0);
        MainUI();

        
    }
    
    public void MainUI()
    {
       GameGUI.Ins.HideGameOverDLDialog();
       GameGUI.Ins.ShowGameGUI(true);
    }
    public void PlayGame()
    {
        StartCoroutine(PlatformInit());
        GameGUI.Ins.ShowGameGUI(false);
    }

    public override void Awake() // chỉ lưu dữ liệu ở scene hiện tại 
    {
        MakeSingleton(false);
        
    }

    IEnumerator PlatformInit()
    {
        
        Platform insPlatform = null;
        if (platformPrefab)
        {
            insPlatform = Instantiate(platformPrefab, new Vector2(0, Random.Range(minY, maxY)), Quaternion.identity);
            insPlatform.id = insPlatform.gameObject.GetInstanceID();
            
        }
        yield return new WaitForSeconds(0.5f);
        
        if(playerPrefab)
        {
            P_player = Instantiate(playerPrefab,new Vector3(0,7f,0), Quaternion.identity);
            P_player.lastPlatformID = insPlatform.id;
        }


        
        Platform ins2Plaform = null;
        if (platformPrefab)
        {
            float posX = P_player.GetComponentInParent<Transform>().position.x + Random.Range(minX, maxX);
            float posY = Random.Range(minY,maxY)  ;
            ins2Plaform = Instantiate(platformPrefab, new Vector2(posX, posY), Quaternion.identity);
            ins2Plaform.id = ins2Plaform.gameObject.GetInstanceID();
            
        }

    }

    public void CreatePlatform()
    {
        Platform ClonePlaform = null;
        if (platformPrefab)
        {
            float posX = P_player.GetComponentInParent<Transform>().position.x + Random.Range(minX,maxX);
            float posY = Random.Range(minY, maxY);
            ClonePlaform = Instantiate(platformPrefab, new Vector2(posX, posY), Quaternion.identity);
            ClonePlaform.id = ClonePlaform.gameObject.GetInstanceID();
            
        }

    }

    public void ScoreIncre()
    {
        P_Score++;
        
    }

    public int GetScore()
    {
        return P_Score;
    }

    

    

}
