using AAModClassic._Content.Void.Projectiles;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.__Hardmode.NPCs
{
    public class VoidScout : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Scout");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
            NPC.width = 38;
            NPC.height = 38;
            NPC.value = 0;
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200;
            NPC.defense = 120;
            NPC.damage = 80;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0.3f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.VoidScoutBanner>();
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

		public override void HitEffect(NPC.HitInfo hit)
		{		
			bool isDead = NPC.life <= 0;
			for (int m = 0; m < (isDead ? 25 : 5); m++)
			{
				int dustType = ModContent.DustType<Dusts.VoidDust>();
				Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
			}
		}

		float shootAI = 0;
		public override void AI()
		{
		    BaseAI.AISkull(NPC, ref NPC.ai, false, 6f, 350f, 0.6f, 0.15f);
			Player player = Main.player[NPC.target];
			bool playerActive = player != null && player.active && !player.dead;
            if (shootAI < 60)
            {
                NPC.rotation = NPC.rotation.AngleTowards(NPC.AngleTo(player.Center), 0.1f);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && playerActive)
			{
				shootAI++;
				if(shootAI >= 90)
				{
					shootAI = 0;
                    int projType = ModContent.ProjectileType<DeathBeam>();

                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
                    {
                        Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, NPC.rotation.ToRotationVector2() * 4, projType, (int)(NPC.damage * 0.25f), 3f, Main.myPlayer, NPC.whoAmI);
						p.friendly = false;
						p.hostile = true;
                    }
                }
			}
		}

		public override void FindFrame(int frameHeight)
		{
			if (NPC.frameCounter++ > 5)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y += frameHeight;
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frame.Y = 0;
				}
			}
		}

		/*
		public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<VoidEnergy>(), Main.rand.Next(1, 4));
        }
		*/

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            spriteBatch.Draw(texture2D13, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(GlowTex, NPC.Center - screenPos, NPC.frame, AAColor.ZeroShield, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}