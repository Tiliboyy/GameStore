using System;
using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;

[Serializable]
public class Config : IConfig
{
    [Description("Enables the Plugin")] 
    public bool IsEnabled { get; set; } = true;

    public bool Debug { get; set; } = false;
    public bool ShowOnlyAvalibleItems { get; set; }= true;
    public int MaxMoney { get; set; } = 200000;

    [Description("The amount a player gets from each event. 0 disables the event. -1 Is unlimited")]
    public Structs.Reward EscapeReward { get; set; } = new()
    {
        Name = "Escape",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.Scientist, 2000 },
            { RoleTypeId.ClassD, 2500 }
        },
        MaxPerRound = 1
    };
    public Structs.Reward Scp079LevelReward { get; set; } = new()
    {
        Name = "Scp079Level",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.Scp079, 500 },
        },
        MaxPerRound = -1
    };

    public Structs.Reward CufferReward { get; set; } = new()
    {
        Name = "EscapeCuffer",
        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.None, 1000 }
        },
        MaxPerRound = 1
    };

    public Structs.Reward KillReward { get; set; } = new()
    {
        Name = "Kill",
        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.ChaosConscript, 50 },
            { RoleTypeId.ChaosMarauder, 50 },
            { RoleTypeId.ChaosRepressor, 50 },
            { RoleTypeId.ChaosRifleman, 50 },
            { RoleTypeId.ClassD, 50 },
            { RoleTypeId.FacilityGuard, 50 },
            { RoleTypeId.NtfCaptain, 50 },
            { RoleTypeId.NtfPrivate, 50 },
            { RoleTypeId.NtfSergeant, 50 },
            { RoleTypeId.NtfSpecialist, 50 },
            { RoleTypeId.Scientist, 50 },
            { RoleTypeId.Scp049, 100 },
            { RoleTypeId.Scp079, 100 },
            { RoleTypeId.Scp096, 100 },
            { RoleTypeId.Scp106, 100 },
            { RoleTypeId.Scp0492, 100 },
            { RoleTypeId.Scp939, 100 },
            { RoleTypeId.Scp173, 50 },
            { RoleTypeId.Tutorial, 0 }
        },
        MaxPerRound = -1
    };

    public Structs.Reward ScpKillReward { get; set; } = new()
    {
        Name = "SCPKilled",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.None, 1000 }
        },
        MaxPerRound = 1
    };

    public Structs.Reward DeathReward { get; set; } = new()
    {
        Name = "Died",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.None, 50 }
        },
        MaxPerRound = -1
    };

    public Structs.Reward UsingItemReward { get; set; } = new()
    {
        Name = "UsingItem",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.None, 50 }
        },
        MaxPerRound = -1
    };

    public Structs.Reward SpawnReward { get; set; } = new()
    {
        Name = "Spawned",

        Money = new Dictionary<RoleTypeId, int>
        {
            { RoleTypeId.ChaosConscript, 50 },
            { RoleTypeId.ChaosMarauder, 50 },
            { RoleTypeId.ChaosRepressor, 50 },
            { RoleTypeId.ChaosRifleman, 50 },
            { RoleTypeId.ClassD, 200 },
            { RoleTypeId.FacilityGuard, 200 },
            { RoleTypeId.NtfCaptain, 50 },
            { RoleTypeId.NtfPrivate, 50 },
            { RoleTypeId.NtfSergeant, 50 },
            { RoleTypeId.NtfSpecialist, 50 },
            { RoleTypeId.Scientist, 200 },
            { RoleTypeId.Scp049, 200 },
            { RoleTypeId.Scp079, 200 },
            { RoleTypeId.Scp096, 200 },
            { RoleTypeId.Scp106, 200 },
            { RoleTypeId.Scp0492, 200 },
            { RoleTypeId.Scp939, 200 },
            { RoleTypeId.Tutorial , 0}
        },
        MaxPerRound = -1
    };

    public List<Structs.Category> Categorys { get; set; } = new()
    {
        new Structs.Category
        {
            id = 1, AllowedRoles = new List<RoleTypeId> { RoleTypeId.ClassD }, Name = "D-Klasse",
            Description = "Hier kannst du Gegenstände für D-Klassen kaufen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 2000,
                    NoInventoryCheck = false,
                    Name = "Hausmeisterkarte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardJanitor },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 2,
                    Price = 4000,
                    NoInventoryCheck = false,
                    Name = "Wissenschaftlerkarte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardScientist },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 3,
                    Price = 600,
                    NoInventoryCheck = false,
                    Name = "Schmerzmittel",
                    ItemTypes = new List<ItemType> { ItemType.Painkillers },
                    Maxbuys = 2
                }
            }
        },
        new Structs.Category
        {
            id = 2,
            Name = "Wissenschaftler",
            AllowedRoles = new List<RoleTypeId> { RoleTypeId.Scientist },
            Description = "Hier kannst du Gegenstände für Wissenschaftler kaufen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 4000,
                    NoInventoryCheck = false,
                    Name = "Hauptwissenschaftlerkarte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardResearchCoordinator },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 2,
                    Price = 5000,
                    NoInventoryCheck = false,
                    Name = "Zonenmanagerkarte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardZoneManager },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 3,
                    Price = 500,
                    NoInventoryCheck = false,
                    Name = "Radio",
                    ItemTypes = new List<ItemType> { ItemType.Radio },
                    Maxbuys = 2
                },
                new()
                {
                    Id = 4,
                    Price = 2000,
                    NoInventoryCheck = false,
                    Name = "SCP-500",
                    ItemTypes = new List<ItemType> { ItemType.SCP500 },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 5,
                    Price = 400,
                    NoInventoryCheck = false,
                    Name = "Schmerzmittel",
                    ItemTypes = new List<ItemType> { ItemType.Painkillers },
                    Maxbuys = 2
                },
                new()
                {
                    Id = 6,
                    Price = 5000,
                    NoInventoryCheck = false,
                    Name = "SCP-018",
                    ItemTypes = new List<ItemType> { ItemType.SCP018 },
                    Maxbuys = 2
                },
                new()
                {
                    Id = 7,
                    Price = 7000,
                    NoInventoryCheck = false,
                    Name = "SCP-268",
                    ItemTypes = new List<ItemType> { ItemType.SCP268 },
                    Maxbuys = 1
                },
                new()
                {
                    Id = 8,
                    Price = 3500,
                    NoInventoryCheck = false,
                    Name = "Leichte Brustpanzerung",
                    ItemTypes = new List<ItemType> { ItemType.ArmorLight },
                    Maxbuys = 5
                }
            }
        },
        new Structs.Category
        {
            id = 3, AllowedRoles = new List<RoleTypeId> { RoleTypeId.FacilityGuard }, Name = "Sicherheitspersonal",
            Description = "Hier kannst du Gegenstände für Sicherheitspersonal kaufen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 3000,
                    NoInventoryCheck = false,
                    Name = "Kadettenkarte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardNTFOfficer },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 2,
                    Price = 4000,
                    NoInventoryCheck = false,
                    Name = "Crossvec",
                    ItemTypes = new List<ItemType> { ItemType.GunCrossvec },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 3,
                    Price = 1000,
                    NoInventoryCheck = false,
                    Name = "Granate",
                    ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 4,
                    Price = 1000,
                    NoInventoryCheck = false,
                    Name = "Flash Grenade",
                    ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 5,
                    Price = 4500,
                    NoInventoryCheck = false,
                    Name = "Schwere Brustpanzerung",
                    ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 6,
                    Price = 500,
                    NoInventoryCheck = false,
                    Name = "Schmerzmittel",
                    ItemTypes = new List<ItemType> { ItemType.Painkillers },
                    Maxbuys = 2
                }
            }
        },
        new Structs.Category
        {
            id = 4,
            AllowedRoles = new List<RoleTypeId>
                { RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist },
            Name = "MTF", Description = "Hier kannst du Gegenstände für das MTF kaufen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 7000,
                    NoInventoryCheck = false,
                    Name = "Facilitymanager Karte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardFacilityManager },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 2,
                    Price = 2500,
                    Name = "MTF-E11-SR",
                    NoInventoryCheck = false,
                    ItemTypes = new List<ItemType> { ItemType.GunE11SR },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 3,
                    Price = 3500,
                    NoInventoryCheck = false,
                    Name = "Schwere Brustpanzerung",
                    ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
                    Maxbuys = 1
                },

                new()
                {
                    Id = 4,
                    Price = 1500,
                    NoInventoryCheck = false,
                    Name = "SCP-500",
                    ItemTypes = new List<ItemType> { ItemType.SCP500 },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 5,
                    Price = 500,
                    NoInventoryCheck = false,
                    Name = "Schmerzmittel",
                    ItemTypes = new List<ItemType> { ItemType.Painkillers },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 6,
                    Price = 1000,
                    NoInventoryCheck = false,
                    Name = "Granate",
                    ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
                    Maxbuys = 2
                },


                new()
                {
                    Id = 7,
                    Price = 750,
                    NoInventoryCheck = false,
                    Name = "Flash Granate",
                    ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 8,
                    Price = 50000,
                    NoInventoryCheck = false,
                    Name = "X3-Particle-Disruptor",
                    ItemTypes = new List<ItemType> { ItemType.ParticleDisruptor },
                    Maxbuys = 1
                }
            }
        },
        new Structs.Category
        {
            id = 5,
            AllowedRoles = new List<RoleTypeId>
            {
                RoleTypeId.ChaosConscript, RoleTypeId.ChaosMarauder, RoleTypeId.ChaosRepressor, RoleTypeId.ChaosRifleman
            },
            Name = "Chaos", Description = "Hier kannst du Gegenstände für Chaos insurgency kaufen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 7000,
                    NoInventoryCheck = false,
                    Name = "Facilitymanager Karte",
                    ItemTypes = new List<ItemType> { ItemType.KeycardFacilityManager },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 2,
                    Price = 7000,
                    NoInventoryCheck = false,
                    Name = "Logicer",
                    ItemTypes = new List<ItemType> { ItemType.GunLogicer },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 3,
                    Price = 2500,
                    NoInventoryCheck = false,
                    Name = "Shotgun",
                    ItemTypes = new List<ItemType> { ItemType.GunShotgun },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 4,
                    Price = 3500,
                    NoInventoryCheck = false,
                    Name = "Schwere Brustpanzerung",
                    ItemTypes = new List<ItemType> { ItemType.ArmorHeavy },
                    Maxbuys = 1
                },


                new()
                {
                    Id = 5,
                    Price = 500,
                    Name = "Schmerzmittel",
                    NoInventoryCheck = false,
                    ItemTypes = new List<ItemType> { ItemType.Painkillers },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 6,
                    Price = 1000,
                    Name = "Granate",
                    NoInventoryCheck = false,
                    ItemTypes = new List<ItemType> { ItemType.GrenadeHE },
                    Maxbuys = 2
                },


                new()
                {
                    Id = 7,
                    Price = 750,
                    Name = "Flash Granate",
                    NoInventoryCheck = false,
                    ItemTypes = new List<ItemType> { ItemType.GrenadeFlash },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 8,
                    Price = 50000,
                    Name = "X3-Particle-Disruptor",
                    NoInventoryCheck = false,
                    ItemTypes = new List<ItemType> { ItemType.ParticleDisruptor },
                    Maxbuys = 1
                }
            }
        },
        new Structs.Category
        {
            id = 6, AllowedRoles = new List<RoleTypeId> { RoleTypeId.None }, Name = "Allgemein",
            Description = "Hier findest du allgemeine Sachen.",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 300,
                    NoInventoryCheck = false,
                    Name = "Coin",
                    ItemTypes = new List<ItemType> { ItemType.Coin },
                    Maxbuys = 5
                },

                new()
                {
                    Id = 2,
                    Price = 350,
                    NoInventoryCheck = false,
                    Name = "Flashlight",
                    ItemTypes = new List<ItemType> { ItemType.Flashlight },
                    Maxbuys = 5
                },


                new()
                {
                    Id = 3,
                    Price = 1000,
                    NoInventoryCheck = false,
                    Name = "SCP-207",
                    ItemTypes = new List<ItemType> { ItemType.SCP207 },
                    Maxbuys = 2
                },


                new()
                {
                    Id = 4,
                    Price = 450,
                    NoInventoryCheck = false,
                    Name = "Adrenalin",
                    ItemTypes = new List<ItemType> { ItemType.Adrenaline },
                    Maxbuys = 2
                },

                new()
                {
                    Id = 5,
                    Price = 500,
                    NoInventoryCheck = false,
                    Name = "Medkit",
                    ItemTypes = new List<ItemType> { ItemType.Medkit },
                    Maxbuys = 3
                }
            }
        },
        new Structs.Category
        {
            id = 7, AllowedRoles = new List<RoleTypeId> { RoleTypeId.None },
            Name = "Munition", Description = "Hier findest du Munition",
            Items = new List<Structs.ItemPrice>
            {
                new()
                {
                    Id = 1,
                    Price = 0,
                    NoInventoryCheck = true,
                    Name = "Kostenlose Munition",
                    AmmoTypes = new Dictionary<AmmoType, ushort>
                    {
                        { AmmoType.Nato762, 60 }, { AmmoType.Ammo44Cal, 12 }, { AmmoType.Nato556, 60 },
                        { AmmoType.Ammo12Gauge, 12 }, { AmmoType.Nato9, 60 }
                    },
                    Maxbuys = 1,
                    IsAmmo = true
                },

                new()
                {
                    Id = 2,
                    Price = 75,
                    NoInventoryCheck = true,
                    Name = "9x19mm Munition",
                    AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Nato9, 60 } },
                    Maxbuys = 2,
                    IsAmmo = true
                },
                new()
                {
                    Id = 3,
                    Price = 75,
                    NoInventoryCheck = true,
                    Name = "12/70 Buckshot",
                    AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Ammo12Gauge, 60 } },
                    Maxbuys = 2,
                    IsAmmo = true
                },
                new()
                {
                    Id = 4,
                    Price = 75,
                    NoInventoryCheck = true,
                    Name = ".44 Mag",
                    AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Ammo44Cal, 60 } },
                    Maxbuys = 2,
                    IsAmmo = true
                },
                new()
                {
                    Id = 6,
                    Price = 75,
                    NoInventoryCheck = true,
                    Name = "7.62x39mm Munition",
                    AmmoTypes = new Dictionary<AmmoType, ushort> { { AmmoType.Nato762, 60 } },
                    Maxbuys = 2,
                    IsAmmo = true
                }
            }
        }
    };


}


public class Structs
{
    public struct ItemPrice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        
        public bool NoInventoryCheck { get; set; }


        public List<ItemType> ItemTypes { get; set; }
        public bool IsAmmo { get; set; }
        public Dictionary<AmmoType, ushort> AmmoTypes { get; set; }

        public int Price { get; set; }
        public int Maxbuys { get; set; }
    }

    public struct Category
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public List<RoleTypeId> AllowedRoles { get; set; }
        public List<ItemPrice> Items { get; set; }
    }

    public struct Reward
    {
        public string Name { get; set; }
        public Dictionary<RoleTypeId, int> Money { get; set; }
        public int MaxPerRound { get; set; }
    }
}