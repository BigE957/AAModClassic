using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class Neutralizer: ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Neutralizer");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 70;
            NPC.damage = 55;
            NPC.defense = 90;
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
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossLifeScale);
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool flag = NPC.life <= 0 || (!NPC.active && NPC.AnyNPCs(ModContent.NPCType<Zero>()));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(NPC.position.X + (double)(NPC.width / 2)), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("TeslaHand").Type, NPC.whoAmI, NPC.ai[0], NPC.ai[1], NPC.ai[2], NPC.ai[3], NPC.target);
                Main.npc[ind].Center = NPC.Center;
                Main.npc[ind].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                Main.npc[ind].velocity *= 8f;
                Main.npc[ind].netUpdate2 = true; Main.npc[ind].netUpdate = true;
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;

        public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("Zero").Type, -1, null);
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

            if (Main.netMode != 1) { NPC.ai[2]++; }

            Player player = Main.player[zero.target];

            int aiTimerFire = Main.expertMode ? 190 : 250;

            if (Main.netMode != 1) { NPC.ai[2]++; }

            if (NPC.ai[2] >= aiTimerFire)
            {
                int Arrows = Main.rand.Next(2, 5);
                float spread = 45f * 0.0174f;
                Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                dir *= 14;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / Arrows * 2;
                for (int i = 0; i < Arrows; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("ZArrow").Type, NPC.damage / 2, 5, Main.myPlayer);
                }
                NPC.netUpdate = true;
                NPC.ai[2] = 0;
            }
            Vector2 vector2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
            float num1 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - vector2.X;
            float num2 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - vector2.Y;
            NPC.rotation = (float)Math.Atan2(num2, num1) - 1.57f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = Mod.GetTexture("Glowmasks/Neutralizer2_Glow");
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