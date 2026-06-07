using AAModClassic._Content.Chaos.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosChain_Holdout : FlailHoldout
    {
        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override float LaunchSpeed => 32;

        public override int LaunchTimeLimit => 13;

        public override float RetractAcceleration => base.RetractAcceleration;

        public override float MaxRetractSpeed => base.MaxRetractSpeed;


        public static Asset<Texture2D> SawTexture;
        public static Asset<Texture2D> SphereTexture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Chaos Chain");

            SawTexture = ModContent.Request<Texture2D>(ModContent.GetInstance<PerfectChaosChain_Proj>().Texture);
            SphereTexture = ModContent.Request<Texture2D>(Texture + "_Sphere");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            base.SetDefaults();
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            //Projectile.extraUpdates = 1;
        }

        /*
        public float[] InternalAI = new float[2];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(InternalAI[0]);
                writer.Write(InternalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                InternalAI[0] = reader.ReadSingle();
                InternalAI[1] = reader.ReadSingle();
            }
        }
        */

        float Rot = 0;
        int Dir = 1;
		
		public override void AI()
        {
            base.AI();

            if (CurrentAIState == AIState.LaunchingForward)
            {
                Rot += Projectile.velocity.X * 0.05f;
            }

            if(CurrentAIState == AIState.Spinning)
                Projectile.localNPCHitCooldown = 10;
            else
                Projectile.localNPCHitCooldown = 5;

            /*
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<PerfectChaosChain_Proj>() && Projectile.ai[0] == 1f)
                {
                    InternalAI[1] = 1;
                }
            }
            if (Projectile.velocity.X < 0)
            {
                Dir = -1;
            }
            Rot += 0.03f * Projectile.direction;

            if (Projectile.timeLeft == 120)
            {
                Projectile.ai[0] = 1f;
            }

            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }

            Main.player[Projectile.owner].itemAnimation = 5;
            Main.player[Projectile.owner].itemTime = 5;

            if (Projectile.alpha == 0)
            {
                if (Projectile.position.X + Projectile.width / 2 > Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2)
                {
                    Main.player[Projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[Projectile.owner].ChangeDir(-1);
                }
            }
            Projectile.rotation += .4f;
            Vector2 vector14 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num166 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector14.Y;
            float num168 = (float)Math.Sqrt(num166 * num166 + num167 * num167);
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 1000)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<PerfectChaosChain_Proj>(), Projectile.damage, 0, Main.myPlayer);
                    Projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<PerfectChaosChain_Proj>(), Projectile.damage, 0, Main.myPlayer);
                    Projectile.ai[0] = 1f;
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 5f)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.ai[1] > 8f)
                {
                    Projectile.ai[1] = 8f;
                }
                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.ai[1] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                else
                {
                    Projectile.spriteDirection = 1;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    Projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                Projectile.velocity.X = num166;
                Projectile.velocity.Y = num167;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else
                {
                    Projectile.spriteDirection = -1;
                }

            }
            */
        }

        public override void OnEndLaunch()
        {
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 2, ModContent.ProjectileType<PerfectChaosChain_Proj>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (CurrentAIState == AIState.LaunchingForward)
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<PerfectChaosChain_Proj>(), Projectile.damage, 0, Main.myPlayer);

            target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 240);
        }

        /*
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 24;
            height = 24;
            return true;
        }
        */

        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
            /*
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Chains/ChaosChainEX_Chain").Value;
            BaseDrawing.DrawChain(Main.spriteBatch, texture, Projectile.Center, Main.player[Projectile.owner].Center, 0f, lightColor, 1f);
            Texture2D headTex = Projectile.ai[0] == 1f ? SphereTexture.Value : SawTexture.Value;
            Rectangle frame = new(0, 0, SawTexture.Value.Width, SawTexture.Value.Height);
            BaseDrawing.DrawTexture(Main.spriteBatch, headTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Rot, Dir, 1, frame, lightColor, true);
            return true;
            */
        }

        public override bool PreDrawFlail(SpriteBatch spriteBatch, Color lightColor, ref SpriteEffects spriteEffects)
        {
            if (spriteEffects == SpriteEffects.None)
                spriteEffects = SpriteEffects.FlipVertically;
            Texture2D headTex = (CurrentAIState == AIState.LaunchingForward || CurrentAIState == AIState.Spinning) ? SawTexture.Value : SphereTexture.Value;
            spriteBatch.Draw(headTex, Projectile.Center - Main.screenPosition, null, lightColor, Rot, headTex.Size() * 0.5f, Projectile.scale, spriteEffects, 0);
            return true;
        }
    }
}