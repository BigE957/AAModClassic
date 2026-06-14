using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons
{
    public class TotalDestruction : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Total Destruction");
            // Tooltip.SetDefault("Destroys everything in front of you with a destructive laser");
        }

        public override void SetDefaults()
        {            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 7;
            Item.useTime = 7;
            Item.mana = 10;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 122;
            Item.reuseDelay = 5;
            Item.height = 32;
            Item.damage = 250;
            Item.UseSound = SoundID.Item13;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<TotalDestruction_Proj>();
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-53, -4);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }
        
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<NovaFocus>());
			recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
	        recipe.AddTile(ModContent.TileType<ACS_Tile>());
	        recipe.Register();
		}
	}
}
