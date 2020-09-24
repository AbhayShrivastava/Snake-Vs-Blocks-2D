
using UnityEngine;

public class FoodManager_25 : MonoBehaviour {

    [Header("Snake Manager")]
    SnakeMovement_25 SM;

    public BlocksManager_25 blocks;

    [Header("Food Variables")]
    public GameObject FoodPrefab;
    public int appearanceFrequency;

  

    [Header("Time to spawn Management")]
    public float timeBetweenFoodSpawn;
    private float thisTime;

    // Use this for initialization
    void Start () {

      
        //Set the Snake Movement
        SM = GameObject.FindGameObjectWithTag("TAG_4").GetComponent<SnakeMovement_25>();

        //Spawn the initial Food
        SpawnFood();
    }

    // Update is called once per frame
    void Update () {


        

            if (blocks.canSpawnBlock)
            {

                if (thisTime < timeBetweenFoodSpawn)
                {
                    thisTime += Time.deltaTime;
                }
                else
                {
                    SpawnFood();
                    thisTime = 0;
                }
            }
        
    }

    public void SpawnFood()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;


        for (int i = -2; i < 3; i++)
        {

            //Set the position to spawn the food
            float x = 2 * i * distBetweenBlocks;
            float y = 0;

            if (SM.transform.childCount > 0)
            {
                y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks * 2 + 10;
            }

            Vector3 spawnPos = new Vector3(x, y, 0);





            //Random Number Management
            int number;

            if (appearanceFrequency < 100)
                number = Random.Range(0, 100 - appearanceFrequency);
            else
                number = 1;



            //Actual Spawning step
            GameObject boxInstance;

            if (number == 1)
            {
                boxInstance = Instantiate(FoodPrefab) as GameObject;
                boxInstance.transform.position = spawnPos;
                boxInstance.transform.rotation = Quaternion.identity;

            }
        }
    }
}
