
using UnityEngine;

public class AutoDestroy_25 : MonoBehaviour {

    [Header("Snake Manager")]
    SnakeMovement_25 SM;

   
    [Header("Hits to be destroyed")]
    public int life;
    public float lifeForColor;
    TextMesh thisTextMesh;

    GameObject[] ToDestroy;
    GameObject[] ToUnparent;

    [Header("Color Management")]
    int maxLifeForRed = 50;

    //To fix the initial position due to some issues
    Vector3 initialPos;
    public bool dontMove;

   

    //Initially set the size of the box
    void SetBoxSize()
    {
       // float x;
       // float y;

        //Debug.Log(Screen.height + "\n" + Screen.width + "\n" + Screen.height / Screen.width);

        transform.localScale *= ((float)Screen.width / (float)Screen.height) / (9f/16f) ;
    }

   


    // Use this for initialization
    void Start () {

       
  


        //Set the box size depending on the resolution
        SetBoxSize();

        //Set the Snake Movement
        SM = GameObject.FindGameObjectWithTag("TAG_4").GetComponent<SnakeMovement_25>();

       

        //Initialize the amount of lives
       
        if (transform.tag == "TAG_1")
        {
            life = Random.Range(1, SM.BodyParts.Count + 2);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);
        }


        if (transform.tag == "TAG_5")
        {
            life = Random.Range(10, 60);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);

        }
        if (transform.tag == "TAG_6")
        {
            life = Random.Range(15, SM.BodyParts.Count + 5);
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(false);
        }
        if (transform.tag == "TAG_8")
        {
            life = Random.Range(15, SM.BodyParts.Count + 5);
           transform.GetChild(5).gameObject.SetActive(true);
           transform.GetChild(4).gameObject.SetActive(false);

          
        }


        lifeForColor = life;

        //Initialize this text Mesh
        thisTextMesh = GetComponentInChildren<TextMesh>();
        thisTextMesh.text = "" + life;
      

       

        //Set the color of the box depending on the life
        SetBoxColor();

        //Values to fix the position of the block
        initialPos = transform.position;
        dontMove = false;
    }
  

    public  void Bars()
    {
        int R = Random.Range(6, 9);

        for (int i = R; i < transform.childCount; i++)
        {

            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).SetParent(null);
        }


    }






    public void BlockUpdate()
    {

        //Initialize the amount of lives
        SM = GameObject.FindGameObjectWithTag("TAG_4").GetComponent<SnakeMovement_25>();

        if (transform.tag == "TAG_1")
        {
            life = Random.Range(1, SM.BodyParts.Count + 2);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);
        }


        if (transform.tag == "TAG_5")
        {
            life = Random.Range(10, 60);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);

        }
        if (transform.tag == "TAG_6")
        {
            life = Random.Range(15, SM.BodyParts.Count + 5);
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(false);
        }
        if (transform.tag == "TAG_8")
        {
            life = Random.Range(15, SM.BodyParts.Count + 5);
          transform.GetChild(5).gameObject.SetActive(true);
           transform.GetChild(4).gameObject.SetActive(false);
           
           
        }


        lifeForColor = life;

        //Initialize this text Mesh
        thisTextMesh = GetComponentInChildren<TextMesh>();
        thisTextMesh.text = "" + life;

        SetBoxColor();
    }
	
	// Update is called once per frame
	void Update () {

        //Fix the position
        if(dontMove)
            transform.position = initialPos;










      
        //SetBoxColor();

        lifeForColor = life;

        //If the block has 0 life
        if (life <= 0)
        {


            gameObject.SetActive(false);


            //Play the particle System if it's off
            if (transform.GetComponentInChildren<ParticleSystem>().isStopped)
                transform.GetComponentInChildren<ParticleSystem>().Play();

            //Destroy after 1 sec
           
        }

    }
   

    public void UpdateText()
    {
        thisTextMesh.text = "" + life;
    }

  


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("TAG_10"))
        {
            gameObject.SetActive(false);


           
        }
        if(collision.gameObject.CompareTag("TAG_1"))
        {
            gameObject.SetActive(false);
        }
     else  if(collision.gameObject.CompareTag("TAG_6"))
        {
            gameObject.SetActive(false);
        }
      else if(collision.gameObject.CompareTag("TAG_8"))
        {
            gameObject.SetActive(false);
        }
    
    
    }
   



    public void SetBoxColor()
    {
        Color32 thisImageColor = GetComponent<SpriteRenderer>().color;

        if (lifeForColor > maxLifeForRed)
            thisImageColor = new Color32(255, 64, 61, 255 );

        else if (lifeForColor >= maxLifeForRed / 2 && lifeForColor <= maxLifeForRed)
            thisImageColor = new Color32(255, (byte)(510 * (1 - (lifeForColor / maxLifeForRed))), 50, 255);

        else if (lifeForColor > 0 && lifeForColor < maxLifeForRed)
            thisImageColor = new Color32((byte)(510 * lifeForColor / maxLifeForRed), 255, 50, 255);

        GetComponent<SpriteRenderer>().color = thisImageColor;

    }
}
