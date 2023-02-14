using ContinentalDevastation.Content.DBZ.Buffs.DeathsIncarnate;
using ContinentalDevastation.Content.DBZ.Buffs.Traitless;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ContinentalDevastation
{
    public class ContinentalDevastationPlayer : ModPlayer
    {
        public static ModKeybind SSJEKeybind;

        public bool GSSJAchieved;
        public bool GSSJActive;
        public bool GSSJUnlockMsg;
        public bool SSJEAchieved;
        public bool SSJEReady;
        public bool SSJEActive;
        public bool SSJEUnlockMsg;
        public bool MP_unlock;

        public bool eclipseOver;

        public int SSJETimer;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("GSSJAchieved", GSSJAchieved);
            tag.Add("GSSJUnlockMsg", GSSJUnlockMsg);
            tag.Add("SSJEAchieved", SSJEAchieved);
            tag.Add("SSJEUnlockMsg", SSJEUnlockMsg);
            tag.Add("SSJEReady", SSJEReady);
        }

        public override void LoadData(TagCompound tag)
        {
            GSSJAchieved = tag.GetBool("GSSJAchieved");
            GSSJUnlockMsg = tag.GetBool("GSSJUnlockMsg");
            SSJEAchieved = tag.GetBool("SSJEAchieved");
            SSJEUnlockMsg = tag.GetBool("SSJEUnlockMsg");
            SSJEReady = tag.GetBool("SSJEReady");
        }

        public static ContinentalDevastationPlayer ModPlayer(Player player) => player.GetModPlayer<ContinentalDevastationPlayer>();

        public override void OnRespawn(Player player)
        {
            if (MP_unlock && Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate" && NPC.downedBoss2)
                {
                    GSSJAchieved = true;
                    MP_unlock = false;
                }
                if (eclipseOver == true && SSJEReady == false)
                {
                    eclipseOver = false;
                    SSJEReady = true;
                    MP_unlock = false;
                }
            }
        }

        public override void PostUpdate()
        {
            if (GSSJAchieved && !GSSJUnlockMsg)
            {
                GSSJUnlockMsg = true;
                if (Main.netMode != NetmodeID.Server)
                {
                    Main.NewText("For being Death's True Incarnate You have achieved Gray Super Saiyan!", Color.Gray);
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation(ModContent.BuffType<GSSJTransformation>()).Value);
                }
            }

            if (SSJEReady && Player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate")
            {
                SSJETimer++;
                if (SSJETimer >= 600)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        SSJEAchieved = true;
                        SSJETimer = 0;
                    }
                    else if (SSJETimer >= 600)
                    {
                        SSJETimer = 0;
                        Main.NewText(SSJETextSelect(), (Color?)Color.DarkRed);
                    }
                }
            }

            if (SSJEAchieved && !SSJEUnlockMsg)
            {
                SSJEUnlockMsg = true;
                if (Main.netMode != NetmodeID.Server)
                {
                    Main.NewText("Something of demonic nature swirls inside of you... Death is let loose.", Color.DarkOrange);
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation(ModContent.BuffType<SSJETransformation>()).Value);
                }
            }
        }

        public object SSJETextSelect()
        {
            return Main.rand.Next(2) switch
            {
                0 => "You are filled with death, Your rage escalates.",
                1 => "Your blood is drained from within, A deep burning lingers.",
                _ => 0,
            };
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SSJEKeybind.JustPressed)
            {
                if (!TransformationHandler.IsTransformed(Player))
                {
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation(ModContent.BuffType<SSJETransformation>()).Value);
                }
                else
                    TransformationHandler.ClearTransformations(Player);
            }
        }
    }
}