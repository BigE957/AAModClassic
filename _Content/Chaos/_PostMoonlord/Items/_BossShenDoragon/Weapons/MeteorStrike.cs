using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons   //where is located
{
    public class MeteorStrike : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Meteor Strike");
            /* Tooltip.SetDefault(@"Fires a barrage of meteors at your foes
Hitting enemies causes a smaller, but more damaging explosion
Hitting a tile causes a larger, but less damaging projectile
Inflicts Discordian Inferno"); */

        }


        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<MeteorStrike_Meteor>();
            Item.damage = 400;            
            Item.DamageType = DamageClass.Magic;            //if it's magic
            Item.width = 32;              
            Item.height = 36;             
            Item.useTime = 16;          
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;      
            Item.knockBack = .5f;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.mana = 10;
            Item.UseSound = SoundID.Item124;
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.shootSpeed = 16f;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = Main.rand.Next(3);

            switch (type)
            {
                case 0:
                    type = ModContent.ProjectileType<MeteorStrike_Meteor>();
                    break;
                case 1:
                    type = ModContent.ProjectileType<MeteorStrike_MeteorRed>();
                    break;
                default:
                    type = ModContent.ProjectileType<MeteorStrike_MeteorBlue>();
                    break;
            }


            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
            return false;
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 600);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Sunstorm>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AbyssalBomb>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
