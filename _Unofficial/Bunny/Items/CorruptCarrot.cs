using AAModClassic._Content._Misc.__Hardmode.Items.Consumables;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Bunny.Items
{
    public class CorruptCarrot : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Carrot>();
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.buffType = ModContent.BuffType<CorruptGastroenteritis_Buff>();
            Item.buffTime = 52000;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ModContent.ProjectileType<CorruptCarrot_Proj>();
                Item.shootSpeed = 10f;
                Item.UseSound = SoundID.Item1;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.EatFood;
                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 0;
                Item.UseSound = SoundID.Item2;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                player.itemRotation = 0f;
                player.AddBuff(ModContent.BuffType<CorruptGastroenteritis_Buff>(), 52000);
            }
            return null;
        }
    }

    public class CorruptCarrot_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            AIType = ProjectileID.ThrowingKnife;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item2, target.Center);
            target.AddBuff(ModContent.BuffType<CrimsonGastroenteritis_Buff>(), 3600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            SoundEngine.PlaySound(SoundID.Item2, target.Center);
            target.AddBuff(ModContent.BuffType<CrimsonGastroenteritis_Buff>(), 3600);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 10;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center + oldVelocity);

            Point point = Projectile.Center.ToTileCoordinates();
            Tile hit = Framing.GetTileSafely(point);

            if (hit.HasTile && hit.TileType == TileID.CorruptGrass)
            {
                Point pAbove = point - new Point(0, 1);
                Tile above = Framing.GetTileSafely(pAbove);
                if (!above.IsTileSolid() && WorldGen.PlaceObject(pAbove.X, pAbove.Y, ModContent.TileType<CorruptCarrot_Tile>(), false, 0))
                    NetMessage.SendObjectPlacement(-1, pAbove.X, pAbove.Y, ModContent.TileType<CorruptCarrot_Tile>(), 0, 0, -1, -1);
            }
            else if (!hit.IsTileSolid())
            {
                Point pBelow = point + new Point(0, 1);
                Tile below = Framing.GetTileSafely(pBelow);
                if (below.HasTile && below.TileType == TileID.CorruptGrass && WorldGen.PlaceObject(point.X, point.Y, ModContent.TileType<CorruptCarrot_Tile>(), false, 0))
                    NetMessage.SendObjectPlacement(-1, point.X, point.Y, ModContent.TileType<CorruptCarrot_Tile>(), 0, 0, -1, -1);
            }
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Corruption, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
        }
    }

    public class CorruptGastroenteritis_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = Math.Min(player.lifeRegen, 0) - 4;
            player.lifeRegenTime = 0;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 4;
        }
    }

    public class CorruptCarrot_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<CorruptCarrot>());
            DustType = DustID.Corruption;
            HitSound = SoundID.Grass;
        }

        public override bool IsTileDangerous(int i, int j, Player player)
        {
            return true;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }

        public override void Convert(int i, int j, int conversionType)
        {
            if (conversionType == BiomeConversionID.Purity || conversionType == BiomeConversionID.Purity)
                WorldGen.ConvertTile(i, j, ModContent.TileType<Carrot_Tile>());
        }
    }
}
