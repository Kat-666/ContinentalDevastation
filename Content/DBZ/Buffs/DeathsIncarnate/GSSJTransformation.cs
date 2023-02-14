using DBZGoatLib;
using DBZGoatLib.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using DBZGoatLib.UI;
using ContinentalDevastation;

namespace ContinentalDevastation.Content.DBZ.Buffs.DeathsIncarnate
{
    internal class GSSJTransformation : Transformation
    {


        public override AuraData AuraData() => new AuraData("ContinentalDevastation/Assets/Textures/Effects/Animations/Aura/GSSJAura", 4, BlendState.AlphaBlend, new Color(74, 74, 74));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<ContinentalDevastationPlayer>();

            bool isDeath = player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate";

            return !player.HasBuff<GSSJTransformation>() && modPlayer.GSSJAchieved && isDeath;
        }

        public override string FormName() => "GSSJ";

        public override string HairTexturePath() => "ContinentalDevastation/Assets/Textures/Hairs/GSSJHair";

        public override Gradient KiBarGradient() => null;

        public override void OnTransform(Player player) =>
            player.GetModPlayer<ContinentalDevastationPlayer>().GSSJActive = true;
        public override void PostTransform(Player player) =>
            player.GetModPlayer<ContinentalDevastationPlayer>().GSSJActive = false;

        public override bool SaiyanSparks() => true;

        public override SoundData SoundData() => new SoundData("ContinentalDevastation/Assets/Sounds/GSSJAscension", "ContinentalDevastation/Assets/Sounds/GSSJ", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Gray;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 6f;
            kiDrainRateWithMastery = 3f;
            attackDrainMulti = 8.5f;
            if (NPC.downedBoss2)
            {
            damageMulti = 2.5f;
            speedMulti = 1f;

            baseDefenceBonus = 31;
            }
            else if (NPC.downedBoss3 && NPC.downedBoss2)
            {
                damageMulti = 3.5f;
                baseDefenceBonus = 56;
            }
            else if (NPC.downedBoss3 && NPC.downedQueenBee)
            {
                damageMulti = 4.5f;
                baseDefenceBonus = 97;
            }
            else if (Main.hardMode == true && NPC.downedMechBossAny)
            {
                damageMulti = 6f;
                baseDefenceBonus = 132;
            }

            base.SetStaticDefaults(); // ALWAYS call this somewhere in your SetStaticDefaults()!
        }

        public AnimationData Anims() => new AnimationData(AuraData(), SaiyanSparks(), SoundData(), HairTexturePath());

        public TransformationInfo info => new(676, "SSJE", true, "Only after triumph during the moon of solar, can one achieve this ancient power", TextColor(), CanTransform, OnTransform, PostTransform, Anims(), new Gradient(Color.DarkOrange).AddStop(0.65f, Color.DarkOrange).AddStop(0.8f, Color.White).AddStop(1f, Color.Black));
    }
}
