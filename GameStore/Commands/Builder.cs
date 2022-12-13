using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Firearms.Ammo;
using MapEditorReborn.Commands.UtilityCommands;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Map = Exiled.API.Features.Map;
using Player = Exiled.API.Features.Player;
using Warhead = Exiled.API.Features.Warhead;


namespace GameStore.UnityMethods

{
    internal class Builder
    {

        public static string CategoryBuilder()
        {
            string category = "";
            foreach (var item in Plugin.Instance.Config.Categorys)
            {

                category += "\n " + item.Name + "\n" + item.Description;
            }
            return category;
        }
        public static string ItemListBuilder(int category)
        {
            string items = "";
            int i = 1;
            foreach (var item in Plugin.Instance.Config.Items)
            {
                if(category == item.Category)
                {
                    items += $"\n[{i}] {item.Name} - {item.Price} {Plugin.Instance.Translation.Currencyname}";
                    i++;
                } 
            }
            return items;
        }

    }
}