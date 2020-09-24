
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager_25 : MonoBehaviour {

    [Header("Snake Manager")]
    public SnakeMovement_25 SM;
    public float distanceSnakeBarrier;

    [Header("Block Prefab")]
    public GameObject BlockPrefab;

    public bool canSpawnBlock;

    public ObjectPooling_25 BLocks;

    public ObjectPooling_25 SimpleBlocks;

   

  
  






   [Header("Time to spawn Management")]
    public float minSpawnTime;
    public float maxSpawnTime;
    private float thisTime;
    private float randomTime;

    [Header("Snake Values for Spawning")]
    public int minSpawnDist;
    Vector2 previousSnakePos;
    public List<Vector3> SimpleBoxPositions = new List<Vector3>();


    void Start () {
       BLocks = new ObjectPooling_25(BlockPrefab, 15);
        SimpleBlocks = new ObjectPooling_25(BlockPrefab, 15);
      

      


        //Initialize this time
        thisTime = 0;

        //Spawn the First Barrier
     

        //Set a random time to spawn blocks
        randomTime = Random.Range(minSpawnTime, maxSpawnTime);

	}
	
	// Update is called once per frame
	void Update () {

       
            if (thisTime < randomTime)
            {
                thisTime += Time.deltaTime;
            }
            else
            {
              
                SpawnBlock();
                thisTime = 0;
                randomTime = Random.Range(minSpawnTime, maxSpawnTime);
               
            }
            if (SM.BodyParts.Count > 0 )
            {



                if (SM.BodyParts[0].transform.position.y - previousSnakePos.y >minSpawnDist)
                {
                    SpawnBarrier();
                    
                }
            }
        
	}

    public void SpawnBarrier()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;


        for(int i = -2; i < 3; i++)
        {
            float x = 2 * i * distBetweenBlocks;
            float y = 0;

            if (SM.transform.childCount > 0)
            {
                y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks  + distanceSnakeBarrier;
                
            }

            Vector3 spawnPos = new Vector3(x, y, 0);
            GameObject boxInstance = BLocks.GetObject();

          
            int R = Random.Range(1, 20);
            if (R == 7)
            {
                boxInstance.name = "PowerBox";
                boxInstance.tag = "TAG_6";
            }
            else if (R == 6)
            {
                boxInstance.name = "MagnetBox";
                boxInstance.tag = "TAG_8";
            }
            else
            {
                boxInstance.name = "box";
                boxInstance.tag = "TAG_1";
            }



            boxInstance.GetComponent<AutoDestroy_25>().BlockUpdate();
            boxInstance.GetComponent<AutoDestroy_25>().Bars();

            boxInstance.transform.position = spawnPos;
            boxInstance.transform.rotation=Quaternion.identity;


            
                       



            //Save the Head Position
            if (SM.transform.childCount > 0)
                previousSnakePos = SM.transform.GetChild(0).position;

        }

    }

    public void SpawnBlock()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;

        int random;
        random = Random.Range(-2, 3);

        float x = 1.8f * random * distBetweenBlocks;
        float y = 0;

        if (SM.transform.childCount > 0)
        {
            y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks + distanceSnakeBarrier;

            if (Screen.height / Screen.width == 4 / 3)
                y *= 4/3;

        }


        Vector3 spawnPos = new Vector3(x, y, 0);

        //Boolean to spawn or not regarding the position
         canSpawnBlock = true;

        //If there are no positions in the list, add this one
        if (SimpleBoxPositions.Count == 0)
            SimpleBoxPositions.Add(spawnPos);
        else
        {
            //Check if the position is already used or not
            for (int k = 0; k < SimpleBoxPositions.Count; k++)
            {
                if (spawnPos == SimpleBoxPositions[k])
                    canSpawnBlock = false;
            }
        }

        GameObject boxInstance;
      

        if (canSpawnBlock)
        {
            //Add the position to the list
            SimpleBoxPositions.Add(spawnPos);


            boxInstance = SimpleBlocks.GetObject();
           
           

            int R = Random.Range(1, 20);
            if (R == 8)
            {
                boxInstance.name = "PowerBox";
                boxInstance.tag = "TAG_6";
            }
            else if (R == 5)
            {
                boxInstance.name = "MagnetBox";
                boxInstance.tag = "TAG_8";
            }

            else
            {
                boxInstance.name = "SimpleBox";
                boxInstance.tag = "TAG_5";
            }

            boxInstance.GetComponent<AutoDestroy_25>().BlockUpdate();
        
            boxInstance.transform.position = spawnPos;
         
            boxInstance.transform.rotation = Quaternion.identity;


            if (boxInstance.GetComponent<Rigidbody2D>() == null)
            {
                boxInstance.AddComponent<Rigidbody2D>();

                boxInstance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }






        }

    }

   
}
