using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Mire.Buffs;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord.GripOfAbyssalWrath
{
    public class AbyssGripOrbiter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyss Grip Orbiter");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 255;
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
        }

        public float rotValue = -1f;

        public int timecount = 0;

        public int proj = 0;

        public bool shooting = false;

        public override void AI()
        {
            Vector2 Center = new Vector2();
            NPC centerNPC = Main.npc[(int)Projectile.ai[0]];

            if(proj == 0) Center = centerNPC.Center;

            timecount ++;

            for(int i = 0; i<1000; i++)
            {
                if(Main.projectile[i].friendly && !Main.projectile[i].minion && Main.projectile[i].Hitbox.Intersects(Projectile.Hitbox))
                {
                    Main.projectile[i].Kill();
                }
                if(Main.projectile[i].type == ModContent.ProjectileType<AbyssalBomb_Proj>() && proj == 0)
                {
                    Center = Main.projectile[i].Center;
                    proj = Main.projectile[i].whoAmI;
                }
            }

            if (rotValue == -1f) rotValue = Projectile.ai[1];
            rotValue += 0.05f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            



            if(proj != 0 && !shooting)
            {
                if(Main.projectile[proj].timeLeft <= 5 || !Main.projectile[proj].active) 
                {
                    Projectile.velocity = 18f * Vector2.Normalize(Main.projectile[proj].DirectionTo(Projectile.Center));
                    shooting = true;
                }
                else
                {
                    for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
                    {
                        Projectile.oldPos[m] = Projectile.oldPos[m - 1];
                    }
                    Projectile.oldPos[0] = Projectile.position;
                    Center = Main.projectile[proj].Center;
                    Projectile.Center = BaseUtility.RotateVector(Center, Center + new Vector2(140f, 0f), rotValue);
                }
            }
            else if(centerNPC.active && centerNPC.life > 0 && centerNPC.type == ModContent.NPCType<AbyssGrip>() && !shooting)
            {
                for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
                {
                    Projectile.oldPos[m] = Projectile.oldPos[m - 1];
                }
                Projectile.oldPos[0] = Projectile.position;
                Projectile.Center = BaseUtility.RotateVector(Center, Center + new Vector2(140f, 0f), rotValue);
            }
            else if((!centerNPC.active || centerNPC.life <= 0 || centerNPC.type != ModContent.NPCType<AbyssGrip>()) && !shooting)
            {
                Projectile.velocity = 18f * Vector2.Normalize(centerNPC.DirectionTo(Projectile.Center));
                shooting = true;
            }
            
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 200);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color Alpha = lightColor;
            if(timecount < 10)
            {
                Alpha.R = (byte)0f;
                Alpha.G = (byte)0f;
                Alpha.B = (byte)0f;
                Alpha.A = (byte)0f;
            }
            else if(timecount < 100)
            {
                Alpha.R = (byte)(float)(timecount * 2);
                Alpha.G = (byte)(float)(timecount * 2);
                Alpha.B = (byte)(float)(timecount * 2);
                Alpha.A = (byte)(float)(timecount * 2);
            }
            else
            {
                Alpha.R = (byte)200f;
                Alpha.G = (byte)200f;
                Alpha.B = (byte)200f;
                Alpha.A = (byte)200f;
            }
            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, blue, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}
