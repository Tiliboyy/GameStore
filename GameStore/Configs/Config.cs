using Discord;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Config : IConfig
{

    [Description("Enables the Plugin")]
    public bool IsEnabled { get; set; } = true;

    public bool Debug { get; set; } = false;


    [Description("The amount a player gets from each event. 0 disables the event")]
    public int Escapeamount { get; set; } = 1000;
    public int Escapecufferamount { get; set; } = 1000;

    public int Killamount { get; set; } = 50;

    public int Scpkillamount { get; set; } = 200;

    public int Deathamount { get; set; } = 50;

    public int Spawnamount { get; set; } = 50;
    public int Scpspawnamount { get; set; } = 50;


    public struct ItemPrice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ItemType Item { get; set; }
        public int Price { get; set; }

        public List<RoleType> Roles { get; set; }

        public int Maxbuys { get; set; }

        public int Category { get; set; }


    }

    public struct Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }


    public List<Category> Categorys { get; set; } = new List<Category>()
    {
        new Category() { Id = 1, Name = "[1] - D-Klasse", Description = "        test1"},
        new Category() { Id = 2, Name = "[2] - Wissenschaftler", Description = "        test2"},
        new Category() { Id = 3, Name = "[3] - Sicherheitspersonal", Description = "        test3"},
        new Category() { Id = 4, Name = "[4] - MTF", Description =  "       test4"},
        new Category() { Id = 5, Name = "[5] - Chaos", Description = "        test5"},
        new Category() { Id = 6, Name = "[6] - Allgemein", Description =  "        test6"},
        new Category() { Id = 7, Name = "[7] - Munition", Description = "        test7"},
        new Category() { Id = 8, Name = "[8] - Spezial", Description =  "       test8"}
    };
    public List<ItemPrice> Items { get; set; } = new List<ItemPrice>()
    {

        new ItemPrice() { 
            Id = 1, 
            Price = 2000,
            Name = "Hausmeisterkarte",
            Description = "Eine Hausmeisterkarte", 
            Item = ItemType.KeycardJanitor,
            Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, 
            Maxbuys = 5 , 
            Category = 1
        },

        new ItemPrice() {
            Id = 2,
            Price = 4000,
            Name = "Wissenschaftlerkarte",
            Description = "Eine Wissenschaftlerkarte",
            Item = ItemType.KeycardScientist,
            Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain },
            Maxbuys = 5 ,
            Category = 1
        },

        new ItemPrice() { 
            Id = 3, 
            Price = 600, 
            Name = "Schmerzmittel",
            Description = "Schmerzmittel, scheint aber verückte Effekte zu haben!", 
            Item = ItemType.Painkillers,
            Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, 
            Maxbuys = 5 , 
            Category = 1
        },



        new ItemPrice() { 
            Id = 4, 
            Price = 4000, 
            Name = "Hauptwissenschaftlerkarte",
            Description = "Eine Hauptwissenschaftlerkarte", 
            Item = ItemType.KeycardResearchCoordinator,
            Roles = new List<RoleType> { RoleType.Scientist }, 
            Maxbuys = 5 , 
            Category = 2
        },

        new ItemPrice() { 
            Id = 5, 
            Price = 5000, 
            Name = "Zonenmanagerkarte", 
            Description = "Eine Zonenmanagerkarte",
            Item = ItemType.KeycardZoneManager,
            Roles = new List<RoleType> { RoleType.Scientist }, 
            Maxbuys = 5 , 
            Category = 2
        },

        new ItemPrice() { 
            Id = 6, 
            Price = 500, 
            Name = "Radio", 
            Description = "Ein Radio für deine Kommunikation", 
            Item = ItemType.Radio,
            Roles = new List<RoleType> { RoleType.Scientist }, 
            Maxbuys = 5 , 
            Category = 2
        },
        new ItemPrice() {
            Id = 7,
            Price = 2000,
            Name = "SCP-500",
            Description = "Ein Exemplar von SCP-500, stärker als ein Medkit.",
            Item = ItemType.SCP500,
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5 ,
            Category = 2
        },

        new ItemPrice() {
            Id = 8,
            Price = 400,
            Name = "Schmerzmittel",
            Description = "Schmerzmittel, scheint aber verückte Effekte zu haben!",
            Item = ItemType.Painkillers,
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5 ,
            Category = 2
        },
        new ItemPrice() {
            Id = 9,
            Price = 5000,
            Name = "SCP-018",
            Description = "SCP-018, Völkerball aber Brutaler.",
            Item = ItemType.SCP018,
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5 ,
            Category = 2
        },
        new ItemPrice() {
            Id = 10,
            Price = 7000,
            Name = "SCP-268",
            Description = "SCP-268, werd zum Vater und geh Milch holen.",
            Item = ItemType.SCP268,
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5 ,
            Category = 2
        },
        new ItemPrice() {
            Id = 11,
            Price = 3500,
            Name = "Leichte Brustpanzerung ",
            Description = "Ist nicht viel, reicht aber gegen den Gürtel von Vater.",
            Item = ItemType.ArmorLight,
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5 ,
            Category = 2
        },
    };


}
public class Builders
{
    public static string CategoryBuilder()
    {
        string category = "";
        int i = 1;
        foreach (var categoryitem in Plugin.Instance.Config.Categorys)
        {

            category += $"\n[{i}] " + categoryitem.Name + "\n" + categoryitem.Description;
            i++;
        }
        return category;
    }
    public static string ItemListBuilder(int category)
    {
        string items = "";
        int i = 1;
        foreach (var item in Plugin.Instance.Config.Items)
        {
            if (category == item.Category)
            {
                items += $"\n[{i}] {item.Name} - {item.Price} {Plugin.Instance.Translation.Currencyname}";
                i++;
            }
        }
        return items;
    }
}