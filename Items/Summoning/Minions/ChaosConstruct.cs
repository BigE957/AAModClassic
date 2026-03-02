using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    public class ChaosConstruct : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Construct");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.width = 44;
			Projectile.height = 44;
			Projectile.timeLeft = 18000;
			Projectile.timeLeft *= 5;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft *= 5;
			Projectile.minion = true;
			Projectile.netImportant = true;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
		}
		public override void AI()
		{
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("ChaosConstruct").Type;
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			player.AddBuff(Mod.Find<ModBuff>("ChaosConstruct").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.ChaosConstruct = false;
				}
				if (modPlayer.ChaosConstruct)
				{
					Projectile.timeLeft = 2;
				}
			}

			float num633 = 700f;
			float num634 = 800f;
			float num635 = 1200f;
			float num636 = 150f;
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = Main.projectile[num638].type == Mod.Find<ModProjectile>("ChaosConstruct").Type;
				if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < Projectile.width)
				{
					if (Projectile.position.X < Main.projectile[num638].position.X)
					{
						Projectile.velocity.X = Projectile.velocity.X - num637;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X + num637;
					}
					if (Projectile.position.Y < Main.projectile[num638].position.Y)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num637;
					}
					else
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num637;
					}
				}
			}
			bool flag24 = false;
			if (flag24)
			{
				return;
			}
			Vector2 vector46 = Projectile.position;
			bool flag25 = false;
			if (Projectile.ai[0] != 1f)
			{
				Projectile.tileCollide = false;
			}
			if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
			{
				Projectile.tileCollide = false;
			}
			if (player.HasMinionAttackTargetNPC)
			{
				NPC nPC2 = Main.npc[player.MinionAttackTargetNPC];
                if (nPC2.CanBeChasedBy(Projectile, false))
				{
					float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
					if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
			}
			else
			{
				for (int num645 = 0; num645 < 200; num645++)
				{
					NPC nPC2 = Main.npc[num645];
					if (nPC2.CanBeChasedBy(Projectile, false))
					{
						float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
						if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
						{
							num633 = num646;
							vector46 = nPC2.Center;
							flag25 = true;
						}
					}
				}
			}
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > num647)
			{
				Projectile.ai[0] = 1f;
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			if (flag25 && Projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - Projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 6f;
					vector47 *= scaleFactor2;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num649 = 4f;
					vector47 *= -num649;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = Projectile.ai[0] == 1f;
				}
				float num650 = 6f;
				if (flag26)
				{
					num650 = 15f;
				}
				Vector2 center2 = Projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 8f)
				{
					num650 = 8f;
				}
				if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
					Projectile.netUpdate = true;
				}
				if (num651 > 70f)
				{
					vector48.Normalize();
					vector48 *= num650;
					Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
				}
				else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
				{
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}

			Projectile.rotation += Projectile.velocity.X * .01f;

			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 160f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 8f;
				if (flag25 && Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - Projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, ModContent.ProjectileType<ChaosConstructShot>(), (int)(Projectile.damage * 0.8f), 0f, Main.myPlayer, 0f, Main.rand.Next(2));
						Projectile.netUpdate = true;
					}
				}
			}
        }

		public float rot = 0;

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
			Texture2D Glow = Mod.GetTexture("Items/Summoning/Minions/ChaosConstructEye");

			Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);

			BaseDrawing.DrawTexture(spritebatch, Glow, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, 0, 0, 4, frame, Color.White, true);
			BaseDrawing.DrawTexture(spritebatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, lightColor, true);
			return false;
		}
	}
}
