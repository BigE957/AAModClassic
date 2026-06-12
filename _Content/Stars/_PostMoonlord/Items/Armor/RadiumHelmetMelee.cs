using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class RadiumHelmetMelee : BaseAAItem, ICustomEquipGlow
    {
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<AAPlayer>().Radium;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Helmet");
			/* Tooltip.SetDefault(@"15% increased melee damage
Shines with the light of a starry night sky"); */

        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 30;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.15f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<RadiumChestplate>() && legs.type == ModContent.ItemType<RadiumLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{
            const float effectRange = 500;
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumHelmetBonus");
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int p = 0; p < Main.player.Length; p++)
                {
                    if (Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team != Main.player[p].team)
                    {
                        Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().ShieldTime = 2;
                        Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().badShield = true;
                    }
                }
            }
            for(int n = 0; n < Main.npc.Length; n++)
            {
                if ((Main.npc[n].Center - player.Center).Length() < effectRange && Main.npc[n].CanBeChasedBy(ignoreDontTakeDamage: false))
                {
                    Main.npc[n].GetGlobalNPC<StarHelmetMeleePlayer_RadiumWeaken>().BrokenShield = 2;
                }
            }
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    
}