namespace BOOK.classes
{
    public class Book
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int Total { get; set; }

        public Book(string name, string genre, string author, int total)
        {
            Name = name;
            Genre = genre;
            Author = author;
            Total = total;
        }

        public override string ToString()
        {
            return $"{Name}, {Genre}, {Author}, {Total} ";
        }

        
    }
}