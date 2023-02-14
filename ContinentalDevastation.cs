using DBZGoatLib.Handlers;
using DBZGoatLib.Model;
using DBZMODPORT;
using Microsoft.Xna.Framework;
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
        public TraitInfo death => new TraitInfo("Death's Incarnate", 0.001f, new Gradient(Color.DarkOrange).AddStop(0.5f, Color.White).AddStop(1f, Color.Black), OnTrait1, OnLoseTrait1);

        public override void Load()
        {
            TraitHandler.RegisterTrait(death);
        }

        public override void Unload()
        {
            TraitHandler.UnregisterTrait(death);
        }
    }
}