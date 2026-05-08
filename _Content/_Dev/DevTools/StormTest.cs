using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content._Dev.DevTools.Cinematic;

namespace AAModClassic._Content._Dev.DevTools
{
    public class StormTest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			// DisplayName.SetDefault("[DEV] Feather Test");
            BaseUtility.AddTooltips(Item, new string[] { "Feathers" });					
		}			
		
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 0;
            Item.rare = ItemRarityID.Purple;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.expert = true; Item.expertOnly = true;
            Item.shootSpeed = 9f;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.noUseGraphic = true;
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int a = 0; a < 8; a++)
            {
                Dust.NewDust(player.Center, player.width, player.height, ModContent.DustType<Feather2>(), Main.rand.Next(-2, 2), 1, 0);
            }
            return false;
		}
    }
}