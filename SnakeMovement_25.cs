
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement_25 : MonoBehaviour
{

    [Header("Managers")]
    public GameController_25 GC;






    public bool PowerOn;
    public bool MagnetOn;
    public BGRepeat_25 BG;

    public bool Collide;

    [Header("Some Snake Variables & Storing")]
    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.1f;
    public int initialAmount;
    public float speed = 1;
    public float rotationSpeed = 50;
    public float LerpTimeX;
    public float LerpTimeY;

    [Header("Snake Head Prefab")]
    public GameObject BodyPrefab;

    [Header("Parts Text Amount Management")]
    public TextMesh PartsAmountTextMesh;

    [Header("Private Fields")]
    private float distance;
    private Vector3 refVelocity;

    private Transform curBodyPart;
    private Transform prevBodyPart;

    private bool firstPart;
    public float delay = 7f;
    public float Magnetdelay = 7f;




    [Header("Mouse Control Variables")]
    Vector2 mousePreviousPos;
    Vector2 mouseCurrentPos;

    [Header("Particle System Management")]
    public ParticleSystem SnakeParticle;
    public GameObject power;

    // Use this for initialization
    void Start()
    {


        //At the beginning, if it's the first part, spawn it at (0, -3, 0)
        firstPart = true;

        //Add the initial BodyParts
        for (int i = 0; i < initialAmount; i++)
        {
            //Use invoke to avoid a weird bug where the snake goes down at the beginning.
            AddBodyPart();
        }

    }

    public void SpawnBodyParts()
    {
        firstPart = true;

        //Add the initial BodyParts
        for (int i = 0; i < initialAmount; i++)
        {
            //Use invoke to avoid a weird bug where the snake goes down at the beginning.
            AddBodyPart();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (MagnetOn)
        {

            

            Magnetdelay -= Time.deltaTime;

            if (Magnetdelay>0)
            {
               if(BodyParts.Count>0)
                BodyParts[0].GetChild(3).gameObject.SetActive(true);

            }
            else if (Magnetdelay<0)
            {
                BodyParts[0].GetChild(3).gameObject.SetActive(false);
                MagnetOn = false;
            }
        
        }
        else
        {
            Magnetdelay = 7f;
        }

        if (PowerOn == true)
        {


            delay -= Time.deltaTime;

            if (delay > 0)
            {
                if(BodyParts.Count>0)
                BodyParts[0].transform.GetChild(2).gameObject.SetActive(true);
                speed = 14;
                LerpTimeY = 0.55f;
                power.SetActive(true);
                if (!GameController_25.instance.Boost.isPlaying)
                    GameController_25.instance.Boost.Play();



            }
            if (delay < 0.5f)
                BodyParts[0].GetComponent<Animation>().Play();

            if (delay < 0)
            {
                BodyParts[0].transform.GetChild(2).gameObject.SetActive(false);
                PowerOn = false;

                LerpTimeY = 0.33f;
                BodyParts[0].GetComponent<Animation>().Stop();
                power.SetActive(false);
                GameController_25.instance.Boost.Stop();
            }

        }

        else
        {
            delay = 7f;
            if (int.Parse(GameController_25.instance.SCORE.ToString())<25)
            {
                speed = 5f;
                LerpTimeY = 0.34f;
                BG.scrollSpeed = 0.1f;
            }
            else
                SpeedAdded();

        }



        Move();

        //Check if no more snake parts
        if (BodyParts.Count == 0)
        {
            MagnetOn = false;
            PowerOn = false;
            GameController_25.instance.gameOver = true;

            BG.scrollSpeed = 0;
            if (!GameController_25.instance.GameOverSound.isPlaying)
                GameController_25.instance.GameOverSound.PlayOneShot(GameController_25.instance.GameOverSound.clip, 1f);
            


        }


    





        //Update the Parts Amount text
        if (PartsAmountTextMesh != null)
            PartsAmountTextMesh.text = transform.childCount + "";


    }

    public void Move()
    {
        float curSpeed = speed;

        //Always move the body Up
        if (BodyParts.Count > 0)
        {
            if (Collide)
            {
                BodyParts[0].GetChild(0).gameObject.SetActive(false);
                BodyParts[0].Translate(Vector2.down * 10 * Time.smoothDeltaTime);
                BodyParts[0].GetChild(1).gameObject.SetActive(true);
                GameController_25.instance.SFX.PlayOneShot(GameController_25.instance.SFX.clip, 1);

                Collide = false;

            }
            else
            {


                BodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);
                BodyParts[0].GetChild(0).gameObject.SetActive(true);
                BodyParts[0].GetChild(1).gameObject.SetActive(false);
            }
        }
        //check if we are still on screen
        float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;

        if (BodyParts.Count > 0)
        {
            if (BodyParts[0].position.x > maxX) //Right pos
            {
                BodyParts[0].position = new Vector3(maxX - 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
            else if (BodyParts[0].position.x < -maxX) //Left pos
            {
                BodyParts[0].position = new Vector3(-maxX + 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
        }

        //Move the snake on the Horizontal Axis with mouse control
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {

                case TouchPhase.Began:
                    mousePreviousPos = Camera.main.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    if (BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX)
                    {
                        mouseCurrentPos = Camera.main.ScreenToWorldPoint(touch.position);

                        float deltaMousePos = Mathf.Abs(mousePreviousPos.x - mouseCurrentPos.x);
                        float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);


                        //BodyParts[0].Translate(Vector3.left * rotationSpeed * Time.deltaTime * deltaMousePos * sign);
                        BodyParts[0].GetComponent<Rigidbody2D>().AddForce
                            (Vector2.right * rotationSpeed * deltaMousePos * -sign);

                        mousePreviousPos = mouseCurrentPos;
                    }
                    else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX) //Right pos
                    {
                        BodyParts[0].position = new Vector3(maxX - 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);

                    }
                    else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX) //Left pos
                    {
                        BodyParts[0].position = new Vector3(-maxX + 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);

                    }
                    break;


            }


        }



        /* if (Input.GetMouseButtonDown(0))
          {
              mousePreviousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          }
          else if (Input.GetMouseButton(0))
          {
              //check if he is still in screen

              if ( BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX)
              {
                  mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                  float deltaMousePos = Mathf.Abs(mousePreviousPos.x - mouseCurrentPos.x);
                  float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);


                //  BodyParts[0].Translate(Vector3.left * rotationSpeed * Time.deltaTime * deltaMousePos * sign);
      BodyParts[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * rotationSpeed * deltaMousePos * -sign);

                  mousePreviousPos = mouseCurrentPos;
              }
              else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX) //Right pos
              {
                  BodyParts[0].position = new Vector3(maxX - 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);

              } else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX) //Left pos
              {
                  BodyParts[0].position = new Vector3(-maxX + 0.3f, BodyParts[0].position.y, BodyParts[0].position.z);

              }
          }*/



        //Move the other body parts depending on the Head, that's why we start the loop at 1
        for (int i = 1; i < BodyParts.Count; i++)
        {


            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            //  distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;


            newPos.z = BodyParts[0].position.z;

            //Try 2 Lerps, one on the x pos and one on the Y
            Vector3 pos = curBodyPart.position;

            pos.x = Mathf.Lerp(pos.x, newPos.x, LerpTimeX);
            pos.y = Mathf.Lerp(pos.y, newPos.y, LerpTimeY);

            curBodyPart.position = pos;
        }



    }

    public void AddBodyPart()
    {
        Transform newPart;

        if (firstPart)
        {
            newPart = (Instantiate(BodyPrefab, new Vector3(0, 0, 0),
                       Quaternion.identity) as GameObject).transform;


            //newPart.GetComponent<SpriteRenderer>().sortingOrder = 1;

            // Set this part as the parent of the Text Mesh
            PartsAmountTextMesh.transform.parent = newPart;

            //Place it correctly
            PartsAmountTextMesh.transform.position = newPart.position +
                new Vector3(0, 0.5f, 0);

            firstPart = false;

        }
        else
        {


            newPart = (Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].position,
           BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
            newPart.GetComponent<SpriteRenderer>().sortingOrder = BodyParts[BodyParts.Count - 1].GetComponent<SpriteRenderer>().sortingOrder - 1;
        }

        newPart.SetParent(transform);

        BodyParts.Add(newPart);

    }
    void SpeedAdded()
    {
        switch (int.Parse(GameController_25.instance.SCORE.ToString()))
        {
            case 25:
                speed = 6f;
                LerpTimeY = 0.36f;
                BG.scrollSpeed = 0.15f;

                break;

            case 50:
                speed = 7f;
                LerpTimeY = 0.37f;
                BG.scrollSpeed = 0.2f;
                break;

            case 100:
                speed = 8f;
                LerpTimeY = 0.42f;
                BG.scrollSpeed = 0.25f;
                break;

            case 150:

                speed = 9f;
                LerpTimeY = 0.44f;
                BG.scrollSpeed = 0.3f;
                break;

            case 200:
                speed = 10f;
                LerpTimeY = 0.47f;
                BG.scrollSpeed = 0.35f;
                break;


            case 250:
                speed = 11f;
                LerpTimeY = 0.49f;
                BG.scrollSpeed = 0.4f;
                break;

            case 300:
                speed = 12f;
                LerpTimeY = 0.52f;
                BG.scrollSpeed = 0.45f;

                break;





        }
    }
}

