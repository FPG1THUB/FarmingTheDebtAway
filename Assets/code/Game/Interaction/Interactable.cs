//public interface Interactable: This defines an interface called Interactable.
public interface Interactable
{
    //void OnInteraction(): This is a method that any class implementing Interactable must define.
    void OnInteraction();
    string ToolTip();
}