//public interface Interactable: This defines an interface called Interactable.
public interface Interactable // Attach to script to make it Interactable.
{
    // In the script being added, right click Interactable, and click "Quick Actions and Refactoring" then implementing

    //void OnInteraction(): This is a method that any class implementing Interactable must define.
    void OnInteraction(); // if need an example, look at TestInteract to see it.
    string ToolTip();
}