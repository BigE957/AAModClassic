using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.CrossMod.CalamityMod;
using AAModClassic.CrossMod;

namespace AAModClassic.Items.DevTools
{
    public class RogueTest : RogueWeapon
    {
        public override string Texture => "AAModClassic/Items/DevTools/NoodleSword";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[DEV] Rogue Noodle Sword");
            // Tooltip.SetDefault(@"Top 10 op weapons in video games");
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 10000;
            Item.width = 64;            
            Item.height = 70;         
            Item.useTime = 17;   
            Item.useAnimation = 17;     
            Item.useStyle = ItemUseStyleID.Swing;       
            Item.knockBack = 4;   
            Item.value = 0;        
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.expert = true; Item.expertOnly = true;
			Item.shoot = Mod.Find<ModProjectile>("Noodle").Type;
			Item.shootSpeed = 9f;
            Item.GetGlobalItem<RogueItem>().rogue = true; //Set rogue damage
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (ModSupport.GetMod("CalamityMod") != null)
            {
                int num = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, Mod.Find<ModProjectile>("Noodle").Type, damage, knockback, player.whoAmI, 0f, 1f);
                Main.projectile[num].GetGlobalProjectile<RogueProj>().rogue = true;
                if (player.GetModPlayer<RoguePlayer>().StealthStrikeAvailable) //Stealth Strike
                {
                    float scaleFactor = 15f;
                    int num5 = 25;
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, new Vector2(1f, 0f) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, new Vector2(0f, 1f) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, new Vector2(-1f, 0f) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, new Vector2(0f, -1f) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, Vector2.Normalize(new Vector2(1f, 1f)) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, Vector2.Normalize(new Vector2(1f, -1f)) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, Vector2.Normalize(new Vector2(-1f, -1f)) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.position, Vector2.Normalize(new Vector2(-1f, 1f)) * scaleFactor, Mod.Find<ModProjectile>("Noodle").Type, num5, 2f, player.whoAmI, 0f, 0f);
                }
                return false;
            }
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (ModSupport.GetMod("CalamityMod") == null)
            {
                Item.TurnToAir();
            }
        }
    }
}
