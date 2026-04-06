using AAModClassic.Items.ReforgeSouls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AAModReforgeSouls : GlobalItem
	{
        public override bool CanRightClick(Item item)
		{
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == ModContent.ItemType<Godly>() && reforgable) ||
                (Main.mouseItem.type == ModContent.ItemType<Legendary>() && reforgable && item.CountsAsClass(DamageClass.Melee)) || 
                (Main.mouseItem.type == ModContent.ItemType<Unreal>() && reforgable && (item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Throwing)) && item.ammo == AmmoID.None) ||
                (Main.mouseItem.type == ModContent.ItemType<Mythical>() && reforgable && (item.CountsAsClass(DamageClass.Summon) || item.CountsAsClass(DamageClass.Magic))))
			{
				return true;
			}
            return base.CanRightClick(item);
		}


		public override void RightClick(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == ModContent.ItemType<Godly>() && reforgable) ||
                (Main.mouseItem.type == ModContent.ItemType<Legendary>() && reforgable && item.CountsAsClass(DamageClass.Melee)) ||
                (Main.mouseItem.type == ModContent.ItemType<Unreal>() && reforgable && (item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Throwing)) && item.ammo == AmmoID.None) ||
                (Main.mouseItem.type == ModContent.ItemType<Mythical>() && reforgable && (item.CountsAsClass(DamageClass.Summon) || item.CountsAsClass(DamageClass.Magic))))
            { 
                Main.mouseItem.stack = 0;
			}
        }
		
		public override bool ConsumeItem(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;
            if (Main.mouseItem.type == ModContent.ItemType<Godly>() && reforgable)
			{
				Main.mouseItem.stack--;
				Item.NewItem(Item.GetSource_NaturalSpawn(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 59);
			}
			if (Main.mouseItem.type == ModContent.ItemType<Legendary>() && reforgable && item.CountsAsClass(DamageClass.Melee))
			{
				Main.mouseItem.stack--;
				Item.NewItem(Item.GetSource_NaturalSpawn(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 81);
			}
			if (Main.mouseItem.type == ModContent.ItemType<Unreal>() && reforgable && (item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Throwing)) && item.ammo == AmmoID.None)
			{
				Main.mouseItem.stack--;
				Item.NewItem(Item.GetSource_NaturalSpawn(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 82);
			}
			if (Main.mouseItem.type == ModContent.ItemType<Mythical>() && reforgable && (item.CountsAsClass(DamageClass.Summon) || item.CountsAsClass(DamageClass.Magic)))
			{
				Main.mouseItem.stack--;
				Item.NewItem(Item.GetSource_NaturalSpawn(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 83);
			}
            return base.ConsumeItem(item, player);
		}
    }
}
