using System;
using System.Xml;


public  class User 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}


public static class CurrentUser
{
    public static User User { get; private set; }
    public static string TypeOfUser { get; private set; }


    public static void SetCurrentUser(User user , string TypeUser )
    {
        User = user;
        TypeOfUser = TypeUser;
    }

    public static void ClearCurrentUser()
    {
        User = null;
        TypeOfUser = null;
    }
}
