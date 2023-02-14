using ContinentalDevastation.Content.DBZ.Buffs.DeathsIncarnate;
using ContinentalDevastation.Content.DBZ.Buffs.Traitless;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using DBZGoatLib.Model;
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
        public bool GSSJAchieved;
        public bool GSSJActive;
        public bool GSSJUnlockMsg;
        public bool SSJEAchieved;
        public bool SSJEReady;
        public bool SSJEActive;
        public bool SSJEUnlockMsg;
        public bool MP_unlock;

        private bool masteredMessageGSSJ;
        private bool masteredMessageSSJE;

        public int SSJETimer;


        public override void SaveData(TagCompound tag)
        {
            tag.Add("GSSJAchieved", GSSJAchieved);
            tag.Add("GSSJUnlockMsg", GSSJUnlockMsg);
            tag.Add("SSJEAchieved", SSJEAchieved);
            tag.Add("SSJEUnlockMsg", SSJEUnlockMsg);
            tag.Add("SSJEReady", SSJEReady);

            tag.Add("masteredMessageGSSJ", (object)masteredMessageGSSJ);
            tag.Add("masteredMessageSSJE", (object)masteredMessageSSJE);
        }

        public override void LoadData(TagCompound tag)
        {
            GSSJAchieved = tag.GetBool("GSSJAchieved");
            GSSJUnlockMsg = tag.GetBool("GSSJUnlockMsg");
            SSJEAchieved = tag.GetBool("SSJEAchieved");
            SSJEUnlockMsg = tag.GetBool("SSJEUnlockMsg");
            SSJEReady = tag.GetBool("SSJEReady");

            masteredMessageGSSJ = tag.GetBool("masteredMessageGSSJ");
            masteredMessageSSJE = tag.GetBool("masteredMessageSSJE");

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
                if (player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate" && NPC.downedQueenBee)
                {
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
                if (SSJETimer >= 300)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        SSJEAchieved = true;
                        SSJETimer = 0;
                    }
                    else if (SSJETimer >= 300)
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
                    Main.NewText("Something of what seems like Eclipse origin's swirls inside of you... You let it loose.", Color.DarkOrange);
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation(ModContent.BuffType<SSJETransformation>()).Value);
                }
            }

            if (((ModPlayer)this).Player.GetModPlayer<GPlayer>().GetMastery("GSSJ") >= 1f && !masteredMessageGSSJ)
            {
                masteredMessageGSSJ = true;
                Main.NewText("Your Gray Super Saiyan has reached Max Mastery.", (byte)132, (byte)132, (byte)132);
            }
            if (((ModPlayer)this).Player.GetModPlayer<GPlayer>().GetMastery("SSJE") >= 1f && !masteredMessageSSJE)
            {
                masteredMessageSSJE = true;
                Main.NewText("Your Super Saiyan Eclipse has reached Max Mastery.", (byte)132, (byte)132, (byte)132);
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
    }

    public class GraySaiyanTree : TransformationTree
    {
        public override bool Complete() => true;

        public override bool Condition(Player player)
        {
            if (player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate")
            {
                return true;
            }

            return false;
        }

        public override Connection[] Connections() { return null; }

        public override string Name() => "Death's Transformation Tree";

        public bool UnlockCondition1(Player player)
        {
            return player.GetModPlayer<ContinentalDevastationPlayer>().GSSJAchieved && player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate";
        }

        // our condition for whether or not our Node is discoevered
        public bool DiscoverCondition1(Player player)
        {
            var ModPlayer = player.GetModPlayer<ContinentalDevastationPlayer>();

            // For this example, we are assuming that you know how to fetch data from Dragon Ball Terraria
            // to fetch whether their form is unlocked or not.
            return ModPlayer.GSSJAchieved && player.GetModPlayer<GPlayer>().Trait == "Death's Incarnate";
        }

        public bool UnlockCondition2(Player player)
        {
            return player.GetModPlayer<ContinentalDevastationPlayer>().SSJEAchieved;
        }

        // our condition for whether or not our Node is discoevered
        public bool DiscoverCondition2(Player player)
        {
            var ModPlayer = player.GetModPlayer<ContinentalDevastationPlayer>();

            // For this example, we are assuming that you know how to fetch data from Dragon Ball Terraria
            // to fetch whether their form is unlocked or not.
            return ModPlayer.SSJEAchieved;
        }

        public override Node[] Nodes() => new Node[]
        {
            new Node(1, 2, "GSSJ", "ContinentalDevastation/Content/DBZ/Buffs/DeathsIncarnate/GSSJTransformation", "Only after being Death's incarnate can one achieve this power.", UnlockCondition1, DiscoverCondition1),
            new Node(3, 2, "SSJE", "ContinentalDevastation/Content/DBZ/Buffs/Traitless/SSJETransformation", "Only after one's death in the eclipse of solar, can they achieve this ancient power", UnlockCondition2, DiscoverCondition2)
        };
    }
}