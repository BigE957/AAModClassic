using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Desert.NPCs._BossDesertDjinn
{
    [AutoloadBossHead]
    public class DesertDjinn_Unofficial : ModNPC
    {
        public override string BossHeadTexture => "AAModClassic/_Content/Desert/___PreHardmode/NPCs/__BossDesertDjinn/DesertDjinn_Head_Boss";

        public int Exhaustion = 0;
        public static int ExhaustionCap => Main.masterMode ? 8 : Main.expertMode ? 7 : 5;

        public enum DjinnState
        {
            Spawn,
            RecoverFlex,
            GrandSlam,
            TwisterPunch,
            SubmergedUppercut,
            Dive
        }

        public DjinnState CurrentState { get => (DjinnState)NPC.ai[0]; set => NPC.ai[0] = (float)value; }
        public ref float Timer => ref NPC.ai[1];
        public bool AttackFlag = false;

        public Player Target => Main.player[NPC.target];

        private int FrameX = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Djinn");
            Main.npcFrameCount[NPC.type] = 9;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<DesertDjinn>());
            Music = MusicManagementSystem.MusicSlots["Djinn"];
        }

        public override void AI()
        {
            NPC.TargetClosest();

            switch (CurrentState)
            {
                case DjinnState.Spawn:
                    NPC.velocity = (Target.Center - NPC.Center) / 90f;
                    break;
                case DjinnState.RecoverFlex:
                    break;
                case DjinnState.GrandSlam:
                    break;
                case DjinnState.TwisterPunch:
                    break;
                case DjinnState.SubmergedUppercut:
                    break;
                case DjinnState.Dive:
                    break;
            }
            Timer++;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            NPC.position.X = NPC.position.X + NPC.width / 2;
            NPC.position.Y = NPC.position.Y + NPC.height / 2;
            NPC.position.X = NPC.position.X - NPC.width / 2;
            NPC.position.Y = NPC.position.Y - NPC.height / 2;
            int dust = ModContent.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                Main.dust[d].velocity.Y = hit.HitDirection * 0.1F;
                Main.dust[d].noGravity = false;
            }
            if (NPC.life <= 0)
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore4").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DjinnGore5").Type, 1f);
                }
                for (int Loop = 0; Loop < 60; Loop++)
                {
                    int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                    Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].noGravity = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Width = TextureAssets.Npc[NPC.type].Width() / 6;
            NPC.frameCounter++;

            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }

            int frameCap = FrameX switch
            {
                0 => 6,
                1 => 6,
                2 => 4,
                3 => 4,
                4 => 1,
                5 => 9,
                _ => 1
            };

            if (NPC.frame.Y / frameHeight >= frameCap)
                NPC.frame.Y = 0;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            NPC.spriteDirection = NPC.direction;

            if (!Target.ZoneDesert)
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, texture, NPC.Center - Main.screenPosition, NPC.velocity, 7, NPC.frame, Color.Goldenrod, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection());

            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            return false;
        }
    }
}
