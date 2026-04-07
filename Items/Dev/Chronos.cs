using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Dev
{
    public class Chronos : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.useTime = 25;
            Item.CloneDefaults(ItemID.Terrarian);

            Item.damage = 350;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shoot = ModContent.ProjectileType<Projectiles.Chronos>();
            Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.Flat *= CalcDamageMultiplierFromTimeOfDay(Item.damage);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chronos");
            /* Tooltip.SetDefault("Time Teller EX\n" +
                "Damage changes based on time of day\n" +
                "Damage is greatest at Midday and Midnight\n" +
                "'Time is big ball of wibbly-wobbly timey-wimey yo-yos.'\n" +
                "-Dallin"); */
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TimeTeller>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.Register();
        }

        public static float CalcDamageMultiplierFromTimeOfDay(int baseDamage)
        {
            int minDamage = baseDamage; //this is the damage you set in SetDefaults.
            int maxDamage = 500; //this is the damage you get at midday/midnight.

            float maxMultiplier = maxDamage / (float)minDamage;
            float time = (int)Main.time;
            float calcTimeMax = 0f;
            if (Main.dayTime)
                calcTimeMax = 54000f; //max time in a day
            else
                calcTimeMax = 32400f; //max time in a night

            return BaseUtility.MultiLerp(time / calcTimeMax, 1f, maxMultiplier, 1f);
        }
    }
}
