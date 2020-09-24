using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_25 : MonoBehaviour
{



    private void OnCollisionEnter2D(Collision2D collision)
    { 
     

        if(collision.gameObject.CompareTag("TAG_2"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("TAG_13"))
        {
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("TAG_15"))
        {
            Destroy(collision.gameObject);
        }
    }
}

