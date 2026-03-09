using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AAModClassic.Tiles.Altar
{
    public class WormSpawn : ModNPC
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heavenly Voice");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.alpha = 255;
            //Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Prequinox");
            NPC.lifeMax = 1;
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
                WormAltar.SpawnBoss(player, ModContent.NPCType<DBPortal>(), false, player.Center);
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<NCPortal>()))
            {
                WormAltar.SpawnBoss(player, ModContent.NPCType<NCPortal>(), false, player.Center);
            }

            NPC.ai[0]++;

            string s = Main.netMode == NetmodeID.SinglePlayer ? "" : Lang.TheEquinox("s");

            if (NPC.ai[0] == 180)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn1"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 360)
            {

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn2") + s + Lang.TheEquinox("WormSpawn3"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 540)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn4"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 720)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn5"), new Color(43, 178, 245));
            }
            if (NPC.ai[0] == 900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn6") + s + Lang.TheEquinox("WormSpawn7"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn8"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 1140)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn9"), new Color(43, 178, 245));
            }

            if (NPC.ai[0] == 1320)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn10"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] == 1520)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn11"), new Color(0, 255, 181));
            }

            if (NPC.ai[0] >= 1880)
            {
                string name = Main.netMode == NetmodeID.SinglePlayer ? player.name : Lang.TheEquinox("heroes");

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.TheEquinox("WormSpawn12") + name + ".", new Color(0, 255, 181));

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