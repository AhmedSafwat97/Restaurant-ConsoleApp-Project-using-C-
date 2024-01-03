using System;

sealed class Admin : User
{
    public Admin(int Id, string Email, string Password) 
    {
    }
    private Admin() { }
    private static Admin admin = null;
    private static readonly object adminlock = new object();

    public static Admin Iadmin(string Email, string Password)
    {
        if (admin == null)
        {
            lock (adminlock)
            {
                if (admin == null)
                {
                    admin = new Admin();
                }
            }
        }
        return admin;
    }
}
