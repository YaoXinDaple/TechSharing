namespace BuilderPattern
{
    public class Address
    {
        internal Address()
        {
        }
        public string Street { get; init; } = string.Empty;
        public string Detail { get; init; } = string.Empty;
        public string Province { get; init; } = string.Empty;
        public string ZipCode { get; init; } = string.Empty;
    }
}
