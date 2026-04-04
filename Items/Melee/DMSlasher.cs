using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using AAModClassic.CrossMod.Overhaul;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Melee
{
    public class DMSlasher : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Slasher");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.damage = 350;
            Item.knockBack = 3;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 30 / 2; //dont change the 30 unless you want to soft lock your computer. instead use it as a value and use 15 as the number 2, doing this will divide the 30 with 15 to get a use time of 2
            Item.useAnimation = 13;
            Item.shoot = ModContent.ProjectileType<MS>();
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
            recipe.AddIngredient(null, "DarkEnergy", 35);
            recipe.AddIngredient(null, "DarkMatter", 25);
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

        public override void HoldItem(Player player)
        {
            Saber.HoldItemManager(player, Item, ModContent.ProjectileType<DMSlash>(),
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
    public class DMSlash : ModProjectile
    {
        public static Asset<Texture2D> specialSlash;
        public static int specialProjFrames = 7;
        readonly int chargeSlashDirection = 1;


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
            if (Main.netMode == NetmodeID.Server) return;
        Player projOwner = Main.player[Projectile.owner];
          Projectile.position.X = projOwner.Center.X - (Projectile.width / 2);
          Projectile.position.Y = projOwner.Center.Y - (Projectile.height / 2);
            specialSlash = ModContent.Request<Texture2D>("AAModClassic/Items/Melee/" + GetType().Name + "2");
        Projectile.direction = projOwner.direction;
        Projectile.spriteDirection = projOwner.direction;
        projOwner.heldProj = Projectile.whoAmI;
        projOwner.itemTime = projOwner.itemAnimation;
        }
        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 66;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override bool? CanCutTiles() { return false; }
        public int FrameCheck
        {
            get { return (int)Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }
        public int SlashLogic
        {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Saber.AINormalSlash(Projectile, SlashLogic)) { }
            else
            {
                // Charged attack
                Saber.AISetChargeSlashVariables(player, chargeSlashDirection);
                Saber.NormalSlash(Projectile, player);
            }
            Projectile.damage = 0;
            Projectile.ai[0] += 1f; // Framerate
            Projectile.position += player.velocity;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            int weaponItemID = ModContent.ItemType<DMSlasher>();
            Color lighting = Lighting.GetColor((int)(player.MountedCenter.X / 16), (int)(player.MountedCenter.Y / 16));
            return Saber.PreDrawSlashAndWeapon(Main.spriteBatch, Projectile, weaponItemID, lighting,
                SlashLogic == 0f ? specialSlash.Value : null,
                SlashLogic == 0f ? new Color(1f, 255f, 181f, 1f) : lighting,
                specialProjFrames,
                SlashLogic == 0f ? chargeSlashDirection : SlashLogic);
        }

    }
}
