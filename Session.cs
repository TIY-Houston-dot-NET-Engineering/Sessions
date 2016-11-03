using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

public class User {
    public string Email {get;set;}
    public string Password {get;set;}
    public string Name {get;set;}
}

public interface ISessionService {
    bool IsLoggedIn();
    bool Login(string email, string password);
    bool Register(User user);
    bool Logout();
    void SetSession(ISession s);
}

public class SessionService : ISessionService {
    private List<User> accounts = new List<User>();
    private ISession session;

    public void SetSession(ISession s){
        session = s;
    }

    public bool IsLoggedIn(){
        var v = session.GetString("HasLoggedIn");
        return v != null;
    }

    public bool Login(string email, string password){
        if(accounts.Any(u => u.Email == email && u.Password == password)){
            session.SetString("HasLoggedIn", true.ToString());
            return true;
        }

        return false;
    }

    public bool Register(User user){
        if(accounts.Any(u => user.Email == u.Email))
            return false;

        accounts.Add(user);
        session.SetString("HasLoggedIn", true.ToString());
        return true;
    }

    public bool Logout(){
        session.Clear();
        return false;
    }
}