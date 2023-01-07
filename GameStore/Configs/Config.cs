using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using InventorySystem.Items.Usables.Scp330;
using PlayerRoles;

[Serializable]
public class Config : IConfig
{
    public bool Debug { get; set; } = false;


    [Description("The amount a player gets from each event. 0 disables the event")]
    public int Escapeamount { get; set; } = 1000;

    public int Escapecufferamount { get; set; } = 1000;

    public int Killamount { get; set; } = 50;

    public int Scpkillamount { get; set; } = 200;

    public int Deathamount { get; set; } = 50;

    public int Spawnamount { get; set; } = 50;
    public int Scpspawnamount { get; set; } = 50;


    public List<Category> Categorys { get; set; } = new()
    {
        new Category
            {
                Name = "D-Klasse", Description = "Hier kannst du Gegenstande für D-Klassen kaufen." },
        new Category
        {
            Name = "Wissenschaftler",
            Description = "Hier kannst du Gegenstande für Wissenschaftler kaufen."
        },
        new Category
        {
            Name = "Sicherheitspersonal",
            Description = "Hier kannst du Gegenstande für Sicherheitspersonal kaufen."
        },
        new Category { Name = "MTF", Description = "Hier kannst du Gegenstande für das MTF kaufen." },
        new Category
            {
                Name = "Chaos", Description = "Hier kannst du Gegenstande für Chaos insurgency kaufen." },
        new Category { Name = "Allgemein", Description = "Hier findest du allgemeine Sachen." },
        new Category { Name = "Munition", Description = "Hier findest du Munition" },
        new Category { Name = "Spezial", Description = " Hier findest du spezielle Sachen." }
    };

