
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController_25 : MonoBehaviour {

  

    public bool BGStop=false;

    public bool gameOver;

    public BGRepeat_25 bg;

    public AudioSource BG,SFX,Boost,GameOverSound;

    public GameObject GameOverAnim;
    public GameObject GGameover;



    float delay=3f;

   

    [Header("Score Management")]
    public Text ScoreText;
  
    
    public  Bettr_Encryption.Encrypt SCORE;

    public string Score_Encrypt;

    public AudioClip Clip;

    public static GameController_25 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        
    }

    // Use this for initialization
    void Start () {


        Physics2D.gravity = new Vector2(0, -9.81f);
        Application.targetFrameRate = 60;
        SCORE= new Bettr_Encryption.Encrypt(0);
        Score_Encrypt = "0";
        Score_Encrypt = XOREncryption.encryptDecrypt(Score_Encrypt);





    }

    // Update is called once per frame
    void Update () {

        //Update the score text
        ScoreText.text = SCORE + "";

        if (gameOver)
        {
            GameOverAnim.SetActive(true);
            BG.Stop();
            if (GameObject.FindGameObjectWithTag("TAG_14") != null)
                GameObject.FindGameObjectWithTag("TAG_14").SetActive(false);



            delay -= Time.deltaTime;
        }

       if(delay<0.5f)
            {
                GGameover.SetActive(true);
                gameOver = false;

            }

            

        
      
           


     
    }

    void GameOverOnTimer()
    {


        gameOver = true;
    }

   


    

   

  
}
