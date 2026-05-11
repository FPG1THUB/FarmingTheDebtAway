/// <summary>
/// Interface to make a script interactable, allowing the player to interact and perform actions attached to the functions.
/// If it is giving an error when implementing it at first, right click, go to quick actions, and add in the functions.
/// You can attach this interfact to classes through adding a colum next to the monobehaviour statement, and putting in Interactable
/// </summary>
public interface Interactable 
{
    /// <summary>
    /// Base function to perform actions based on the actions states within the inherited interactable script
    /// </summary>
    void OnInteraction();
    /// <summary>
    /// String function to state what to display to the user in the form of a tool tip
    /// </summary>
    /// <returns></returns>
    string ToolTip();
}