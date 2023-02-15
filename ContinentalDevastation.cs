using ContinentalDevastation.Content.DBZ.Buffs.DeathsIncarnate;
using ContinentalDevastation.Content.DBZ.Buffs.Traitless;
using DBZGoatLib.Handlers;
using DBZGoatLib.Model;
using DBZMODPORT;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ContinentalDevastation
{
	public class ContinentalDevastation : Mod
	{
        public bool hasIncreasedLife;

        public void OnTrait1(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 += 7500;
        }

        public void OnLoseTrait1(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 -= 7500;
        }

        public void OnTrait2(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 += 15000;
            if (hasIncreasedLife == false)
            {
                player.statLifeMax += 250;
                hasIncreasedLife = true;
            }
        }

        public void OnLoseTrait2(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 -= 15000;
            if (hasIncreasedLife == true)
            {
                player.statLifeMax -= 250;
                hasIncreasedLife = false;
            }
        }

        public TraitInfo death => new TraitInfo("Death's Incarnate", 0.001f, new Gradient(Color.DarkOrange).AddStop(0.5f, Color.White).AddStop(1f, Color.Black), OnTrait1, OnLoseTrait1);
        public TraitInfo Metamorphed => new TraitInfo("Metamorphed", 0.001f, new Gradient(Color.DarkOrange).AddStop(1f, Color.Black), OnTrait2, OnLoseTrait2);

        public override void Load()
        {
            TransformationHandler.RegisterTransformation(TransformationHandler.GetTransformation(ModContent.BuffType<GSSJTransformation>()).GetValueOrDefault());
            TransformationHandler.RegisterTransformation(TransformationHandler.GetTransformation(ModContent.BuffType<SSJETransformation>()).GetValueOrDefault());
            TraitHandler.RegisterTrait(Metamorphed);
            TraitHandler.RegisterTrait(death);
        }

        public override void Unload()
        {
            TransformationHandler.UnregisterTransformation(TransformationHandler.GetTransformation(ModContent.BuffType<GSSJTransformation>()).GetValueOrDefault());
            TransformationHandler.UnregisterTransformation(TransformationHandler.GetTransformation(ModContent.BuffType<SSJETransformation>()).GetValueOrDefault());
            TraitHandler.UnregisterTrait(Metamorphed);
            TraitHandler.UnregisterTrait(death);
        }
    }
}