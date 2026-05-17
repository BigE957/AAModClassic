using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.ShenDoragonUtils;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public abstract class ShenDoragon_ChaosFireballAbstract : ModProjectile
    {
        public override string Texture => "AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/__BossShenDoragon/ShenDoragon_ChaosFireballAbstract";
        
        public ChaosType Chaos = ChaosType.Discord;
        public bool IsSmall = false;
        public Rectangle BetterFrame = new Rectangle();

        private const int FRAMECOUNT_X = 6;
        private const int FRAMECOUNT_Y = 4;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                // hitboxes slightly smaller for fairness. both smaller by 8 px from real frame size
                if (IsSmall)
                {
                    Projectile.width = 24;
                    Projectile.height = 36;
                }
                else
                {
                    Projectile.width = 34;
                    Projectile.height = 32;
                }
            }

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustType = Chaos == ChaosType.Inferno ? ModContent.DustType<AkumaDust>() : Chaos == ChaosType.Mire ? ModContent.DustType<YamataDust>() : ModContent.DustType<Discord_Dust>();
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Chaos == ChaosType.Inferno)
                target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 180);
            else if (Chaos == ChaosType.Mire)
                target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 180);
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }

            int frameWidth = TextureAssets.Projectile[Projectile.type].Value.Width / FRAMECOUNT_X;
            int frameHeight = TextureAssets.Projectile[Projectile.type].Value.Height / FRAMECOUNT_Y;
            int horizFrame = Chaos == ChaosType.Inferno ? 0 : Chaos == ChaosType.Mire ? 1 : 2;
            BetterFrame = new Rectangle(horizFrame * frameWidth, Projectile.frame * frameHeight, frameWidth, frameHeight);
            if (IsSmall)
                BetterFrame.X += TextureAssets.Projectile[Projectile.type].Value.Width / 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, BetterFrame, Color.White, Projectile.rotation, BetterFrame.Size() / 2, Projectile.scale, 0, 0);
            return false;
        }
    }
}
