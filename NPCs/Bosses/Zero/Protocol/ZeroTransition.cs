using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Effects;
using AAModClassic.Music;

namespace AAModClassic.NPCs.Bosses.Zero.Protocol
{
    public class ZeroTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Rift");
            Main.npcFrameCount[NPC.type] = 26;
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 146;
            NPC.height = 150;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Silence"];
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, NPC.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 8, NPC.frame, AAColor.Oblivion, true);
            return false;
        }

        public override void AI()
        {
			NPC.TargetClosest();			
            Player player = Main.player[NPC.target];

            NPC.ai[0]++;
            
            if (NPC.ai[0] % 5 == 0)
            {
                NPC.frame.Y += 152;
            }
            if (NPC.ai[0] >= 130)
            {
                NPC.frame.Y = 152 * 25;
            }
            if (NPC.ai[0] >= 135 && !NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<ZeroProtocol>(), false, NPC.Center, "", false);

                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                Main.projectile[b].Center = NPC.Center;

                NPC.netUpdate = true;
                NPC.active = false;
            }
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()))
            {
                return false;
            }
            return true;
        }

    }
}