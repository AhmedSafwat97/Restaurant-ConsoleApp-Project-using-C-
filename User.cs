using System;
using System.Xml;


public abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }


     public User()
    {
        Name = string.Empty;
        Email = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
        Phone = string.Empty;
    }



}
  