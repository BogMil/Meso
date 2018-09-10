namespace GenericCSR
{
    public class OrderByProperties
    {
        public string OrderByColumn { get; set; }
        public string OrderDirection { get; set; } = SortDirections.Ascending;
    }
}
