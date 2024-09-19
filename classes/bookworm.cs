namespace BOOKWORM.classes
{
    public class Bookworm
    {
        public string Name { get; set; }
        public string Password { get; set; } 
        public List<BOOK.classes.Book> BorrowedBooks { get; set; } 

        public Bookworm(string name, string password)
        {
            Name = name;
            Password = password;
            BorrowedBooks = new List<BOOK.classes.Book>(); 
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}