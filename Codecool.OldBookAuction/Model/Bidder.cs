namespace Codecool.OldBookAuction.Model;

public class Bidder
{
    private readonly List<Book> _books = new();
    private readonly Topic _favourite;
    private readonly Topic[] _interested;
    
    private double _capital;

    public int Id { get; }
    public string Name { get; }

    public Bidder(int id, double capital, Topic favourite, Topic[] interested)
    {
        Id = id;
        _capital = capital;
        _favourite = favourite;
        _interested = interested;
        Name = $"Bidder #{id}";
    }

    public bool Interested(Book book)
    {
    }

    public bool CanBid(Book book, int currentPrice)
    {
    }

    public Bid GetBid(Book book, Bid currentBid)
    {
    }

    private static int GetBidPrice(int currentPrice, int threshold)
    {
    }

    private int GetThresholdPrice(Topic topic)
    {
    }

    public void SellTo(Book book)
    {
    }
}