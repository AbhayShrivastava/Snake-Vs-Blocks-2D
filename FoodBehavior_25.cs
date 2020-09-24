using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior_25 : MonoBehaviour {

    [Header("Snake Manager")]
    SnakeMovement_25 SM;

    [Header("Food Amount")]
    public int foodAmount;
    Vector2 pos;
     float distance=5f;
    float coinspeed=15f;
  
    // Use this for initialization
    void Start () {
        SM = GameObject.FindGameObjectWithTag("TAG_4").GetComponent<SnakeMovement_25>();

        foodAmount = Random.Range(1, 8);

        transform.GetComponentInChildren<TextMesh>().text = "" + foodAmount;

     

    }

    // Update is called once per frame
    void Update () {

        if (SM.MagnetOn)
        {
           


              
                if (Vector3.Distance(transform.position, SM.BodyParts[0].position) < distance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, SM.BodyParts[0].position, Time.deltaTime * coinspeed);
                }



            

        


        }

        
           

     
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


       
        if (collision.gameObject.CompareTag("TAG_10"))
           {
            Destroy(gameObject);
            }

        if (collision.gameObject.CompareTag("TAG_1"))
        {
            if(!SM.MagnetOn)
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("TAG_6"))
        {
            if (!SM.MagnetOn)
                Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("TAG_8"))
        {
            if (!SM.MagnetOn)
                Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("TAG_5"))
        {
            if (!SM.MagnetOn)
                Destroy(gameObject);
        }


    }
}