    public List<ItemPrice> Items { get; set; } = new()
    {
        new ItemPrice
        {
            Id = 1,
            Price = 2000,
            Name = "Hausmeisterkarte",
            ItemTypes = new List<ItemType> { ItemType.KeycardJanitor },
            Roles = new List<RoleTypeId> { RoleTypeId.ClassD },
            Maxbuys = 1,
            CategoryNum = 1
        },

        new ItemPrice
        {
            Id = 2,
            Price = 4000,
            Name = "Wissenschaftlerkarte",
            ItemTypes = new List<ItemType> { ItemType.KeycardScientist },
            Roles = new List<RoleTypeId> { RoleTypeId.ClassD },
            Maxbuys = 1,
            CategoryNum = 1
        },

        new ItemPrice
        {
            Id = 3,
            Price = 600,
            Name = "Schmerzmittel",
            ItemTypes = new List<ItemType> { ItemType.Painkillers },
            Roles = new List<RoleTypeId> { RoleTypeId.ClassD },
            Maxbuys = 2,
            CategoryNum = 1
        },


        new ItemPrice
        {
            Id = 4,
            Price = 4000,
            Name = "Hauptwissenschaftlerkarte",
            ItemTypes = new List<ItemType> { ItemType.KeycardResearchCoordinator },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 1,
            CategoryNum = 2
        },

        new ItemPrice
        {
            Id = 5,
            Price = 5000,
            Name = "Zonenmanagerkarte",
            ItemTypes = new List<ItemType> { ItemType.KeycardZoneManager },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 1,
            CategoryNum = 2
        },

        new ItemPrice
        {
            Id = 6,
            Price = 500,
            Name = "Radio",
            ItemTypes = new List<ItemType> { ItemType.Radio },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 2,
            CategoryNum = 2
        },
        new ItemPrice
        {
            Id = 7,
            Price = 2000,
            Name = "SCP-500",
            ItemTypes = new List<ItemType> { ItemType.SCP500 },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 2,
            CategoryNum = 2
        },

        new ItemPrice
        {
            Id = 8,
            Price = 400,
            Name = "Schmerzmittel",
            ItemTypes = new List<ItemType> { ItemType.Painkillers },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 2,
            CategoryNum = 2
        },
        new ItemPrice
        {
            Id = 9,
            Price = 5000,
            Name = "SCP-018",
            ItemTypes = new List<ItemType> { ItemType.SCP018 },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 2,
            CategoryNum = 2
        },
        new ItemPrice
        {
            Id = 10,
            Price = 7000,
            Name = "SCP-268",
            ItemTypes = new List<ItemType> { ItemType.SCP268 },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 1,
            CategoryNum = 2
        },
        new ItemPrice
        {
            Id = 11,
            Price = 3500,
            Name = "Leichte Brustpanzerung",
            ItemTypes = new List<ItemType> { ItemType.ArmorLight },
            Roles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Maxbuys = 5,
            CategoryNum = 2
        },


        new ItemPrice
        {
            Id = 12,
            Price = 3000,
            Name = "Kadettenkarte",
            ItemTypes = new List<ItemType> { ItemType.KeycardNTFOfficer },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 1,
            CategoryNum = 3
        },


        new ItemPrice
        {
            Id = 13,
            Price = 4000,
            Name = "Crossvec",
            ItemTypes = new List<ItemType> { ItemType.GunCrossvec },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 1,
            CategoryNum = 3
        },


        new ItemPrice
        {
            Id = 14,
            Price = 1000,
            Name = "Granate",
            ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 2,
            CategoryNum = 3
        },

        new ItemPrice
        {
            Id = 15,
            Price = 1000,
            Name = "Flash Grenade",
            ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 2,
            CategoryNum = 3
        },

        new ItemPrice
        {
            Id = 16,
            Price = 4500,
            Name = "Schwere Brustpanzerung",
            ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 1,
            CategoryNum = 3
        },


        new ItemPrice
        {
            Id = 17,
            Price = 500,
            Name = "Schmerzmittel",
            ItemTypes = new List<ItemType> { ItemType.Painkillers },
            Roles = new List<RoleTypeId> { RoleTypeId.FacilityGuard },
            Maxbuys = 2,
            CategoryNum = 3
        },


        new ItemPrice
        {
            Id = 18,
            Price = 7000,
            Name = "Facilitymanager Karte",
            ItemTypes = new List<ItemType> { ItemType.KeycardFacilityManager },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 1,
            CategoryNum = 4
        },


        new ItemPrice
        {
            Id = 19,
            Price = 2500,
            Name = "MTF-E11-SR",
            ItemTypes = new List<ItemType> { ItemType.GunE11SR },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 1,
            CategoryNum = 4
        },

        new ItemPrice
        {
            Id = 20,
            Price = 3500,
            Name = "Schwere Brustpanzerung",
            ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 1,
            CategoryNum = 4
        },

        new ItemPrice
        {
            Id = 21,
            Price = 1500,
            Name = "SCP-500",
            ItemTypes = new List<ItemType> { ItemType.SCP500 },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 2,
            CategoryNum = 4
        },

        new ItemPrice
        {
            Id = 22,
            Price = 500,
            Name = "Schmerzmittel",
            ItemTypes = new List<ItemType> { ItemType.Painkillers },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 2,
            CategoryNum = 4
        },

        new ItemPrice
        {
            Id = 23,
            Price = 1000,
            Name = "Granate",
            ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 2,
            CategoryNum = 4
        },


        new ItemPrice
        {
            Id = 24,
            Price = 750,
            Name = "Flash Granate",
            ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 2,
            CategoryNum = 4
        },

        new ItemPrice
        {
            Id = 25,
            Price = 50000,
            Name = "X3-Particle-Disruptor",
            ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
            Roles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Maxbuys = 1,
            CategoryNum = 4
        },


        new ItemPrice
        {
            Id = 26,
            Price = 7000,
            Name = "Facilitymanager Karte",
            ItemTypes = new List<ItemType> { ItemType.KeycardFacilityManager },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 1,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 27,
            Price = 7000,
            Name = "Logicer",
            ItemTypes = new List<ItemType> { ItemType.GunLogicer },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 1,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 28,
            Price = 2500,
            Name = "Shotgun",
            ItemTypes = new List<ItemType> { ItemType.GunShotgun },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 1,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 29,
            Price = 3500,
            Name = "Schwere Brustpanzerung",
            ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 1,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 30,
            Price = 500,
            Name = "Schmerzmittel",
            ItemTypes = new List<ItemType> { ItemType.Painkillers },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 2,
            CategoryNum = 5
        },

        new ItemPrice
        {
            Id = 31,
            Price = 1000,
            Name = "Granate",
            ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 2,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 32,
            Price = 750,
            Name = "Flash Granate",
            ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 2,
            CategoryNum = 5
            
        },

        new ItemPrice
        {
            Id = 33,
            Price = 50000,
            Name = "X3-Particle-Disruptor",
            ItemTypes = new List<ItemType> { ItemType.ParticleDisruptor },
            Roles = new List<RoleTypeId>
                { RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman },
            Maxbuys = 1,
            CategoryNum = 5
        },


        new ItemPrice
        {
            Id = 34,
            Price = 300,
            Name = "Coin",
            ItemTypes = new List<ItemType> { ItemType.Coin },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 5,
            CategoryNum = 6
        },

        new ItemPrice
        {
            Id = 35,
            Price = 350,
            Name = "Flashlight",
            ItemTypes = new List<ItemType> { ItemType.Flashlight },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 5,
            CategoryNum = 6
        },


        new ItemPrice
        {
            Id = 36,
            Price = 1000,
            Name = "SCP-207",
            ItemTypes = new List<ItemType> { ItemType.SCP207 },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 6
        },


        new ItemPrice
        {
            Id = 37,
            Price = 450,
            Name = "Adrenalin",
            ItemTypes = new List<ItemType> { ItemType.Adrenaline },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 6
        },

        new ItemPrice
        {
            Id = 38,
            Price = 500,
            Name = "Medkit",
            ItemTypes = new List<ItemType> { ItemType.Medkit },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 3,
            CategoryNum = 6
        },


        new ItemPrice
        {
            Id = 39,
            Price = 0,
            Name = "Kostenlose Munition",
            AmmoTypes = new Dictionary<AmmoType, ushort>
            {
                { AmmoType.Nato762, 60 }, { AmmoType.Ammo44Cal, 12 }, { AmmoType.Nato556, 60 },
                { AmmoType.Ammo12Gauge, 12 }, { AmmoType.Nato9, 60 }
            },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 1,
            CategoryNum = 7,
            IsAmmo = true
        },

        new ItemPrice
        {
            Id = 40,
            Price = 75,
            Name = "9x19mm Munition",
            AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Nato9, 60 } },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 7,
            IsAmmo = true
        },
        new ItemPrice
        {
            Id = 41,
            Price = 75,
            Name = "12/70 Buckshot",
            AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Ammo12Gauge, 60 } },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 7,
            IsAmmo = true
        },
        new ItemPrice
        {
            Id = 42,
            Price = 75,
            Name = ".44 Mag",
            AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Ammo44Cal, 60 } },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 7,
            IsAmmo = true
        },
        new ItemPrice
        {
            Id = 43,
            Price = 75,
            Name = "9x19mm Munition",
            AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Nato9, 60 } },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 7,
            IsAmmo = true
        },
        new ItemPrice
        {
            Id = 44,
            Price = 75,
            Name = "7.62x39mm Munition",
            AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Nato762, 60 } },
            Roles = new List<RoleTypeId> { RoleTypeId.None },
            Maxbuys = 2,
            CategoryNum = 7,
            IsAmmo = true
        },
        
        
    };

    [Description("Enables the Plugin")] public bool IsEnabled { get; set; } = true;


    public struct ItemPrice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ItemType> ItemTypes { get; set; }
        public bool IsAmmo { get; set; }
        public Dictionary<AmmoType, ushort> AmmoTypes { get; set; }

        public int Price { get; set; }
        public List<RoleTypeId> Roles { get; set; }

        public int Maxbuys { get; set; }

        public int CategoryNum { get; set; }
    }

    public struct Category
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

public class Builders
{
    public static string CategoryBuilder()
    {
        var category = "";
        var i = 1;
        foreach (var categoryitem in Plugin.Instance.Config.Categorys)
        {
            category += $"\n[{i}] " + categoryitem.Name + "\n        " + categoryitem.Description;
            i++;
        }

        return category;
    }

    public static string ItemListBuilder(int category)
    {
        var items = "";
        var i = 1;
        foreach (var item in Plugin.Instance.Config.Items.Where(item => category == item.CategoryNum))
        {
            items += $"\n[{i}] {item.Name} - {item.Price} {Plugin.Instance.Translation.Currencyname}";
            i++;
        }

        return items;
    }
}