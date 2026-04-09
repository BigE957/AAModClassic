using System.Linq;
using AAModClassic.Buffs;
using AAModClassic.Projectiles.Serpent;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Serpent
{
    public class SnakeStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snake Staff");
            /* Tooltip.SetDefault(@"Summons a Snow Serpent to fight for you
Summons 2 segments for each minion slot"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.damage = 11;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<SerpentHead>();
            Item.width = 26;
            Item.height = 28;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.buffType = ModContent.BuffType<SnakeMinion_Buff>();
            Item.DamageType = DamageClass.Summon;
        }

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //to fix tail disapearing meme
            float slotsUsed = 0;

            Main.projectile.Where(x => x.active && x.owner == player.whoAmI && x.minionSlots > 0).ToList().ForEach(x => { slotsUsed += x.minionSlots; });

            if (player.maxMinions - slotsUsed < 1) return false;

            int headCheck = -1;
            int tailCheck = -1;

            for (int i = 0; i < 1000; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == player.whoAmI)
                {
                    if (headCheck == -1 && proj.type == ModContent.ProjectileType<SerpentHead>()) headCheck = i;
                    if (tailCheck == -1 && proj.type == ModContent.ProjectileType<SerpentTail>()) tailCheck = i;
                    if (headCheck != -1 && tailCheck != -1) break;
                }
            }

            //initial spawn
            if (headCheck == -1 && tailCheck == -1)
            {
                int current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, 0, 0, ModContent.ProjectileType<SerpentHead>(), damage, knockback, player.whoAmI, 0f, 0f);

                int previous = 0;

                for (int i = 0; i < 3; i++)
                {
                    current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, 0, 0, ModContent.ProjectileType<SerpentBody>(), damage, knockback, player.whoAmI, current, 0f);
                    previous = current;
                }

                current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, 0, 0, ModContent.ProjectileType<SerpentTail>(), damage, knockback, player.whoAmI, current, 0f);

                Main.projectile[previous].localAI[1] = current;
                Main.projectile[previous].netUpdate = true;
            }
            //spawn more body segments
            else
            {
                int previous = (int) Main.projectile[tailCheck].ai[0];
                int current = 0;

                for (int i = 0; i < 2; i++)
                {
                    current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<SerpentBody>(), damage, knockback, player.whoAmI,
                        Projectile.GetByUUID(Main.myPlayer, previous), 0f);

                    previous = current;
                }

                Main.projectile[current].localAI[1] = tailCheck;

                Main.projectile[tailCheck].ai[0] = current;
                Main.projectile[tailCheck].netUpdate = true;
                Main.projectile[tailCheck].ai[1] = 1f;
            }

            return false;
        }
    }
}
