using AAModClassic._Content.Desert.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Hell.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Void.World.Tiles;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AAGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public static int CountProjectiles(int type)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    num++;
                }
            }

            return num;
        }

        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static float GetSyncedItemAnimation(Projectile projectile, Player player)
        {
            float itemAnimation = player.itemAnimation;

            if (Main.netMode != NetmodeID.SinglePlayer && Main.myPlayer == projectile.owner)
            {
                if (projectile.ai[1] != itemAnimation)
                {
                    projectile.ai[1] = itemAnimation;
                    projectile.netUpdate = true;
                }
            }

            if (Main.netMode == NetmodeID.SinglePlayer || Main.myPlayer == projectile.owner)
                return itemAnimation;

            if (projectile.ai[1] > 0f)
                projectile.localAI[1] = 1f;

            if (projectile.localAI[1] == 1f)
                return projectile.ai[1];

            return Math.Max(1f, player.itemAnimationMax);
        }

        public override void PostAI(Projectile projectile)
        {
            if (isReflecting && projectile.hostile && !projectile.friendly)
            {
                oldvelocity = projectile.velocity;
                projectile.velocity = reflectvelocity;
                projectile.rotation += projectile.velocity.ToRotation() - oldvelocity.ToRotation();
            }
            if (!projectile.minion && projectile.type > ProjectileID.None && !projectile.CountsAsClass(DamageClass.Melee) && !projectile.CountsAsClass(DamageClass.Magic) && !projectile.CountsAsClass(DamageClass.Ranged))
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (Main.projectile[j].active && Main.projectile[j].sentry && Main.projectile[j].type + 1 == projectile.type)
                    {
                        projectile.minion = true;
                        break;
                    }
                }
            }
            if ((projectile.minion || projectile.sentry) && !ProjectileID.Sets.StardustDragon[projectile.type] && !LongMinion)
			{
				if (setDefMinionDamage)
				{
					DefMinionDamageMultiply = Main.player[projectile.owner].GetDamage(DamageClass.Summon).Multiplicative;
					DefMinionDamage = (int)(projectile.damage / DefMinionDamageMultiply);
					setDefMinionDamage = false;
				}
				if (Main.player[projectile.owner].GetDamage(DamageClass.Summon).Flat != DefMinionDamageMultiply)
				{
					int damage = (int)(Main.player[projectile.owner].GetDamage(DamageClass.Summon)).ApplyTo(DefMinionDamage);
                    if(damage <= 0) damage = 1;
					projectile.damage = damage;
				}
			}
            if (projectile.type == ProjectileID.PureSpray)
            {
                Convert((int)(projectile.position.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16);
            }

            base.PostAI(projectile);
        }


        public static void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        if (Main.tile[k, l].TileType == ModContent.TileType<InfernoGrass_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<MireGrass_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Mycelium_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DoomGrass_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Grass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DoomstoneB_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchsand_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthsand_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<TorchsandHardened_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DepthsandHardened_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchsandstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthsandstone_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchice_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<IndigoIce_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }


        public Vector2 reflectvelocity = Vector2.Zero;

        private Vector2 oldvelocity = Vector2.Zero;

        public bool isReflecting = false;

        private bool setDefMinionDamage = true;

        public bool LongMinion = false;

        public float DefMinionDamageMultiply = 1f;

		public int DefMinionDamage;
    }
}
