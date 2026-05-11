using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    internal class OrthrusXHead_ShockingBreath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shocking Breath");
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 15;
            Projectile.aiStyle = -1;
			Projectile.timeLeft = 40; //timed it so all the frames pass through before it dies
			Main.projFrames[Projectile.type] = 14;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		
		int head = -1;
		int frameWidth = 540;
		int frameHeight = 50;

        public override bool PreAI()
        {
			if(head == -1)
			{
				int npcID = BaseAI.GetNPC(Projectile.Center, ModContent.NPCType<OrthrusXHead>(), 500f, null);	
				if(npcID >= 0) head = npcID;
			}
			if(head == -1) return false;				
			NPC headNPC = Main.npc[head];
			if(headNPC == null || headNPC.life <= 0 || !headNPC.active || headNPC.type != ModContent.NPCType<OrthrusXHead>()) 
            { 
                Projectile.Kill(); 
                return false; 
            }

			//Fun fact: this technique is what the shadowbeam staff does!
			if(Main.netMode != NetmodeID.MultiplayerClient && Projectile.timeLeft % 3 == 0) //so it doesn't do this every tick, which would be laggy
			{
				Projectile.Center = headNPC.Center; //reset to start chain movement
				for(int m = 0; m < 18; m++) //this + velocity ends up ~540 in length, same as the texture
				{
					Projectile.Center += Projectile.velocity * 30f; //move to new point in the chain
					Projectile.Damage(); //inflcit damage
				}
			}
			Projectile.Center = headNPC.Center;
            //BaseAI.LookAt(Projectile.Center + Projectile.velocity, Projectile.Center, ref Projectile.rotation, ref Projectile.spriteDirection, 2, 0f, 0.1f, false);
            BaseAI.LookAt(Projectile.Center + Projectile.velocity, Projectile, 2, 0f, 0.1f, false);

            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 14)
                {
					Projectile.frame = 14;
                }
            }
			return false; //so it doesn't add velocity and try to move
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 300);
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOffset = BaseUtility.RotateVector(Vector2.Zero, new Vector2(frameWidth * 0.5f, 0), Projectile.rotation);	
			BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position + drawOffset, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, Main.projFrames[Projectile.type], new Rectangle(0, Projectile.frame * frameHeight, frameWidth, frameHeight), GetAlpha(lightColor), true, default);			
			return false;
		}
    }
}