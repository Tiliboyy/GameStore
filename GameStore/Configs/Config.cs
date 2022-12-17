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

        public List<ItemType> ItemTypes { get; set; }
        public List<AmmoType> AmmoTypes { get; set; }

        public int Price { get; set; }
        public bool IsAmmo { get; set; }

        public ushort AmmoAmount { get; set; }
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
        new Category() { Id = 1, Name = "D-Klasse", Description = "        Hier kannst du Gegenstande für D-Klassen kaufen."},
        new Category() { Id = 2, Name = "Wissenschaftler", Description = "        Hier kannst du Gegenstande für Wissenschaftler kaufen."},
        new Category() { Id = 3, Name = "Sicherheitspersonal", Description = "        Hier kannst du Gegenstande für Sicherheitspersonal kaufen."},
        new Category() { Id = 4, Name = "MTF", Description =  "       Hier kannst du Gegenstande für das MTF kaufen."},
        new Category() { Id = 5, Name = "Chaos", Description = "        Hier kannst du Gegenstande für Chaos insurgency kaufen."},
        new Category() { Id = 6, Name = "Allgemein", Description =  "        Hier findest du allgemeine Sachen."},
        new Category() { Id = 7, Name = "Munition", Description = "        Hier findest du Munition"},
        new Category() { Id = 8, Name = "Spezial", Description =  "       Hier findest du spezielle Sachen."}
    };
    public List<ItemPrice> Items { get; set; } = new List<ItemPrice>()
    {



        new ItemPrice() {
            Id = 1,
            Price = 2000,
            Name = "Hausmeisterkarte",
            Description = "Eine Hausmeisterkarte",
            ItemTypes = new List<ItemType> {ItemType.KeycardJanitor },
            Roles = new List<RoleType> { RoleType.ClassD },
            Maxbuys = 5,
            Category = 1
        },

        new ItemPrice() {
            Id = 2,
            Price = 4000,
            Name = "Wissenschaftlerkarte",
            Description = "Eine Wissenschaftlerkarte",
            ItemTypes = new List<ItemType> {ItemType.KeycardScientist},
            Roles = new List<RoleType> { RoleType.ClassD },
            Maxbuys = 5,
            Category = 1
        },

        new ItemPrice() {
            Id = 3,
            Price = 600,
            Name = "Schmerzmittel",
            Description = "Schmerzmittel, scheint aber verückte Effekte zu haben!",
            ItemTypes = new List<ItemType> {ItemType.Painkillers},
            Roles = new List<RoleType> { RoleType.ClassD },
            Maxbuys = 5,
            Category = 1
        },



        new ItemPrice() {
            Id = 4,
            Price = 4000,
            Name = "Hauptwissenschaftlerkarte",
            Description = "Eine Hauptwissenschaftlerkarte",
            ItemTypes = new List<ItemType> {ItemType.KeycardResearchCoordinator},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },

        new ItemPrice() {
            Id = 5,
            Price = 5000,
            Name = "Zonenmanagerkarte",
            Description = "Eine Zonenmanagerkarte",
            ItemTypes = new List<ItemType> {ItemType.KeycardZoneManager},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },

        new ItemPrice() {
            Id = 6,
            Price = 500,
            Name = "Radio",
            Description = "Ein Radio für deine Kommunikation",
            ItemTypes = new List<ItemType> {ItemType.Radio},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },
        new ItemPrice() {
            Id = 7,
            Price = 2000,
            Name = "SCP-500",
            Description = "Ein Exemplar von SCP-500, stärker als ein Medkit.",
            ItemTypes = new List<ItemType> {ItemType.SCP500},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },

        new ItemPrice() {
            Id = 8,
            Price = 400,
            Name = "Schmerzmittel",
            Description = "Schmerzmittel, scheint aber verückte Effekte zu haben!",
            ItemTypes = new List<ItemType> {ItemType.Painkillers},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },
        new ItemPrice() {
            Id = 9,
            Price = 5000,
            Name = "SCP-018",
            Description = "SCP-018, Völkerball aber Brutaler.",
            ItemTypes = new List<ItemType> {ItemType.SCP018},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },
        new ItemPrice() {
            Id = 10,
            Price = 7000,
            Name = "SCP-268",
            Description = "SCP-268, werd zum Vater und geh Milch holen.",
            ItemTypes = new List<ItemType> {ItemType.SCP268},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },
        new ItemPrice() {
            Id = 11,
            Price = 3500,
            Name = "Leichte Brustpanzerung ",
            Description = "Ist nicht viel, reicht aber gegen den Gürtel von Vater.",
            ItemTypes = new List<ItemType> {ItemType.ArmorLight},
            Roles = new List<RoleType> { RoleType.Scientist },
            Maxbuys = 5,
            Category = 2
        },


        new ItemPrice() {
            Id = 12,
            Price = 3000,
            Name = "Kadettenkarte ",
            Description = "Eine Kadettkarte. Es ist nicht viel, aber du wirst eh in Heavy sterben.",
            ItemTypes = new List<ItemType> {ItemType.KeycardNTFOfficer},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },



        new ItemPrice() {
            Id = 13,
            Price = 4000,
            Name = "Crossvec ",
            Description = "My weapon go Brrrrrr skrr skrr skrrrrrr",
            ItemTypes = new List<ItemType> {ItemType.GunCrossvec},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },


        new ItemPrice() {
            Id = 14,
            Price = 1000,
            Name = "Granate",
            Description = "Ich bin in Gefahr! - 106",
            ItemTypes = new List<ItemType> {ItemType.GrenadeHE},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },

        new ItemPrice() {
            Id = 15,
            Price = 1000,
            Name = "Flash Grenade",
            Description = "Blende deine Gegner",
            ItemTypes = new List<ItemType> {ItemType.GrenadeFlash},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },

        new ItemPrice() {
            Id = 16,
            Price = 4500,
            Name = "Schwere Brustpanzerung",
            Description = "Werde zum average DayLight Moderator",
            ItemTypes = new List<ItemType> {ItemType.ArmorHeavy},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },


        new ItemPrice() {
            Id = 17,
            Price = 500,
            Name = "Schmerzmittel",
            Description = "Schmerzmittel, scheint aber verückte Effekte zu haben!",
            ItemTypes = new List<ItemType> {ItemType.Painkillers},
            Roles = new List<RoleType> { RoleType.FacilityGuard },
            Maxbuys = 5,
            Category = 3
        },


        new ItemPrice() {
            Id = 18,
            Price = 7000,
            Name = "Facilitymanager Karte",
            Description = "Eine Facilitymanager Karte. Mach ruig die nuke\r\nan, die SCP's machen die eh aus.",
            ItemTypes = new List<ItemType> {ItemType.KeycardFacilityManager},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },


        new ItemPrice() {
            Id = 19,
            Price = 2500,
            Name = "MTF-E11-SR",
            Description = "Die Standard MTF waffe, BRRRR SKRRRR.",
            ItemTypes = new List<ItemType> {ItemType.GunE11SR},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },

        new ItemPrice() {
            Id = 20,
            Price = 3500,
            Name = "Schwere Brustpanzerung",
            Description = "Falls deine Mutter die Sandale nach dir wirft.",
            ItemTypes = new List<ItemType> {ItemType.ArmorHeavy},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },

        new ItemPrice() {
            Id = 21,
            Price = 1500,
            Name = "SCP-500",
            Description = "SCP-500, falls du auf Lego getreten bist.",
            ItemTypes = new List<ItemType> {ItemType.SCP500},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },

        new ItemPrice() {
            Id = 22,
            Price = 500,
            Name = "Schmerzmittel",
            Description = "Der letzte ausweg wenn du 096 in die Augen guckst.",
            ItemTypes = new List<ItemType> {ItemType.Painkillers},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },

        new ItemPrice() {
            Id = 23,
            Price = 1000,
            Name = "Granate",
            Description = "Boom",
            ItemTypes = new List<ItemType> {ItemType.GrenadeHE},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },


        new ItemPrice() {
            Id = 24,
            Price = 750,
            Name = "Flash Granate",
            Description = "Discord lightmode",
            ItemTypes = new List<ItemType> {ItemType.GrenadeFlash},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },

        new ItemPrice() {
            Id = 25,
            Price = 50000,
            Name = "X3-Particle-Disruptor",
            Description = "Lazzzzertag",
            ItemTypes = new List<ItemType> {ItemType.GrenadeFlash},
            Roles = new List<RoleType> { RoleType.NtfCaptain, RoleType.NtfPrivate, RoleType.NtfSergeant, RoleType.NtfSpecialist },
            Maxbuys = 5,
            Category = 4
        },









        new ItemPrice() {
            Id = 26,
            Price = 7000,
            Name = "Facilitymanager Karte",
            Description = "Eine Facilitymanager Karte. Mach ruig die nuke\r\nan, die SCP's machen die eh aus.",
            ItemTypes = new List<ItemType>  {ItemType.KeycardFacilityManager},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },


        new ItemPrice() {
            Id = 27,
            Price = 7000,
            Name = "Logicer",
            Description = "BRRRRRRRRRRRRRRRRRRRRR.",
            ItemTypes = new List<ItemType>  {ItemType.GunLogicer},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5 
        },


        new ItemPrice() {
            Id = 28,
            Price = 2500,
            Name = "Shotgun",
            Description = "Eine Shotgut, scheint zu schiessen(Ich hab kein doppel s).",
            ItemTypes = new List<ItemType>  { ItemType.GunShotgun },
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },


        new ItemPrice() {
            Id = 29,
            Price = 3500,
            Name = "Schwere Brustpanzerung",
            Description = "Falls deine Mutter die Sandale nach dir wirft.",
            ItemTypes = new List<ItemType> {ItemType.ArmorHeavy},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },


        new ItemPrice() {
            Id = 30,
            Price = 500,
            Name = "Schmerzmittel",
            Description = "Der letzte ausweg wenn du 096 in die Augen guckst.",
            ItemTypes = new List<ItemType> {ItemType.Painkillers},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },

        new ItemPrice() {
            Id = 31,
            Price = 1000,
            Name = "Granate",
            Description = "Boom",
            ItemTypes = new List<ItemType> {ItemType.GrenadeHE},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },


        new ItemPrice() {
            Id = 32,
            Price = 750,
            Name = "Flash Granate",
            Description = "Discord lightmode",
            ItemTypes = new List<ItemType> {ItemType.GrenadeFlash},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },

        new ItemPrice() {
            Id = 33,
            Price = 50000,
            Name = "X3-Particle-Disruptor",
            Description = "Lazzzzertag",
            ItemTypes = new List<ItemType> {ItemType.ParticleDisruptor},
            Roles = new List<RoleType> { RoleType.ChaosConscript, RoleType.ChaosMarauder, RoleType.ChaosRepressor, RoleType.ChaosRifleman },
            Maxbuys = 5,
            Category = 5
        },






        new ItemPrice() {
            Id = 34,
            Price = 300,
            Name = "Coin",
            Description = "Eine simple Münze",
            ItemTypes = new List<ItemType> {ItemType.Coin},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 6
        },

        new ItemPrice() {
            Id = 35,
            Price = 350,
            Name = "Flashlight",
            Description = "Die perfekte Lichtquelle",
            ItemTypes = new List<ItemType> {ItemType.Flashlight},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 6
        },


        new ItemPrice() {
            Id = 36,
            Price = 1000,
            Name = "SCP-207",
            Description = "LETS GOO SPEEEEEEED",
            ItemTypes = new List<ItemType> {ItemType.SCP207},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 6
        },


        new ItemPrice() {
            Id = 37,
            Price = 450,
            Name = "Adrenalin",
            Description = "Gibt dir 30 AHP und Speed.",
            ItemTypes = new List<ItemType> {ItemType.Adrenaline},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 6
        },

        new ItemPrice() {
            Id = 38,
            Price = 500,
            Name = "Medkit",
            Description = "Ein Medkit, selbst für die schlimmsten Knochenbrüche!.",
            ItemTypes = new List<ItemType> {ItemType.Medkit },
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 6
        },







        new ItemPrice() {
            Id = 39,
            Price = 0,
            Name = "Kostenlose Munition",
            Description = "Gratis Munition von jedem typen.",
            AmmoTypes = new List<AmmoType> {AmmoType.Nato762, AmmoType.Ammo44Cal, AmmoType.Nato556, AmmoType.Ammo12Gauge, AmmoType.Nato9},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 7,
            IsAmmo = true,
            AmmoAmount = 45
        },

        new ItemPrice() {
            Id = 40,
            Price = 75,
            Name = "9x19mm Munition",
            Description = "Munition für Pistolen, Crossvecs etc.",
            AmmoTypes = new List<AmmoType> {AmmoType.Nato9},
            Roles = new List<RoleType> { RoleType.None },
            Maxbuys = 5,
            Category = 7,
            IsAmmo = true,
            AmmoAmount = 45
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