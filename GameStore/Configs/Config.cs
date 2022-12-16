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
    public int Deathamount { get; set; } = 50;

    public int Spawnamount { get; set; } = 50;
    public int Scpspawnamount { get; set; } = 50;


    public struct ItemPrice
    {
        public int Id { get; set; }
        public string Name { get; set; }
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

        new ItemPrice() { Id = 1, Price = 5, Name = "Adrenalin", Item = ItemType.Adrenaline,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 1},
        new ItemPrice() { Id = 2, Price = 5, Name = "SCP018", Item = ItemType.SCP018,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 1},
        new ItemPrice() { Id = 3, Price = 6, Name = "Painkillers", Item = ItemType.Painkillers,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 3},
        new ItemPrice() { Id = 4, Price = 8, Name = "MicroHID", Item = ItemType.MicroHID,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 3},
        new ItemPrice() { Id = 5, Price = 0, Name = "Medkit", Item = ItemType.Medkit,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 2},
        new ItemPrice() { Id = 6, Price = 9, Name = "Particle Disruptor", Item = ItemType.ParticleDisruptor,Roles = new List<RoleType> { RoleType.Scp096, RoleType.NtfCaptain }, Maxbuys = 5 , Category = 2},

    };






}
