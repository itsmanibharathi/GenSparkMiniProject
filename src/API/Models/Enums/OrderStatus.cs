namespace API.Models.Enums
{
    /// <summary>
    /// Order status enum for the status of the order 
    /// <para> [create-0, place-1, preparing-2, picked up-3, delivered-4, cancelled-5] </para>
    /// </summary>
    public enum OrderStatus
    {
        Create,
        Place,
        Preparing,
        PickedUp,
        Delivered,
        Cancelled
    }
}
