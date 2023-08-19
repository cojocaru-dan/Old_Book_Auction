namespace Codecool.OldBookAuction.Model;

public class Bid
{
    public readonly int Id;
    public readonly Bidder Bidder;
    public readonly int Price;

    public Bid(int id, Bidder bidder, int price)
    {
        Id = id;
        Bidder = bidder;
        Price = price;
    }

    public override string ToString()
    {
        return $"Bid(Id: {Id}, Bidder: {Bidder.Name}, Price: {Price})";
    }
}