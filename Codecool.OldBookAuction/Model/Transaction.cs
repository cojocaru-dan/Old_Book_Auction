namespace Codecool.OldBookAuction.Model;

public class Transaction
{
    public readonly int Id;
    public readonly string Date;
    public readonly Bid Bid;
    public readonly Bidder Bidder;

    public Transaction(int id, Bid bid, Bidder bidder)
    {
        Id = id;
        Date = DateTime.Now.ToString();
        Bid = bid;
        Bidder = bidder;
    }

    public override string ToString()
    {
        return $"Transaction(Id: {Id}, Date: {Date}, {Bidder.Name}, {Bid})";
    }
}