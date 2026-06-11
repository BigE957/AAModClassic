using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    //imported from my tAPI mod because I'm lazy
    public class ConflagrateStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Conflagrate Staff");
            /* Tooltip.SetDefault(@"Summons a spinning construct that shreds through enemies
I thought the sky was purple
-Ender"); */

            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.damage = 180;
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
			Item.rare = ItemRarityID.Yellow;
            Item.expert = true; Item.expertOnly = true;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<ConflagrateStaff_ConflagrateConstruct>();
			Item.shootSpeed = 7f;
			Item.buffType = ModContent.BuffType<ConflagrateStaff_Buff>();	//The buff added to player after used the item
		}
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
		
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(5, 158, 130);
                }
            }
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
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(true);
			}
			return base.UseItem(player);
		}
    }
}
