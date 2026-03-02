using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class ElectricitySpell : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.mana = 20;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ElectricitySpellP").Type;  
            Item.shootSpeed = 11f;     
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Electricity Shard");
          // Tooltip.SetDefault("It shoots sparks in an even spread.");
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		float spread = 45f * 0.0174f;
		float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
		double startAngle = Math.Atan2(speedX, speedY)- (spread/2);
		double deltaAngle = spread/5f;
		double offsetAngle;
		int i;
		for (i = 0; i < 5;i++ )
		{
			offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockBack, Main.myPlayer);
		}
		return false;
		}
	}

    public class SpellDrop : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.AngryNimbus && Main.rand.Next(6) == 0)
            {
                npc.DropLoot(ModContent.ItemType<ElectricitySpell>());
            }
        }
    }

}
