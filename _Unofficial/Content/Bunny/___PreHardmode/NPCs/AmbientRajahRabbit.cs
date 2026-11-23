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
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

            DialogueUISystem.DialogueTreeSetup += RajahModifyTree;
            DialogueUISystem.ResponseSelect += RajahDialogueReponseClicked;
            DialogueUISystem.DialogueEnd += RajahDialogueEnd;
        }

        private static void RajahModifyTree(string treeKey, ref DialogueTree tree)
        {
            //Nothing to see here.
        }

        private static void RajahDialogueReponseClicked(string treeKey, int dialogueID, int buttonID)
        {
            if (DialogueUISystem.CurrentContext.Arguments[0] < 0 || DialogueUISystem.CurrentContext.Arguments[0] > 200)
                return;

            NPC npc = Main.npc[(int)DialogueUISystem.CurrentContext.Arguments[0]];

            if (!npc.active || npc.ModNPC is not AmbientRajahRabbit me)
                return;

            string name = treeKey.Replace("Mods.AAModClassic.DialogueTrees.Rajah.", "");
            switch (name)
            {
                case "FirstGrave":
                    if ((dialogueID == 4 && buttonID == 0) || dialogueID == 9 || dialogueID == 12)
                    {
                        me.RajahFlee();
                        break;
                    }

                    if ((dialogueID == 6 && buttonID == 1) || (dialogueID == 10 && buttonID == 1))
                    {
                        me.RajahEnrage();
                        break;
                    }
                    break;
            }
        }

        private void RajahFlee()
        {
            int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(-80, -16), NPC.velocity, ModContent.ProjectileType<RajahRabbitBookIt>(), 100, 0, Main.myPlayer);
            NPC.active = false;
            NPC.netUpdate = true;
        }

        private void RajahEnrage()
        {
            NPC.Transform(ModContent.NPCType<RajahRabbit>());
            NPC.position.X -= 36;
            NPC.ai[3] = -1;
            NPC.netUpdate = true;
            (NPC.ModNPC as RajahRabbit).Roar(RajahRabbit.RoarTimerMax);
            ChatUtils.Chat(NPC.TypeName + " " + Language.GetTextValue("Mods.AAModClassic.Common.BosshasAwoken"), new Color(175, 75, 255));
            Main.LocalPlayer.ModifyRajahReputation(-2f);
            Main.LocalPlayer.GetModPlayer<DialoguePlayer>().chattingLastFrame = false;
        }

        private static void RajahDialogueEnd(string treeKey, int dialogueID)
        {
            //Nothing to see here.
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
            foreach(Player p in Main.ActivePlayers)
            {
                //p.GetModPlayer<RajahReputationPlayer>().ResetReputation();

                if (p.dead)
                    continue;

                if(p.GetModPlayer<RajahReputationPlayer>().BeyondSaving && p.DistanceSQ(NPC.Center) < 102400)
                    RajahEnrage();
            }

            NPC.ai[0]++;
        }

        public override bool CheckActive() => NPC.ai[0] < 3600;

        public override bool CanChat() => !Main.LocalPlayer.GetModPlayer<RajahReputationPlayer>().BeyondSaving;

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
            spriteBatch.Draw(tex, drawPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            return false;
        }
    }

    public class RajahReputationSystem : ModSystem
    {
        internal static HashSet<int> NPCsWhoHurtBunnies = [];

        public override void ClearWorld()
        {
            NPCsWhoHurtBunnies.Clear();
        }

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

            if (!HasMetRajah)
                change /= 10f;

            MyRajahReputation += change;
            if (MyRajahReputation <= -1)
                BeyondSaving = true;

            Main.NewText("Your reputation has changed: " + MyRajahReputation);
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
                Player.ModifyRajahReputation((target.life <= 0) ? -0.05f : -0.001f);
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
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            if(npc.type == NPCID.TownBunny && firstButton)
                Main.LocalPlayer.ModifyRajahReputation(0.001f);
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
            else if(context is BuyItemCreationContext)
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
