using UnityEngine;

public class CropHandler : MonoBehaviour
{
    #region Variables
    [Space(10), Header("Managers")]
    //Reference to the inventory script
    private Inventory inventoryManager;
    //Reference to the time manager script
    private TimeManager timeManager;
    //Plot handler reference, which will retrieve the script from the plot Gameobject variable above
    public PlotHandler plotHandler;
    //GameObject Reference to the plot, which will be used to retrieve the plot handler within the prefab
    public GameObject plot;

    [Space(10), Header("GameObjects and arrays")]

    //Array to store the potato, tomato, and carrot objects
    public GameObject[] availableCrops = new GameObject[4];
    //public GameObject array to store the different carrot states
    public GameObject[] carrotStagesObjects = new GameObject[4];
    //public gameObject array to store the different potato states
    public GameObject[] potatoStagesObjects = new GameObject[4];
    //public GameObject array to store the different tomato states
    public GameObject[] tomatoStagesObjects = new GameObject[4];

    [Space(10), Header("Enums")]
    //enum to store the current carrot state
    //public CarrotState carrotState;
    //enum to store the current potato state
    //public PotatoState potatoState;
    //enum to store the current potato state
    public GrowthState growthState;
    //enum to store the currentStoredCrop
    public Crops currentCrop;


    #endregion

    #region Unity Callbacks
    //Called on the first frame
    private void Start()
    {
        //Retrieves the plot handler script from the plot game object
        plotHandler = plot.GetComponent<PlotHandler>();
        //Loops through each element available within the crops array
        for (int i = 0; i < availableCrops.Length; i++)
        {
            //retrieves the available crops
            availableCrops[i] = this.transform.GetChild(i).gameObject;

        }
        //Loops through each element available within the carrot states array
        for (int i = 0; i < carrotStagesObjects.Length; i++)
        {
            //Retrieves each state of the crop through the crops array
            carrotStagesObjects[i] = availableCrops[0].transform.GetChild(i).gameObject;
        }
        //Loops through each element available within the potato states array
        for (int i = 0; i < potatoStagesObjects.Length; i++)
        {
            //retrieves each state of the crop through the crops array
            potatoStagesObjects[i] = availableCrops[1].transform.GetChild(i).gameObject;
        }
        //Loops through each element available within the tomato states array
        for (int i = 0; i < tomatoStagesObjects.Length; i++)
        {
            //retrieves each state of the crop through the crops array
            tomatoStagesObjects[i] = availableCrops[2].transform.GetChild(i).gameObject;
        }

    }
    public void Update()
    {
        SwitchStates();
    }
    #endregion
    #region Switching states based on actions
    //function to switch the current crop based on what the current crop is, and the state of the current crop
    public void SwitchStates()
    {
        //int for the current state of the current crop
        int currentState = 0;
        //int for the current crop that is planted
        int currentSeed = 0;
        GameObject[] cropArray = new GameObject[4];
        switch (currentCrop)
        {
            case Crops.None:
                currentSeed = 3;
                cropArray = new GameObject[0];
                break;
            case Crops.carrot:
                currentSeed = 0;
                cropArray = carrotStagesObjects;
                break;
            case Crops.potato:
                currentSeed = 1;
                cropArray = potatoStagesObjects;
                break;
            case Crops.tomato:
                currentSeed = 2;
                cropArray = tomatoStagesObjects;
                break;
            default:
                currentSeed = 3;
                cropArray = new GameObject[0];
                break;
        }
      

        switch (growthState)
        {
            case GrowthState.planted:
                currentState = 0;
                break;
            case GrowthState.baby:
                currentState = 1;
                break;
            case GrowthState.teen:
                currentState = 2;
                break;
            case GrowthState.adult:
                currentState = 3;
                break;
            default:
                currentState = 0;
                break;
        }



        foreach (var cropType in availableCrops)
        {
            cropType.SetActive(false);
        }
         availableCrops[currentSeed].SetActive(true);
       // Debug.Log(availableCrops[currentSeed].name);
        if (cropArray.Length > 0)
        {
            foreach (var cropStage in cropArray)
            {
                cropStage.SetActive(false);
            }
        
            cropArray[currentState].SetActive(true);
         //   Debug.Log(cropArray[currentState].name);

        }
    }
    #endregion

}
#region enums
////an enum to store the different states of the carrot
//public enum CarrotState
//{
//    planted,
//    baby,
//    teen,
//    adult
//}
////an enum to store the different states of the potato
//public enum PotatoState
//{
//    planted,
//    baby,
//    teen,
//    adult
//}
//an enum to store the different states of the tomato
public enum GrowthState
{
    planted,
    baby,
    teen,
    adult
}
//an enum to store the different crops
public enum Crops
{
    None,
    carrot,
    potato,
    tomato
}
#endregion
