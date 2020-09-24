
using UnityEngine;

public class HitBoxBehavior_25 : MonoBehaviour {

    SnakeMovement_25 SM;


   


    // Use this for initialization
    
    void Start() {
        SM = transform.GetComponentInParent<SnakeMovement_25>();
        
    }

    // Update is called once per frame
  


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("TAG_0"))
        {
            Physics2D.IgnoreCollision(transform.GetComponent<CircleCollider2D>(), collision.transform.GetComponent<CircleCollider2D>());
        }
        if (collision.transform.CompareTag("TAG_3"))
        {
            Physics2D.IgnoreCollision(transform.GetComponent<CircleCollider2D>(), collision.transform.GetComponent<CircleCollider2D>());
        }



        if (collision.transform.tag == "TAG_3" && transform == SM.BodyParts[0])
            {

            GameController_25.instance.SFX.PlayOneShot(GameController_25.instance.Clip, 1f);
                //Add a body part, will be changed to the amount of body parts it has to add
                for (int i = 0; i < collision.transform.GetComponent<FoodBehavior_25>().foodAmount; i++)
                {
                    SM.AddBodyPart();
                }

                //Destroy the food
                Destroy(collision.transform.gameObject);
            }
        



        //Also need to check if this is the first snake part
        if (collision.transform.tag == "TAG_1" && transform == SM.BodyParts[0])
        {

            

            //poweron
            if (SM.PowerOn)
            {
                //Stop the Particles
                SM.SnakeParticle.Stop();

                //Move the Particle system to the collision position
                SM.SnakeParticle.transform.position = collision.contacts[0].point;

                //Play the particles
                SM.SnakeParticle.Play();


                //Add one to the Score
                GameController_25.instance.SCORE +=new Bettr_Encryption.Encrypt(collision.transform.GetComponent<AutoDestroy_25>().life);
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
                GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + collision.transform.GetComponent<AutoDestroy_25>().life).ToString();
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

                //Diminish the text of the box
                collision.transform.GetComponent<AutoDestroy_25>().life = 0;
            }


            //Reset the parent of the text Mesh
            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            //Stop the Particles
            SM.SnakeParticle.Stop();

            //Move the Particle system to the collision position
            SM.SnakeParticle.transform.position = collision.contacts[0].point;

            //Play the particles
            SM.SnakeParticle.Play();

            //Destroy the Part of the snake that hit the box
            if(!SM.PowerOn)
            Destroy(this.gameObject);

            //Add one to the Score
            GameController_25.instance.SCORE+=new Bettr_Encryption.Encrypt(1);
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
            GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + 1).ToString();
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

            //Diminish the text of the box
            collision.transform.GetComponent<AutoDestroy_25>().life -= 1;
            collision.transform.GetComponent<AutoDestroy_25>().UpdateText();

            //change its box color
            collision.transform.GetComponent<AutoDestroy_25>().SetBoxColor();

            //Remove it from the body parts list to avoid errors

            if(!SM.PowerOn)
            SM.BodyParts.Remove(SM.BodyParts[0]);

            SM.Collide = true;

        }


        else if (collision.transform.tag == "TAG_5" && transform == SM.BodyParts[0])
        {

            
         //poweron

            if (SM.PowerOn)
            {
                //Stop the Particles
                SM.SnakeParticle.Stop();

                //Move the Particle system to the collision position
                SM.SnakeParticle.transform.position = collision.contacts[0].point;

                //Play the particles
                SM.SnakeParticle.Play();


                //Add one to the Score
                GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(collision.transform.GetComponent<AutoDestroy_25>().life);
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
                GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + collision.transform.GetComponent<AutoDestroy_25>().life).ToString();
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

                //Diminish the text of the box
                collision.transform.GetComponent<AutoDestroy_25>().life = 0;
            }

            //Stop the Particles
            SM.SnakeParticle.Stop();

            //Move the Particle system to the collision position
            SM.SnakeParticle.transform.position = collision.contacts[0].point;

            //Play the particles
            SM.SnakeParticle.Play();

            // Reset the parent of the text Mesh
            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }
            //Destroy the Part of the snake that hit the box
            if(!SM.PowerOn)
            Destroy(this.gameObject);

            //Add one to the Score
            GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(1);
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
            GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + 1).ToString();
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

            //Diminish the text of the box
            collision.transform.GetComponent<AutoDestroy_25>().life -= 1;
            collision.transform.GetComponent<AutoDestroy_25>().UpdateText();

            //change its box color
            collision.transform.GetComponent<AutoDestroy_25>().SetBoxColor();

            //Remove it from the body parts list to avoid errors
            if(!SM.PowerOn)
            SM.BodyParts.Remove(SM.BodyParts[0]);
            SM.Collide = true;
        }
        else if (collision.transform.tag == "TAG_6" && transform == SM.BodyParts[0])
        {

         
           //poweron
            if (SM.PowerOn)
            {
                //Stop the Particles
                SM.SnakeParticle.Stop();

                //Move the Particle system to the collision position
                SM.SnakeParticle.transform.position = collision.contacts[0].point;

                //Play the particles
                SM.SnakeParticle.Play();


                //Add one to the Score
                GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(collision.transform.GetComponent<AutoDestroy_25>().life);
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
                GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + collision.transform.GetComponent<AutoDestroy_25>().life).ToString();
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
                //Diminish the text of the box
                collision.transform.GetComponent<AutoDestroy_25>().life = 0;
            }

            //Reset the parent of the text Mesh
            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            //Stop the Particles
            SM.SnakeParticle.Stop();

            //Move the Particle system to the collision position
            SM.SnakeParticle.transform.position = collision.contacts[0].point;

            //Play the particles
            SM.SnakeParticle.Play();

            //Destroy the Part of the snake that hit the box
            if(!SM.PowerOn)
            Destroy(this.gameObject);

            //Add one to the Score
            GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(1);
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
            GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + 1).ToString();
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
            //Diminish the text of the box
            collision.transform.GetComponent<AutoDestroy_25>().life -= 1;
            collision.transform.GetComponent<AutoDestroy_25>().UpdateText();

            //change its box color
            collision.transform.GetComponent<AutoDestroy_25>().SetBoxColor();

            //Remove it from the body parts list to avoid errors
            if(!SM.PowerOn)
            SM.BodyParts.Remove(SM.BodyParts[0]);

            if (collision.transform.GetComponent<AutoDestroy_25>().life == 0&&SM.BodyParts.Count>0)
            {
                SM.PowerOn = true;
          
            }

            SM.Collide = true;


        }
        else if (collision.transform.tag == "TAG_8" && transform == SM.BodyParts[0])
        {

          
          //poweron
            if (SM.PowerOn)
            {
                //Stop the Particles
                SM.SnakeParticle.Stop();

                //Move the Particle system to the collision position
                SM.SnakeParticle.transform.position = collision.contacts[0].point;

                //Play the particles
                SM.SnakeParticle.Play();


                //Add one to the Score
                GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(collision.transform.GetComponent<AutoDestroy_25>().life);

                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
                GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + collision.transform.GetComponent<AutoDestroy_25>().life).ToString();
                GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

                //Diminish the text of the box
                collision.transform.GetComponent<AutoDestroy_25>().life = 0;
            }

            //Reset the parent of the text Mesh
            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            //Stop the Particles
            SM.SnakeParticle.Stop();

            //Move the Particle system to the collision position
            SM.SnakeParticle.transform.position = collision.contacts[0].point;

            //Play the particles
            SM.SnakeParticle.Play();

            //Destroy the Part of the snake that hit the box
            if(!SM.PowerOn)
            Destroy(this.gameObject);

            //Add one to the Score
            GameController_25.instance.SCORE += new Bettr_Encryption.Encrypt(1);
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);
            GameController_25.instance.Score_Encrypt = (int.Parse(GameController_25.instance.Score_Encrypt) + 1).ToString();
            GameController_25.instance.Score_Encrypt = XOREncryption.encryptDecrypt(GameController_25.instance.Score_Encrypt);

            //Diminish the text of the box
            collision.transform.GetComponent<AutoDestroy_25>().life -= 1;
            collision.transform.GetComponent<AutoDestroy_25>().UpdateText();

            //change its box color
            collision.transform.GetComponent<AutoDestroy_25>().SetBoxColor();

            //Remove it from the body parts list to avoid errors
            if(!SM.PowerOn)
            SM.BodyParts.Remove(SM.BodyParts[0]);

            if (collision.transform.GetComponent<AutoDestroy_25>().life == 0&&SM.BodyParts.Count>0)
            {
               
                    SM.MagnetOn = true;
              
            }


            SM.Collide = true;

        }





       
    }
   












}
