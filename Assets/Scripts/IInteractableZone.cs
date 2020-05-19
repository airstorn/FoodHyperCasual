public enum InteractableZoneArgs
{
    Remove,
    Add
}

public interface IInteractableZone
{
    bool InteractWith<T>(T interactionObject, InteractableZoneArgs args) ;
}
