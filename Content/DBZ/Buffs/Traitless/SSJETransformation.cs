using DBZGoatLib;
using DBZGoatLib.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ContinentalDevastation.Content.DBZ.Buffs.Traitless
{
    internal class SSJETransformation : Transformation
    {

        public override AuraData AuraData() => new AuraData("ContinentalDevastation/Assets/Textures/Effects/Animations/Aura/SSJEAura", 8, BlendState.AlphaBlend, new Color(255, 255, 255));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<ContinentalDevastationPlayer>();

            // GPlayer is DBZGoatLib's mod class. It contains the player's current trait.
            bool isDeath = player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate";

            return !player.HasBuff<SSJETransformation>() && modPlayer.SSJEAchieved && isDeath;
        }

        public override string FormName() => "SSJE";

        public override string HairTexturePath() => "ContinentalDevastation/Assets/Textures/Hairs/SSJEHair";

        public override Gradient KiBarGradient()
        {
            Gradient gradient = new(Color.DarkOrange);
            gradient.AddStop(0.6f, Color.DarkOrange);
            gradient.AddStop(0.75f, Color.White);
            gradient.AddStop(1f, Color.Black);

            return gradient;
        }

        public override void OnTransform(Player player) =>
            player.GetModPlayer<ContinentalDevastationPlayer>().SSJEActive = true;
        public override void PostTransform(Player player) =>
            player.GetModPlayer<ContinentalDevastationPlayer>().SSJEActive = false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("", "ContinentalDevastation/Assets/Music/SSJETheme", 9092);

        public override bool Stackable() => true;

        public override Color TextColor() => Color.DarkOrange;

        public override void SetStaticDefaults()
        {
            damageMulti = 5f; // The Damage multiplier. 1f is 0% bonus, 1.2f is 20% bonus, 3.2f is 220% and so on.
            speedMulti = 2f; // The speed multiplier. 1f is 0% bonus 1.5f is 50% bonus, etc.

            // The rate at which ki is drained. You lose this much ki every TICK. 
            // Terraria runs at 60 ticks per second. So you lose 60 times this value every second.
            // 4.5f = 270 ki/sec for example
            kiDrainRate = 8f;
            kiDrainRateWithMastery = 3.5f;

            attackDrainMulti = 3.75f; // Extra Ki usage multiplier, higher value means you use more Ki on ki attacks
            baseDefenceBonus = 157; // Bonus to defense

            base.SetStaticDefaults(); // ALWAYS call this somewhere in your SetStaticDefaults()!
        }
        public AnimationData Anims() => new AnimationData(AuraData(), SaiyanSparks(), SoundData(), HairTexturePath());

        public TransformationInfo info => new(676, "SSJE", true, "Only after triumph during the moon of solar, can one achieve this ancient power", TextColor(), CanTransform, OnTransform, PostTransform, Anims(), new Gradient(Color.DarkOrange).AddStop(0.65f, Color.DarkOrange).AddStop(0.8f, Color.White).AddStop(1f, Color.Black));
    }
}
