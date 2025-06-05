namespace CompositionOverInheritage.Invoices.Interfaces
{
    public interface IHasSellerInformationBehavior
    {
        public string SellerName { get; }
        public string SellerTaxNumber { get; }
    }
}
