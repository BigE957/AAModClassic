using AAModClassic._Content._Misc.__Hardmode.Items.Consumables;
using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs;
using AAModClassic.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unofficial.Content.Bunny
{
    public class RajahReputationSystem : ModSystem
    {
        internal static HashSet<int> NPCsWhoHurtBunnies = [];

        public override void ClearWorld()
        {
            NPCsWhoHurtBunnies.Clear();
        }

        public override void PostUpdateNPCs()
        {
            if (NPCsWhoHurtBunnies.Count > 0)
                NPCsWhoHurtBunnies.RemoveWhere(i => !Main.npc[i].active);
        }
    }

    public class RajahReputationPlayer : ModPlayer
    {
        public bool HasMetRajah = false;
        public float MyRajahReputation = 0f;
        public bool BeyondSaving = false;
        public bool Good => !BeyondSaving && MyRajahReputation > 0.5f;
        public bool Evil => BeyondSaving || MyRajahReputation < -0.5f;
        public bool GettingEvil = false;

        public void ModifyReputation(float change)
        {
            if (BeyondSaving)
                return;

            if (!HasMetRajah)
                change /= 10f;

            if (!GettingEvil && MyRajahReputation > -0.25f && MyRajahReputation + change <= -0.25f)
            {
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo3"), 107, 137, 179);
                GettingEvil = true;
            }
            else if (GettingEvil && MyRajahReputation <= -0.25f && MyRajahReputation + change > -0.25f)
                GettingEvil = false;

            MyRajahReputation += change;
            if (MyRajahReputation <= -1)
                BeyondSaving = true;
        }

        public void ResetReputation()
        {
            HasMetRajah = false;
            MyRajahReputation = 0f;
            BeyondSaving = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.IsBunny())
            {
                Player.ModifyRajahReputation((target.life <= 0) ? -0.05f : -0.001f);
                if (target.life <= 0)
                    target.GetGlobalNPC<BunnyDeathGlobalNPC>().DeathType = RabbitDeathType.KilledByEnemy;
            }
            else if (target.IsGoodToKill())
                Player.ModifyRajahReputation((target.life <= 0) ? 0.075f : 0.0015f);
            else if (RajahReputationSystem.NPCsWhoHurtBunnies.Contains(target.whoAmI))
                Player.ModifyRajahReputation((target.life <= 0) ? 0.1f : 0.002f);
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("HasMetRajah", HasMetRajah);
            tag.Add("MyRajahReputation", MyRajahReputation);
            tag.Add("BeyondSaving", BeyondSaving);
        }

        public override void LoadData(TagCompound tag)
        {
            ResetReputation();

            tag.TryGet("HasMetRajah", out HasMetRajah);
            tag.TryGet("MyRajahReputation", out MyRajahReputation);
            tag.TryGet("BeyondSaving", out BeyondSaving);
        }
    }

    public static class RajahReputationUtils
    {
        public static void ModifyRajahReputation(this Player player, float change) => player.GetModPlayer<RajahReputationPlayer>().ModifyReputation(change);

        public static bool IsBunny(this NPC npc)
        {
            return npc.type == NPCID.Bunny ||
                   npc.type == NPCID.GoldBunny ||
                   npc.type == NPCID.BunnySlimed ||
                   npc.type == NPCID.BunnyXmas ||
                   npc.type == NPCID.ExplosiveBunny ||
                   npc.type == NPCID.GemBunnyAmber ||
                   npc.type == NPCID.GemBunnyAmethyst ||
                   npc.type == NPCID.GemBunnyDiamond ||
                   npc.type == NPCID.GemBunnyEmerald ||
                   npc.type == NPCID.GemBunnyRuby ||
                   npc.type == NPCID.GemBunnySapphire ||
                   npc.type == NPCID.GemBunnyTopaz ||
                   npc.type == NPCID.TownBunny ||
                   npc.type == NPCID.PartyBunny ||
                   npc.type == ModContent.NPCType<WoundedBunny>();
        }

        public static bool IsGoodToKill(this NPC npc)
        {
            return npc.type == NPCID.CorruptBunny ||
                   npc.type == NPCID.CrimsonBunny;
        }

        public static bool CanBecomeWounded(this NPC npc) => npc.type == NPCID.Bunny && !npc.SpawnedFromStatue && !npc.GetGlobalNPC<BunnyDeathGlobalNPC>().BeenHealed;
    }

    public class BunnyCarrotGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.IsBunny();
        public bool FedCarrot = false;
    }

    public class RajahReputationGlobalNPC : GlobalNPC
    {
        public override bool PreHoverInteract(NPC npc, bool mouseIntersects)
        {
            Point tileCoords = npc.Hitbox.ClosestPointInRect(Main.LocalPlayer.Center).ToTileCoordinates();
            bool inRange = Main.LocalPlayer.InInteractionRange(tileCoords.X, tileCoords.Y, TileReachCheckSettings.Simple);

            if (!mouseIntersects || !inRange)
                return true;

            if (!npc.IsBunny() || npc.type == ModContent.NPCType<WoundedBunny>() || npc.GetGlobalNPC<BunnyCarrotGlobalNPC>().FedCarrot)
                return true;

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Carrot>()))
            {
                Player player = Main.LocalPlayer;
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = ModContent.ItemType<Carrot>();

                if (!player.dead && Main.mouseRight)
                {
                    if (PlayerInput.UsingGamepad)
                        player.releaseInventory = false;

                    if (!player.tileInteractionHappened && Main.LocalPlayer.ConsumeItem(ModContent.ItemType<Carrot>()))
                    {
                        player.ModifyRajahReputation(0.025f);
                        npc.GetGlobalNPC<BunnyCarrotGlobalNPC>().FedCarrot = true;
                        SoundEngine.PlaySound(SoundID.Item2, npc.Center);
                    }
                }

                return false;
            }

            return true;
        }

        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            if (npc.type == NPCID.TownBunny && firstButton)
                Main.LocalPlayer.ModifyRajahReputation(0.001f);
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.CanBecomeWounded())
                modifiers.SetMaxDamage(target.life - 1);
        }

        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            if (target.IsBunny())
            {
                RajahReputationSystem.NPCsWhoHurtBunnies.Add(npc.whoAmI);

                bool canBecomeWounded = target.CanBecomeWounded();
                if (target.life <= (canBecomeWounded ? 1 : 0))
                {
                    target.GetGlobalNPC<BunnyDeathGlobalNPC>().DeathType = RabbitDeathType.KilledByEnemy;
                    if (canBecomeWounded)
                    {
                        target.Transform(ModContent.NPCType<WoundedBunny>());
                        target.life = 1;
                    }
                }
            }
        }
    }

    public class RajahReputationGlobalItem : GlobalItem
    {
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (context is RecipeItemCreationContext)
            {
                switch (item.type)
                {
                    case ItemID.ExplosiveBunny:
                        Main.LocalPlayer.ModifyRajahReputation(-0.06f);
                        break;
                    case ItemID.BunnyStew:
                        Main.LocalPlayer.ModifyRajahReputation(-0.04f);
                        break;
                }
            }
            else if (context is BuyItemCreationContext)
            {
                switch (item.type)
                {
                    case ItemID.DontHurtCrittersBook:
                        Main.LocalPlayer.ModifyRajahReputation(0.05f);
                        break;
                    case ItemID.LicenseBunny:
                        Main.LocalPlayer.ModifyRajahReputation(0.05f);
                        break;
                }
            }
        }
    }
}
