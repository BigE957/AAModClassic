using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Altar
{
    public class WormSpawn : ModNPC
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heavenly Voice");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Equinox_Intro"];
            NPC.lifeMax = 1;
            NPC.boss = true;
            NPC.dontTakeDamage = true; 
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10000000;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (!NPC.HasNPCTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f);

            if (!NPC.AnyNPCs(ModContent.NPCType<DBPortal>()))
            {
                WormAltar_Tile.SpawnBoss(player, ModContent.NPCType<DBPortal>(), false, player.Center);
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<NCPortal>()))
            {
                WormAltar_Tile.SpawnBoss(player, ModContent.NPCType<NCPortal>(), false, player.Center);
            }

            NPC.ai[0]++;

            string s = Main.netMode == NetmodeID.SinglePlayer ? "" : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.s");

            if (NPC.ai[0] == 175)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn1"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 235)
            {

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn2") + s + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn3"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 380)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn4"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 540)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn5"), new Color(43, 178, 245));
            }
            if (NPC.ai[0] == 720)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn6") + s + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn7"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 780)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn8"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 990)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn9"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 1200)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn10"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 1480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn11"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 1600)
            {
                string name = Main.netMode == NetmodeID.SinglePlayer ? player.name : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.heroes");

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn12") + name + ".", new Color(0, 255, 181));

                AAWorld.WormActive = true;
            }

            if (NPC.ai[0] >= 1880)
            {
                AAWorld.WormActive = true;
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                return false;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }
    }
}