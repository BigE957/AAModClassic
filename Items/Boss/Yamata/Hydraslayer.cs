using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata   //where is located
{
    public class Hydraslayer : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amenomuraku");
            /* Tooltip.SetDefault(@"Used to defeat the multi-headed monstrosities of the abyss
Inflicts Moonrazed"); */
        }

        
        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("PhantomSword").Type;
            Item.damage = 220;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 86;              
            Item.height = 86;             
            Item.useTime = 13;          
            Item.useAnimation = 13;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 3f;      
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item20;      
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            float numberProjectiles = 1; // This defines how many projectiles to shot
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                numberProjectiles = 2 + Main.rand.Next(3);
            }

            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    position = new Vector2(player.position.X - ((Main.rand.Next(61) + 40) * player.direction), player.position.Y - (Main.rand.Next(91) - 40)); //this defines the distance of the projectiles form the player when the projectile spawns
                    Vector2 perturbedSpeed = Vector2.Normalize(new Vector2((Main.MouseWorld.X - position.X) + (Main.rand.Next(41) - 20), (Main.MouseWorld.Y - position.Y) + (Main.rand.Next(41) - 20))) * 15f; // This defines the projectile roatation and speed. .4f == projectile speed
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}