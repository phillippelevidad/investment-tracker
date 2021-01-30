namespace Api.Core.Domain.Transactions
{
    public class Operation : Enumeration<string>
    {
        public static readonly Operation Purchase = new Operation("purchase", nameof(Purchase), 1m);
        public static readonly Operation Sale = new Operation("sale", nameof(Sale), -1m);

        private Operation(string id, string name, decimal multiplier) : base(id, name)
        {
            Multiplier = multiplier;
        }

        public decimal Multiplier { get;}
    }
}
