using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.Items;
using AAModClassic.UI.Dialogue;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs
{
    public class AmbientRajahRabbit : ModNPC
    {
        public override string Texture => "AAModClassic/_Content/Bunny/__Hardmode/NPCs/__BossRajahRabbit/RajahRabbit";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 8;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 150;
            NPC.aiStyle = -1;
            NPC.damage = 130;
            NPC.defense = 90;
            NPC.lifeMax = 65000;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/Rajah");
            NPC.value = Item.buyPrice(0, 1, 10, 0);
            NPC.dontTakeDamage = true;
            NPC.netAlways = true;
            NPC.friendly = true;
        }

        public override void AI()
        {
            base.AI();
        }

        public override bool CheckActive() => true;

        public override bool CanChat() => true;

        public override string GetChat()
        {
            Main.BestiaryTracker.Chats.RegisterChatStartWith(ContentSamples.NpcsByNetId[ModContent.NPCType<RajahRabbit>()]);

            Main.LocalPlayer.GetModPlayer<RajahReputationPlayer>().HasMetRajah = true;

            Main.LocalPlayer.GetModPlayer<DialoguePlayer>().StartDialogue(NPC, "Mods.AAModClassic.DialogueTrees.Rajah.FirstGrave");

            return "";
        }

        public override void FindFrame(int frameHeight)
        {
            int rajahFrameWidth = TextureAssets.Npc[NPC.type].Width() / 4;
            NPC.frame.Width = rajahFrameWidth;
            NPC.frame.X = 0;

            if (NPC.frameCounter++ > 7)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 2)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Vector2 drawPos = NPC.Center - Vector2.UnitY * 31 - screenPos;
            spriteBatch.Draw(tex, drawPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            return false;
        }
    }

    public class RajahReputationSystem : ModSystem
    {
        internal static HashSet<int> NPCsWhoHurtBunnies = [];

        public override void PostUpdateNPCs()
        {
            if(NPCsWhoHurtBunnies.Count > 0)
                NPCsWhoHurtBunnies.RemoveWhere(i => !Main.npc[i].active);
        }
    }

    public class RajahReputationPlayer : ModPlayer
    {
        public bool HasMetRajah = false;
        public float MyRajahReputation = 0f;
        public bool BeyondSaving = false;

        public void ModifyReputation(float change)
        {
            if (BeyondSaving)
            {
                Main.NewText("You're fucking disgusting there's no saving you.");
                return;
            }

            //if (!HasMetRajah && change < 0)
            //    return;

            MyRajahReputation += change;
            if (MyRajahReputation <= -1)
                BeyondSaving = true;

            Main.NewText("Your reputation has changed: " + MyRajahReputation);
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
                   npc.type == NPCID.PartyBunny;
        }

        public static bool IsGoodToKill(this NPC npc)
        {
            return npc.type == NPCID.CorruptBunny || 
                   npc.type == NPCID.CrimsonBunny;
        }
    } 

    public class RajahReputationGlobalNPC : GlobalNPC
    {
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (npc.IsBunny())
                player.ModifyRajahReputation((npc.life <= 0) ? -0.05f : -0.001f);
            else if (npc.IsGoodToKill())
                player.ModifyRajahReputation((npc.life <= 0) ? 0.075f : 0.003f);
            else if (RajahReputationSystem.NPCsWhoHurtBunnies.Contains(npc.whoAmI))
                player.ModifyRajahReputation((npc.life <= 0) ? 0.1f : 0.005f);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.owner == 255)
                return;

            Player player = Main.player[projectile.owner];

            if (npc.IsBunny())
                player.ModifyRajahReputation((npc.life <= 0) ? -0.05f : -0.001f);
            else if (npc.IsGoodToKill())
                player.ModifyRajahReputation((npc.life <= 0) ? 0.075f : 0.003f);
            else if(RajahReputationSystem.NPCsWhoHurtBunnies.Contains(npc.whoAmI))
                player.ModifyRajahReputation((npc.life <= 0) ? 0.1f : 0.005f);
        }

        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            if (target.IsBunny())
                RajahReputationSystem.NPCsWhoHurtBunnies.Add(npc.whoAmI);
        }

        public override void OnKill(NPC npc)
        {
            RabbitTombstoneSystem.RegisterRabbitDeath(npc.Center, npc.type == NPCID.GoldBunny);
        }
    }

    public class RajahReputationGlobalItem : GlobalItem
    {
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (context is RecipeItemCreationContext crafted)
            {
                switch (item.type)
                {
                    case ItemID.ExplosiveBunny:
                        Main.LocalPlayer.ModifyRajahReputation(-0.01f);
                        break;
                    case ItemID.BunnyStew:
                        Main.LocalPlayer.ModifyRajahReputation(-0.05f);
                        break;
                }
            }
            else if(context is BuyItemCreationContext bought)
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
