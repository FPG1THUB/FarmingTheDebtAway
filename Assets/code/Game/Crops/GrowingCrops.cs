using UnityEngine;

public class GrowingCrops : MonoBehaviour
{
    #region variables
    [Header("Plant Crops")]
    //References the different crops within the game
    public GameObject carrotSeeds;
    public GameObject tomatoSeeds;
    public GameObject potatoSeeds;
    //Reference to Carrot GameObject in scene
    public GameObject carrots;
    public GameObject tomatoes;
    public GameObject potatoes;
    //References what seed you are trying to place
    public GameObject currentlyEquippedSeed;
    //Reference to the amount of carrot seeds the player has
    public int carrotSeedAmount;
    //Reference to how many tomato seeds the player has
    public int tomatoSeedAmount;
    //Reference to how many potato seeds the player has
    public int potatoSeedAmount;

    //checks to see if the plot if occupied before planting
    public bool isOccupied;

    [Header("Grow Crops")]
    //References to check if the crop has been watered for the day
    public bool isWatered;
    //Reference to check the progress of the plant that is being watered
    public float waterProgress;
    //Reference to the amount of water in the watering can
    public float waterAmount;
    //Reference to the current day the player is on
    public int day;
    //Reference to the different day amounts the crops should take to reach the next stage
    public int[] carrotProgressByDay = new int[3];
    public int[] tomatoProgressByDay = new int[3];
    public int[] potatoProgressByDay = new int[3];
    [Header("Harvest Crops")]
    //Reference to whether or not the player can harvest the crop
    public bool isHarvestable;







    #endregion
    #region functions
    /*This function will check to see if the space the player is trying to plant
    the crop in is occupied by another crop, and if it is not, checks to see if
    there is enough seeds to be able to plant the crop, and if so, plant the
    crop */
    public void PlantCrops()
    {
        //Checks to see if the player has pressed E, will change to use keybind manager once it's done
       
            //Checks to see if the seed the player is trying to use a carrot
            if (currentlyEquippedSeed == carrotSeeds)
            {
                //checks to see if the space is occupied
                if (isOccupied == true)
                {
                    //sends a log to the consolde saying it's occupied
                    Debug.Log("This plot is occupied!");
                }
                //if it is not occupied, checks to see if the player has enough seeds
                else if (carrotSeedAmount > 0)
                {
                    //displays on console that it has been placed
                    Debug.Log("Carrot Placed!");
                    carrotSeedAmount = carrotSeedAmount - 1;
                    
                }
                else
                {
                    //otherwise, console will display that there is not enough seeds
                    Debug.Log("you don't have enough seeds!");
                }
            }
            //Checks to see if the current seed is the tomato
            if (currentlyEquippedSeed == tomatoSeeds)
            {
               //checks to see if the space is occupied
                if (isOccupied == true)
                {
                    //is so, console says the plot is occupied
                    Debug.Log("This plot is occupied!");
                }
                //if unoccupied, checks to see if there is enough seeds
                else if (tomatoSeedAmount > 0)
                {
                    //displays that the tomato has been placed
                    Debug.Log("Tomato Placed!");
                    tomatoSeedAmount = tomatoSeedAmount - 1;
                new GameObject("Tomato");
                }
                else
                {
                    //otherwise, displays there isnt enough seeds
                    Debug.Log("You don't have enough seeds!");
                }
            }
            //Checks to see if the current seed is the potato
            if (currentlyEquippedSeed == potatoSeeds)
            {
                //Checks to see if space is occupied
                if (isOccupied == true)
                {
                    //displays that the space is occupied
                    Debug.Log("This plot is occupied!");
                }
                //if unoccupied, checks to see if the player has enough seeds
                else if (potatoSeedAmount > 0)
                {
                    //if so, displays that thepotato has been placed
                    Debug.Log("Potato Placed!");
                    potatoSeedAmount = potatoSeedAmount - 1;
                new GameObject("Potato");
                }
                else
                {
                    //if not, displays that there isnt enough seeds
                    Debug.Log("You don't have enough seeds!");
                }
            }

        

    }
    /* This function will check to see if the crop is watered, and if not, will
     check to see if there is any water in the watering can, and if there is water,
    will water the crop, 
      */
    public void WaterCrops()
    {

    }
    /* Checks to see if the crop has been fully watered, and if so, will add a day
     * where it has been grown, anf it will also check if a specific amount of days
     * have gone by, and if so, it will advance the crop to the next stage
      */
    public void GrowCrops()
    {

    }
    /*Checks to see if the crop can be harvested, and if so, harvests it and stores
     * it*/
    public void HarvestCrops()
    {

    }
    #endregion
    #region Unity Callbacks
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlantCrops();
        }
    }
    #endregion

}
