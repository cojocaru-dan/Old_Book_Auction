namespace Codecool.OldBookAuction.Model;

public class Book
{
    public readonly int Id;
    public readonly string BookName;
    public readonly string Title;
    public readonly Topic Topic;
    public readonly int Price;

    public Book(int id, string title, Topic topic, int price)
    {
        Id = id;
        BookName = $"Book #{Id}";
        Title = title;
        Topic = topic;
        Price = price;
    }
}