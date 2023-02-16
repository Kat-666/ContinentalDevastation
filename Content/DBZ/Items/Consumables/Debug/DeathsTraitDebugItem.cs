using DBZGoatLib;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ContinentalDevastation.Content.DBZ.Items.Consumables.Debug
{
    internal class DeathsTraitDebugItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("DEUBG ITEM\nGives the Death's Incarnate Trait.");
            DisplayName.SetDefault("Death's Incarnate Trait Giver");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Item.type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 40000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.defense = 0;
            Item.consumable = true;
        }
        public override bool ConsumeItem(Player player)
        {
            return true;
        }
        public override void OnConsumeItem(Player player)
        {
            player.GetModPlayer<GPlayer>().Trait = "Death's Incarnate";
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<GPlayer>().Trait = "Death's Incarnate";
            return true;
        }
    }
}
