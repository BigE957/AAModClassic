using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    [AutoloadBossHead]
    public class ZeroBrokenWeapon : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Weapon");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
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
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
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
            NPC.TargetClosest();
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<Zero>(), 1000, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != ModContent.NPCType<Zero>()) { NPC.active = false; return; }

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
                    float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    for (int i = 0; i < Main.rand.Next(4); i++)
                    {
                        double offsetAngle = startAngle + deltaAngle * i;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<ZeroA_StaticShock>(), (int)(NPC.damage / 1.5f), 5, Main.myPlayer);
                    }
                }
            }

            float NewRotation = NPC.AngleTo(Main.player[NPC.target].Center) - 1.57f;
            NPC.rotation = NPC.rotation.AngleLerp(NewRotation, 1f / 30f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = Glowmask.Value;
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC, 1, 1, 6, true, 0, 0, Color.DarkRed, NPC.frame);
            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, AAColor.COLOR_WHITEFADE1, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}