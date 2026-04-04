using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic.Items.Boss.AH
{
    public class AshRain : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ash Rain");
			/* Tooltip.SetDefault(@"Shoots fireball which explodes on hit or after some time
Right click to detonate fireballs"); */
        }

        public override void ModifyTooltips(List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.damage = 315;                        
            Item.DamageType = DamageClass.Magic;            
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;    
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            Item.mana = 5;
            Item.autoReuse = true;
            Item.shootSpeed = 11f;
        }

        
        private readonly List<int> AshRainFire = new List<int>();
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse != 2)
            {
                Item.shoot = ModContent.ProjectileType<FireMagic>();
                Item.UseSound = SoundID.Item20;
            }
            if (player.altFunctionUse == 2)
            {
                foreach(int P in AshRainFire)
                {
                    if(Main.projectile[P].type == ModContent.ProjectileType<FireMagic>()) Main.projectile[P].Kill();
                }
                Item.UseSound = null;
                AshRainFire.Clear();
            }
            return true;
        }
        
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse != 2)
			{
				int P = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<FireMagic>(), damage, knockback, player.whoAmI, 0f, 0f);
                AshRainFire.Add(P);
			}
			return false;
		}
    }
}
