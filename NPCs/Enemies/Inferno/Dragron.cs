using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class Dragron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pigron");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[170];
		}

		public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 36;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 12;
            NPC.lifeMax = 210;
            NPC.HitSound = SoundID.NPCHit27;
            NPC.DeathSound = SoundID.NPCDeath30;
            NPC.knockBackResist = 0.5f;
            NPC.value = 2000f;
            AnimationType = NPCID.PigronCorruption;
            NPC.buffImmune[31] = false;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("DragronBanner").Type;
        }


        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            if (Main.rand.Next(1000) == 0)
            {
                SoundEngine.PlaySound(SoundID.Zombie9, NPC.position);
            }
            NPC.noGravity = true;
            if (!NPC.noTileCollide)
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                    {
                        NPC.velocity.X = 2f;
                    }
                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                    {
                        NPC.velocity.X = -2f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                    {
                        NPC.velocity.Y = 1f;
                    }
                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                    {
                        NPC.velocity.Y = -1f;
                    }
                }
            }
            NPC.TargetClosest(true);
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                if (NPC.ai[1] > 0f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[1] = 0f;
                    NPC.ai[0] = 0f;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[1] == 0f)
            {
                NPC.ai[0] += 1f;
            }
            if (NPC.ai[0] >= 300f)
            {
                NPC.ai[1] = 1f;
                NPC.ai[0] = 0f;
                NPC.netUpdate = true;
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.alpha = 0;
                NPC.noTileCollide = false;
            }
            else
            {
                NPC.wet = false;
                NPC.alpha = 200;
                NPC.noTileCollide = true;
            }
            NPC.rotation = NPC.velocity.Y * 0.1f * NPC.direction;
            NPC.TargetClosest(true);
            if (NPC.direction == -1 && NPC.velocity.X > -4f && NPC.position.X > Main.player[NPC.target].position.X + Main.player[NPC.target].width)
            {
                NPC.velocity.X = NPC.velocity.X - 0.08f;
                if (NPC.velocity.X > 4f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.04f;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.2f;
                }
                if (NPC.velocity.X < -4f)
                {
                    NPC.velocity.X = -4f;
                }
            }
            else if (NPC.direction == 1 && NPC.velocity.X < 4f && NPC.position.X + NPC.width < Main.player[NPC.target].position.X)
            {
                NPC.velocity.X = NPC.velocity.X + 0.08f;
                if (NPC.velocity.X < -4f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.04f;
                }
                else if (NPC.velocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f;
                }
                if (NPC.velocity.X > 4f)
                {
                    NPC.velocity.X = 4f;
                }
            }
            if (NPC.directionY == -1 && NPC.velocity.Y > -2.5 && NPC.position.Y > Main.player[NPC.target].position.Y + Main.player[NPC.target].height)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                if (NPC.velocity.Y > 2.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.05f;
                }
                else if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.15f;
                }
                if (NPC.velocity.Y < -2.5)
                {
                    NPC.velocity.Y = -2.5f;
                }
            }
            else if (NPC.directionY == 1 && NPC.velocity.Y < 2.5 && NPC.position.Y + NPC.height < Main.player[NPC.target].position.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                if (NPC.velocity.Y < -2.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.05f;
                }
                else if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                }
                if (NPC.velocity.Y > 2.5)
                {
                    NPC.velocity.Y = 2.5f;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            if (NPC.life > 0)
            {
                int num589 = 0;
                while (num589 < damage / NPC.lifeMax * 50.0)
                {
                    int num590 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.BroodmotherDust>(), 0f, 0f, 0, default, 1.5f);
                    Main.dust[num590].velocity *= 1.5f;
                    Main.dust[num590].noGravity = true;
                    num589++;
                }
                return;
            }
            for (int num591 = 0; num591 < 10; num591++)
            {
                int num592 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.BroodmotherDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num592].velocity *= 2f;
                Main.dust[num592].noGravity = true;
            }
            for (int num593 = 0; num593 < 4; num593++)
            {
                int num594 = Gore.NewGore(new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(hitDirection, 0f), 99, NPC.scale);
                Main.gore[num594].velocity *= 0.3f;
            }
        }

		public override void OnKill()
		{
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 3532, 1, false, 0, false, false);
            }
        }
	}
}