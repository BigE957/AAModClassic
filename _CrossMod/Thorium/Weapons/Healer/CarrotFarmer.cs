using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._CrossMod.Thorium.Weapons.Healer
{
	public class CarrotFarmer : CrossoverItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.CrossMod.Healer";
        public override string CrossoverModName => "ThoriumMod";

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Carrot Farmer");
            /* Tooltip.SetDefault(@"Spins a Carrot Scythe around you that shreds through enemies
Scythes fire off carrots while spun
Grants 1 soul essence on direct hit"); */			
		}

		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.UseSound = SoundID.Item1;
            Item.damage = 80;
            Item.knockBack = 9;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CarrotFarmer_Holdout>();
            Item.shootSpeed = 0.1f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int index = -1;
            for (int m = 0; m < list.Count; m++)
            {
                if (list[m].Name.Equals("ItemName"))
                {
                    index = m;
                    break;
                }
            }

            if (index == -1)
                return;

            //Thorium doesn't localize this line... For some reason. So I guess we won't either?
            TooltipLine colorLine = new TooltipLine(Mod, "Healer", "-Healer Class-")
            {
                OverrideColor = new Color(255, 255, 91)
            };
            list.Insert(index + 1, colorLine);
        }
    }
}