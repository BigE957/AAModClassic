using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._CrossMod.Thorium.Weapons.Healer
{
    public class AuroraScythe : CrossoverItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.CrossMod.Healer";
        public override string CrossoverModName => "ThoriumMod";
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Aurora Scythe");
            /* Tooltip.SetDefault(@"Spins a frostburning scythe around you that shreds through enemies
Scythes inflict frostburn on contact
Grants 1 soul essence on direct hit"); */			
		}

		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 5, 50, 50);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.UseSound = SoundID.Item1;
            Item.damage = 24;
            Item.knockBack = 6;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AuroraScythe_Holdout>();
            Item.shootSpeed = 0.1f;
            Item.DamageType = ThoriumMod.HealerClass;

        }

        public override void UpdateInventory(Player player)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out _))
            {
                Item.TurnToAir();
            }
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceSickle);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceSickle);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}