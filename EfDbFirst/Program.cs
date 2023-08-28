using EfDbFirst.Models;

var _context = new SqlprojectContext();

var users = _context.Users.ToList();

foreach(var user in users)
{
    Console.WriteLine($"{user.Firstname} {user.Lastname}");
}
    
