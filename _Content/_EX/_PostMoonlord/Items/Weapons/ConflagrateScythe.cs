using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    //imported from my tAPI mod because I'm lazy
    public class ConflagrateScythe : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Conflagrate Scythe");
            /* Tooltip.SetDefault(@"Summons a spinning construct that shreds through enemies
Conflagrate Staff EX"); */

            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.damage = 400;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 20;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.shoot = ModContent.ProjectileType<ConflagrateScythe_ConflagrateConstructEX>();
            Item.buffType = ModContent.BuffType<ConflagrateConstructEX_Buff>();
            Item.rare = ItemRarityID.Yellow;
            Item.expert = true; Item.expertOnly = true;
			Item.UseSound = SoundID.Item44;
			Item.shootSpeed = 7f;	//The buff added to player after used the item
        }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ConflagrateStaff>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return player.altFunctionUse != 2;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(true);
            }
            return base.UseItem(player);
        }
    }
}
