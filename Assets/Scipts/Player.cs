using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngineInternal;
using System;
using UnityEngine.SocialPlatforms;

public class Player : Singleton<Player>
{
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;
    public float maxForceX;
    public float maxForceY;
    public float minForceY;
    public float minForceX;
    public float moveMent;
    public Vector2 debug ;

    [HideInInspector]
    public int lastPlatformID;
    public List<Platform> platforms = new List<Platform>();

    bool P_isJump;
    bool P_powerSetted;

    Rigidbody2D P_rgbd2d;
    Animator P_ani;
    Transform P_tran;
    CamControl camControl;
    BackGround backGround;
    BackGround1 backGround01;
    
    private void Awake()
    {
        P_rgbd2d = GetComponent<Rigidbody2D>();
        P_ani = GetComponent<Animator>();
        P_tran = GetComponent<Transform>();
        camControl = GameObject.FindObjectOfType<CamControl>();
        backGround = GameObject.FindObjectOfType<BackGround>();
        backGround01 = GameObject.FindObjectOfType<BackGround1>();
        
        
    }


    private void Update()
    {
        
        if (Input.GetMouseButton(0))
            PowerIsSetting();
        if(Input.GetMouseButtonUp(0))
            ReleasePower();
        if (P_isJump)
            camControl.SetLerp(true, this.transform.position.x);
        if (this.transform.position.x >= backGround.transform.position.x )
        {
            backGround01.transform.position = new Vector3(backGround.transform.position.x + backGround.GetComponent<Renderer>().bounds.size.x, 0, 0);
            
        }

        if (this.transform.position.x >= backGround01.transform.position.x )
        {
            backGround.transform.position = new Vector3(backGround01.transform.position.x + backGround01.GetComponent<Renderer>().bounds.size.x, 0, 0);
            
        }
        
        




    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            P_isJump = false;
            P_rgbd2d.linearVelocity = Vector2.zero;
            jumpForce = Vector2.zero;
            P_ani.SetBool("isJump", false);
            camControl.SetLerp(false, this.transform.position.x);
        }
    }

   

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConst.GROUND))
        {

            Platform p = collision.transform.root.GetComponent<Platform>();
            {
                P_rgbd2d.linearVelocity = Vector2.zero;
                jumpForce = Vector2.zero;
                P_isJump = false;
                P_ani.SetBool("isJump", false);
            }

            if(p && p.id != lastPlatformID)
            {
                lastPlatformID = p.id;
                GameManager.Ins.CreatePlatform();
                GameManager.Ins.ScoreIncre();
                GameGUI.Ins.UpdateScores();
            }
            
        }
        camControl.SetLerp(false, this.transform.position.x);
        if(collision.CompareTag(TagConst.DEATHZONE))
        {
            //Destroy(this.gameObject);
            
            Prefs.BestScore = GameManager.Ins.GetScore();
            debug.x = Prefs.BestScore;
            debug.y = GameManager.Ins.GetScore();
            GameGUI.Ins.ShowGameOverDLDialog();
            

        }
        

    }

    void PowerIsSetting()
    {
        if ( !P_isJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            jumpForce.x = Mathf.Clamp(jumpForce.x,minForceX,maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y,minForceY,maxForceY);
            GameGUI.Ins.UpdatePowerBar((float)Math.Sqrt(jumpForce.x*jumpForce.x+ jumpForce.y * jumpForce.y));
        }

        
    }

    void ReleasePower()
    {
        
        if( !P_isJump)
        {
            Jump();
            GameGUI.Ins.UpdatePowerBar(0);
        }
    }
    
    void Jump()
    {
        if (!P_rgbd2d || jumpForce.x <= 0 || jumpForce.y <= 0)
            return;
        P_rgbd2d.linearVelocity = jumpForce;
        
        if (P_ani)
        {
            P_ani.SetBool("isJump", true);

        }
        P_isJump = true;
        debug.x = platforms.Count;



    }

}
