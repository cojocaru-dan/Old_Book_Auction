using Codecool.OldBookAuction.Model;

namespace Codecool.OldBookAuction;

internal class Program
{
    private static Random rand = new Random();
    public static void Main(string[] args)
    {
        var transactions = new List<Transaction>();
        const int bookCount = 20;
        const int minPrice = 200;
        const int maxPrice = 500;
        
        const int bidderCount = 10;
        const int minimumCapital = 250;
        const int maximumCapital = 1400;

        var books = new List<Book>();
        GenerateBooks(bookCount, books, minPrice, maxPrice);
        
        var bidders = new List<Bidder>();
        GenerateBidders(bidderCount, bidders, minimumCapital, maximumCapital);
        
        StartAuctionProcess(books, bidders, transactions);
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction + "\n");
        }
        Console.WriteLine($"{transactions.Count} where sold out of {bookCount} books.");
    }

    private static void GenerateBooks(int booksNumber, List<Book> books, int minPrice, int maxPrice)
    {
        for (int i = 0; i < booksNumber; i++)
        {
            string bookTitle = GetBookTitle();
            Topic bookTopic = GetBookTopic();
            int bookPrice = GetBookPrice(minPrice, maxPrice);
            books.Add(new Book(i, bookTitle, bookTopic, bookPrice));
        }
    }

    private static string GetBookTitle()
    {   
        string[] bookTitles = 
        { 
            "The Great Gatsby", "To Kill a Mockingbird", "The Chronicles of Narnia", "Pride and Prejudice", "The Catcher in the Rye",
            "The Lord of the Rings", "Harry Potter and the Sorcerer's Stone", "Fahrenheit 451", "Brave New World", "The Hobbit",
            "The Hunger Games", "The Da Vinci Code", "The Alchemist", "The Kite Runner", "Gone with the Wind",
            "The Shining", "Wuthering Heights", "Moby-Dick", "Jane Eyre", "The Hitchhiker's Guide to the Galaxy"
        };
        return bookTitles[rand.Next(bookTitles.Length)];
    }

    private static Topic GetBookTopic() 
    {
        var topicsArray = new List<Topic>();
        foreach (var topic in Enum.GetValues<Topic>())
        {
            topicsArray.Add(topic);
        }
        return topicsArray[rand.Next(topicsArray.Count)];
    }

    private static int GetBookPrice(int min, int max) => rand.Next(min, max + 1);
    private static void GenerateBidders(int biddersNumber,List<Bidder> bidders, int minimumCapital, int maximumCapital)
    {
        for (int i = 0; i < biddersNumber; i++)
        {
            double bidderCapital = GetBidderCapital(minimumCapital, maximumCapital);
            Topic bidderFavTopic = GetBidderFavTopic();
            Topic[] bidderInterestedTopics = GetBidderInterestedTopics();
            bidders.Add(new Bidder(i, bidderCapital, bidderFavTopic, bidderInterestedTopics));
        }
    }

    private static double GetBidderCapital(int minimumCapital, int maximumCapital) => minimumCapital + rand.NextDouble() * (maximumCapital - minimumCapital);
    private static Topic GetBidderFavTopic()
    {
        var topicsArray = new List<Topic>();
        foreach (var topic in Enum.GetValues<Topic>())
        {
            topicsArray.Add(topic);
        }
        return topicsArray[rand.Next(topicsArray.Count)];
    }
    private static Topic[] GetBidderInterestedTopics()
    {
        Topic[] interestedTopics = new Topic[2];
        var topicsArray = new List<Topic>();
        foreach (var topic in Enum.GetValues<Topic>())
        {
            topicsArray.Add(topic);
        }
        for (int i = 0; i < interestedTopics.Length; i++)
        {
            interestedTopics[i] = topicsArray[rand.Next(topicsArray.Count)];
        }
        return interestedTopics;
    }

    private static void StartAuctionProcess(List<Book> books, List<Bidder> bidders, List<Transaction> transactions)
    {   
        int transactionId = 0;
        foreach (var book in books)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"{book.BookName} [{book.Topic}] selling for {book.Price} ({book.Id + 1}/20)");
            Bidder? currentBidder = null;
            Bid? currentBid = null;
            int currentPrice = book.Price;
            bool bookSold = false;
            bool biddersInAuction = true;
            
            while (!bookSold && biddersInAuction)
            {
                Thread.Sleep(2000);
                var interestedAbleToPayBidders = GetInterestedAbleToPayBidders(book, bidders, currentPrice);
                Console.WriteLine($"\t{interestedAbleToPayBidders.Count} bidders are interested in this book at the current price of {currentPrice}.");
                if (interestedAbleToPayBidders.Count == 0)
                {
                    NotBiddersInAuction(ref biddersInAuction, book);
                } 
                else if (interestedAbleToPayBidders.Count == 1)
                {
                    OneBidderWinBook(ref bookSold, ref currentBidder, ref currentBid, interestedAbleToPayBidders, ref currentPrice, ref transactionId, transactions, book);
                } 
                else
                {
                    OneBidderPlaceABid(ref currentBidder, ref currentBid, interestedAbleToPayBidders, ref currentPrice, book);
                }

            }
            Console.WriteLine("------------");
        }
    }
    private static List<Bidder> GetInterestedAbleToPayBidders(Book book, List<Bidder> bidders, int currentPrice)
    {
        var interestedAbleToPayBidders = new List<Bidder>();
        foreach (var bidder in bidders)
        {
            if (bidder.Interested(book) && bidder.CanBid(book, currentPrice))
            {
                interestedAbleToPayBidders.Add(bidder);
            }
        }
        return interestedAbleToPayBidders;   
    }
    private static Bidder PickBidder(List<Bidder> bidders,Bidder? lastBidder)
    {
        Bidder randomBidder = bidders[rand.Next(bidders.Count)];
        while (randomBidder == lastBidder)
        {
            randomBidder = bidders[rand.Next(bidders.Count)];
        }
        return randomBidder;
    }

    private static void NotBiddersInAuction(ref bool biddersInAuction, Book book)
    {
        biddersInAuction = false;
        Console.WriteLine($"\t{book.BookName} [{book.Topic}] cannot be sold. Proceeding with next book.");
    }

    private static void OneBidderWinBook(ref bool bookSold, ref Bidder? currentBidder, ref Bid? currentBid, List<Bidder> interestedAbleToPayBidders, ref int currentPrice, ref int transactionId, List<Transaction> transactions, Book book)
    {
        bookSold = true;
        currentBidder ??= interestedAbleToPayBidders[0];
        currentBid ??= currentBidder.GetFirstBid(currentPrice);
        Transaction newTransaction = new Transaction(++transactionId, currentBid, currentBidder);
        transactions.Add(newTransaction);
        currentBidder?.SellTo(book);
        Console.WriteLine($"\t{book.BookName} sold to {currentBidder?.Name} for {currentBid?.Price} $ on {newTransaction.Date}");
    }

    private static void OneBidderPlaceABid(ref Bidder? currentBidder, ref Bid? currentBid, List<Bidder> interestedAbleToPayBidders, ref int currentPrice, Book book)
    {
        currentBidder = PickBidder(interestedAbleToPayBidders, currentBidder);
        currentBid = currentBid == null ? currentBidder.GetFirstBid(currentPrice) : currentBidder.GetBid(book, currentBid);
        currentPrice = currentBid.Price;
        Console.WriteLine($"\t{currentBidder.Name} bids {currentBid.Price}");
    }
}