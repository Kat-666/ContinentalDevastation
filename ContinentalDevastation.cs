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

        public void OnTrait2(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 += 10000;
            if (player.statLifeMax >= 100 && hasIncreasedLife == false)
            {
                player.statLifeMax += 250;
                hasIncreasedLife = true;
            }
        }

        public void OnLoseTrait2(Player player)
        {
            player.GetModPlayer<MyPlayer>().kiMax2 -= 10000;
            if (hasIncreasedLife == true)
            {
                player.statLifeMax -= 250;
                hasIncreasedLife = false;
            }
        }

        public TraitInfo MetamorphedTrait => new TraitInfo("Metamorphed", 0f, new Gradient(Color.DarkGreen).AddStop(1f, Color.SpringGreen), OnTrait2, OnLoseTrait2);

        public TraitInfo death => new TraitInfo("Death's Incarnate", 0.001f, new Gradient(Color.DarkOrange).AddStop(0.5f, Color.White).AddStop(1f, Color.Black), OnTrait1, OnLoseTrait1);

        public override void Load()
        {
            TraitHandler.RegisterTrait(MetamorphedTrait);
            TraitHandler.RegisterTrait(death);
        }

        public override void Unload()
        {
            TraitHandler.UnregisterTrait(MetamorphedTrait);
            TraitHandler.UnregisterTrait(death);
        }
    }
}