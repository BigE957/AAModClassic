using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Boss.Sagittarius
{
    public class SagCore : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Sagittarius Core");
            BaseUtility.AddTooltips(Item, new string[] { "Activates probes that orbit you and defend you from surrounding enemies" });			
		}		

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.maxStack = 1;
            Item.rare = 4;
            Item.value = BaseUtility.CalcValue(0, 0, 60, 0);
            Item.useStyle = 1;
            Item.useAnimation = 35;
            Item.useTime = 35;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.shoot = Mod.ProjType("OrbiterMinion");
            Item.shootSpeed = 5;
            Item.damage = 50;
            Item.mana = 10;
        }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Mod.Find<ModBuff>("SagOrbiter").Type, 2, true);
			}
		}
    }
}