using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Bosses.Anubis
{
    public class HorusHawk : ModNPC
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Horus Hawk");
            Main.npcFrameCount[NPC.type] = 4;
		}

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 38;
            NPC.value = BaseUtility.CalcValue(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 500;
            NPC.defense = 30;
            NPC.damage = 40;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath35;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode == 2) { return; }
			for (int m = 0; m < (NPC.life <= 0 ? 30 : 8); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, 1.1f);
			}		
		}

		public override void FindFrame(int dummy)
        {
            NPC.frameCounter++;
            if (dash)
            {
                NPC.frame.Y = dummy;
            }
            if (NPC.frameCounter >= 2)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += dummy;
                if (NPC.frame.Y > dummy * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }
        bool dash = false;
		public override void AI()
		{
            dash = false;

            NPC.TargetClosest(true);

			Player player = Main.player[NPC.target];

            NPC.direction = NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;

            switch (NPC.ai[2])
            {
                case 0:
                    BaseAI.AISkull(NPC, ref NPC.ai, false, 4, 250, .011f, .22f);
                    break;
                case 1:
                    if (++NPC.ai[3] > 30)
                    {
                        Vector2 targetPos = player.Center;
                        targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                        DashMovement(targetPos, 0.8f);
                        if (NPC.ai[3] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 50) //initiate dash
                        {
                            NPC.ai[2]++;
                            NPC.ai[3] = 0;
                            NPC.netUpdate = true;
                            NPC.velocity.X = -30 * (NPC.Center.X < player.Center.X ? -1 : 1);
                            NPC.velocity.Y *= 0.1f;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f; //decelerate briefly
                    }
                    NPC.rotation = 0;
                    break;

                case 2: //dashing
                    dash = true;
                    if (++NPC.ai[3] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 600 : NPC.Center.X < player.Center.X - 600))
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                default:
                    NPC.ai[2] = 0;
                    goto case 0;
            }
            NPC.rotation = 0;
		}

        private void DashMovement(Vector2 targetPos, float speedModifier)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > 30)
                NPC.velocity.X = 30 * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > 30)
                NPC.velocity.Y = 30 * Math.Sign(NPC.velocity.Y);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D bodyTex = TextureAssets.Npc[NPC.type].Value;
            Color lightColor = BaseDrawing.GetNPCColor(NPC, null);
            BaseDrawing.DrawTexture(sb, bodyTex, 0, NPC, lightColor);
			return false;
		}
	}
}