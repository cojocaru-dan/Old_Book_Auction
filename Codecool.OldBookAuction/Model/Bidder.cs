namespace Codecool.OldBookAuction.Model;

public class Bidder
{
    private static int BidId = 0;
    private int _newPrice = 10;
    private Bid? _newBid;
    public readonly List<Book> _books = new();
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
        if (_favourite == book.Topic || _interested.Contains(book.Topic)) return true;
        return false;
    }

    public bool CanBid(Book book, int currentPrice)
    {
        int threshold = GetThresholdPrice(book.Topic);
        if (Interested(book) && currentPrice < threshold) return true;
        return false;
    }

    public Bid GetBid(Book book, Bid currentBid)
    {
        int threshold = GetThresholdPrice(book.Topic);
        _newPrice = GetBidPrice(currentBid.Price, threshold);
        BidId++;
        _newBid = new Bid(BidId, this, _newPrice);
        return _newBid;
    }

    public Bid GetFirstBid(int currentPrice)
    {
        _newPrice = currentPrice;
        BidId++;
        return new Bid(BidId, this, _newPrice);
    } 
    private static int GetBidPrice(int currentPrice, int threshold) => currentPrice + (threshold - currentPrice) / 2;
    private int GetThresholdPrice(Topic topic)
    {
        if (_favourite == topic) return (int) (0.5 * _capital);
        else if (_interested.Contains(topic)) return (int) (0.25 * _capital);
        else return -1;
    }

    public void SellTo(Book book)
    {
        _capital -= _newPrice;
        _books.Add(book);
    }
}