using System.Collections.Generic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Thorium.Healer
{
	public class CarrotFarmer : CrossoverItem
	{
		public override void SetStaticDefaults()
		{
			crossoverModName = "ThoriumMod";
            // DisplayName.SetDefault("Carrot Farmer");
            /* Tooltip.SetDefault(@"Spins a Carrot Scythe around you that shreds through enemies
Scythes fire off carrots while spun
Grants 1 soul essence on direct hit"); */			
		}

		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.value = BaseUtility.CalcValue(0, 10, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item1;
            Item.damage = 80;
            Item.knockBack = 9;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("CarrotFarmer").Type;
            Item.shootSpeed = 0.1f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("CarrotFarmerEffect").Type, damage, knockBack, player.whoAmI, k, 0f);
			}
			return true;
		}

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            mult *= ((ModSupportPlayer)player.GetModPlayer(Mod, "ModSupportPlayer")).Thorium_radiantBoost;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(Mod, "ModSupportPlayer")).Thorium_radiantCrit)
			{
				crit = true;
			}
		}

        public override void UpdateInventory(Player player)
        {
            if (ModSupport.GetMod("ThoriumMod") == null)
            {
                Item.TurnToAir();
            }
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int index = -1, index2 = -1;
            for (int m = 0; m < list.Count; m++)
            {
                if (list[m].Name.Equals("Damage")) { index = m; continue; }
                if (list[m].Name.Equals("Tooltip0")) { index2 = m; continue; }		
				if(index > -1 && index2 > -1) break;
            }
            string oldTooltip = list[index].Text;
            string[] split = oldTooltip.Split(' '); 
            list.RemoveAt(index);
            list.Insert(index, new TooltipLine(Mod, "Damage", split[0] + " radiant damage"));
            TooltipLine colorLine = new TooltipLine(Mod, "Healer", "-Healer Class-")
            {
                OverrideColor = new Color(255, 255, 91)
            };
            list.Insert(index2, colorLine);
			base.ModifyTooltips(list);
        }
    }
}