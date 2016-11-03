using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Card : HasId
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    [StringLength(250, MinimumLength = 10)]
    public string Content { get; set; }

    public int CardListId {get;set;}
}

public class CardList : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Summary { get; set; }
    [Required]
    public List<Card> Cards { get; set; }

    public int BoardId {get;set;}
}

public class Board : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public List<CardList> Lists { get; set; }
}

// colocate DbSet declarations with classes
public partial class DB : DbContext {
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardList> CardLists { get; set; }
    public DbSet<Board> Boards { get; set; }
}

public partial class Handler {
    public void RegisterRepos(IServiceCollection services){
        Repo<Card>.Register(services, "Cards");
        Repo<CardList>.Register(services, "CardLists",
            d => d.Include(l => l.Cards));
        Repo<Board>.Register(services, "Boards",
            d => d.Include(b => b.Lists).ThenInclude(l => l.Cards));
    }
}