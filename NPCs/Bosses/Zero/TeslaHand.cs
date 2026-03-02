using System;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class TeslaHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Weapon");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 44;
            NPC.damage = 56;
            NPC.defense = 55;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.lifeMax = 30000;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.dontTakeDamage = true;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;

        public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("Zero").Type, 1000, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != Mod.Find<ModNPC>("Zero").Type) { NPC.active = false; return; }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int probeNumber = ((Zero)zero.ModNPC).WeaponCount;
            if (rotValue == -1f) rotValue = NPC.ai[0] % probeNumber * ((float)Math.PI * 2f / probeNumber);
            rotValue += Main.expertMode ? .05f : 0f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            NPC.Center = BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(((Zero)zero.ModNPC).Distance, 0f), rotValue);

            if (Main.netMode != NetmodeID.MultiplayerClient) { NPC.ai[2]++; }

            Player player = Main.player[zero.target];

            int aiTimerFire = Main.expertMode ? 120 : 180;

            if (zero.ai[0] > 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.active = false;
                NPC.netUpdate = true;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient) { NPC.ai[2]++; }

            if (NPC.ai[2] == aiTimerFire)
            {
                NPC.ai[2] = 0;
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.Center, player.width, player.height))
                {
                    float spread = 45f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                    dir *= 14f;
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < Main.rand.Next(4); i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.ProjType("Static"), (int)(NPC.damage / 1.5f), 5, Main.myPlayer);
                    }
                }
            }

            Vector2 vector2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
            float num1 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - vector2.X;
            float num2 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - vector2.Y;
            float NewRotation = (float)Math.Atan2(num2, num1) - 1.57f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, NewRotation, 1f / 30f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = Mod.GetTexture("Glowmasks/TeslaHandZ");
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC, 1, 1, 6, true, 0, 0, Color.DarkRed, NPC.frame);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, AAColor.COLOR_WHITEFADE1);
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}