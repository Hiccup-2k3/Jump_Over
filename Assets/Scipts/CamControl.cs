using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float lerpTime;
    public float xOffSet;
    bool P_canLerp;
    float P_lerpDis;

    private void Start()
    {
        P_canLerp = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if( P_canLerp )
        {
            MoveLerp();
        }


    }

    void MoveLerp()
    {
       
       transform.position = new Vector3( P_lerpDis, transform.position.y,transform.position.z );
       
    }

    public void SetLerp(bool LerpOrNot, float xPos)
    {
        P_lerpDis = xPos;
        P_canLerp = LerpOrNot;
    }
}
