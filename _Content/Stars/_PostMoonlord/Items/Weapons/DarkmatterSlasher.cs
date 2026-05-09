using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using AAModClassic.CrossMod.Overhaul;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class DarkmatterSlasher : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Slasher");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 350;
            Item.knockBack = 3;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 30 / 2; //dont change the 30 unless you want to soft lock your computer. instead use it as a value and use 15 as the number 2, doing this will divide the 30 with 15 to get a use time of 2
            Item.useAnimation = 13;
            Item.shoot = ModContent.ProjectileType<DarkmatterSlasher_DarkmatterWave>();
            Item.shootSpeed = 25f;
            Item.value = 25000;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 35);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 25);
		    recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

        public override void HoldItem(Player player)
        {
            Saber.HoldItemManager(player, Item, ModContent.ProjectileType<DarkmatterSlasher_Slash>(),
                Color.Blue, 0.9f, player.itemTime == 0 ? 0f : 1f);
        }

        // Doesn't get called unless item.shoot is defined.
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        { return Saber.IsChargedShot(player); }

        public override void UseItemFrame(Player player)
        {
            Saber.UseItemFrame(player, 0.9f, Item.beingGrabbed);
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            int height = 80;
            int length = 132;
            Saber.UseItemHitboxCalculate(player, Item, ref hitbox, ref noHitbox, 0.9f, height, length);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Color colour = new Color(0.1f, 255f, 181f);
            Saber.OnHitFX(player, target, hit.Crit, colour, true);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Saber.SabreIsChargedStriking(player, Item))
            { modifiers.FinalDamage.Flat = -500; }
        }
    }
}
