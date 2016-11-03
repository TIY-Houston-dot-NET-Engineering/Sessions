using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;

[Route("/")]
public class HomeController : Controller
{
    private IRepository<Card> cards;
    private IRepository<CardList> lists;
    private IRepository<Board> boards;
    private ISessionService session;
    public HomeController(
        IRepository<Card> cards,
        IRepository<CardList> lists,
        IRepository<Board> boards,
        ISessionService session
    ){
        this.cards = cards;
        this.lists = lists;
        this.boards = boards;
        this.session = session;
    }

    [HttpGet("/{username?}")]
    [HttpGet("Home/Index/{username?}")]
    public IActionResult Root(string username = "you")
    {
        ViewData["Message"] = "Some extra info can be sent to the view";
        ViewData["Username"] = username;
        return View("Index"); // View(new Student) method takes an optional object as a "model", typically called a ViewModel
    }

    [HttpGet("/Account")]
    public IActionResult Account(){
        if(!session.IsLoggedIn())
            return RedirectToAction("Root");
        return View("Account");
    }

    [HttpGet("/Account/login")]
    public IActionResult Login([FromQuery] string email,[FromQuery] string pass){
        session.Login(email, pass);
        (new { email = email, pass = pass }).Log();
        return RedirectToAction("Account");
    }

    [HttpGet("/Account/register")]
    public IActionResult Register([FromQuery] string email,[FromQuery] string pass, [FromQuery] string name){

        bool success = session.Register(
            new User {Email = email, Password = pass, Name = name }
        );

        new { success = success, email = email, pass = pass, name = name }.Log();

        if(!success){
            return BadRequest(new { error = "A user with that email already exists." });
        }

        // store this id in session
        // session.IsLoggedIn()
        // (new { email = email, pass = pass, name = name }).Log();
        return RedirectToAction("Account");
    }

    // [HttpGet("sql/cards")] // ?sql=....
    // public IActionResult SqlCards([FromQuery]string sql) => Ok(cards.FromSql(sql));

    // [HttpGet("sql/lists")] // ?sql=....
    // public IActionResult SqlLists([FromQuery]string sql) => Ok(lists.FromSql(sql));

    // [HttpGet("sql/boards")] // ?sql=....
    // public IActionResult SqlBoards([FromQuery]string sql) => Ok(boards.FromSql(sql));
}