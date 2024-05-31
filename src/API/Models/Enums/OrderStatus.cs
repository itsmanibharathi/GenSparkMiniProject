namespace API.Models.Enums
{
    /// <summary>
    /// Order status enum for the status of the order 
    /// <para> [create-0, place-1, preparing-2, prepared-3, pickedup-4, delivered-5, cancelled-6] </para>
    /// </summary>
    public enum OrderStatus
    {
        Create,
        Place,
        Preparing,
        Prepared,
        PickedUp,
        Delivered,
        Cancelled
    }
}
