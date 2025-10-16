// Will contain default keys for developers to use if the save function stops between scenes/dealing with development issues.
public class DefaultControls
{
    // List of actions the players can do, and will match the keybinding UI
    public static string[] keyNames = new string[] 
    {
        "Forward",   // Move forward
        "Backward",  // Move backward
        "Left",      // Move left
        "Right",     // Move right
        "Jump",      // Jump action
        "Sprint",    // Sprint or run
        "Crouch",    // Crouch or sneak
        "Interact"   // Interact with objects or NPCs
    };

    // These are the actual keys assigned to those actions.
    public static string[] keyValues = new string[]
    {
        "W",             // Forward
        "S",             // Backward
        "A",             // Left
        "D",             // Right
        "Space",         // Jump
        "LeftShift",     // Sprint
        "LeftControl",   // Crouch
        "E"              // Interact
    };
}
