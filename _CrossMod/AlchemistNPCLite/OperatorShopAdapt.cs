using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.BossStandard;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.BossStandard;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.BossStandard;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.BossStandard;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.BossStandard;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.BossStandard;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.BossStandard;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard;
using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.BossStandard;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.BossStandard;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.BossStandard;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.BossStandard;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.BossStandard;
using Microsoft.Build.Evaluation;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.AlchemistNPCLite;

public class OperatorShopAdapt : ModSystem
{
    private ModNPC _operator;

    public override void Load()
    {
        // no need to mark as Unofficial because anpc did this feature in 1.3
        // but wait for anpc update is a long journey(maybe 12 months?)
        if (!ModLoader.TryGetMod("AlchemistNPCLite", out Mod anpclite))
            return;

        if (!anpclite.TryFind<ModNPC>("Operator", out _operator))
            return;

        MethodInfo addshops = _operator.GetType().GetMethod("AddShops",BindingFlags.Public |BindingFlags.Instance,null,Type.EmptyTypes,null);
        if (addshops == null)
            return;

        // have to use il without JIT
        MonoModHooks.Modify(addshops, il =>
        {
            ILCursor cursor = new(il);

            if(cursor.TryGotoNext(x => x.MatchLdstr("ModBags3")))
            {
                cursor.Index-=2;
                cursor.EmitDelegate<Action>(AddItemToShop);
            }
        });
    }

    private void AddItemToShop()
    {
        int operator_type = _operator.Type;

        #region material part
        string ModMaterials_Name = NPCShopDatabase.GetShopName(operator_type, "ModMaterials");

        if (!NPCShopDatabase.TryGetNPCShop(ModMaterials_Name, out AbstractNPCShop ModMaterials))
            return;

        if (ModMaterials == null)
            return;

        NPCShop shop_mate = ModMaterials as NPCShop;
        // if add new item to shop use this method plz
        // shop_mate.Add(new Item(ModContent.ItemType<>()) { shopCustomPrice =  },AAConditions.);

        #endregion

        #region treasure bag part
        // ModBags2 is for [Fargo] & [Thorium] & [Ancients Awaken]
        string ModBags2_Name = NPCShopDatabase.GetShopName(operator_type, "ModBags2");

        if (!NPCShopDatabase.TryGetNPCShop(ModBags2_Name, out AbstractNPCShop ModBags2))
            return;

        if (ModBags2 == null)
            return;

        NPCShop shop_bag = ModBags2 as NPCShop;

        shop_bag.Add(new Item(ModContent.ItemType<MushroomMonarchTreasureBag>()) { shopCustomPrice = 150000 },AAConditions.downedMushroomMonarch);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<FeudalFungusTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedFeudalFungus);

        shop_bag.Add(new Item(ModContent.ItemType<GripsOfChaosTreasureBag>()) { shopCustomPrice = 300000 },AAConditions.downedGripsOfChaos);

        shop_bag.Add(new Item(ModContent.ItemType<TruffleToadTreasureBag>()) { shopCustomPrice = 350000 },AAConditions.downedTruffleToad);

        shop_bag.Add(new Item(ModContent.ItemType<BroodmotherTreasureBag>()) { shopCustomPrice = 500000 },AAConditions.downedBroodmother);

        shop_bag.Add(new Item(ModContent.ItemType<HydraTreasureBag>()) { shopCustomPrice = 750000 },AAConditions.downedHydra);

        shop_bag.Add(new Item(ModContent.ItemType<SubzeroSerpentTreasureBag>()) { shopCustomPrice = 1000000 },AAConditions.downedSubzeroSerpent);

        shop_bag.Add(new Item(ModContent.ItemType<DesertDjinnTreasureBag>()) { shopCustomPrice = 1000000 },AAConditions.downedDesertDjinn);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<SagittariusTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedSagittarius);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<TechnoTruffleTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedTechnoTruffle);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<RetrieverTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedRetriever);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<OrthrusXTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedOrthrusX);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<RaiderUltimaTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedRaiderUltima);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<AnubisTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedAnubis);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<AthenaTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedAthena);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<GreedTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedGreed);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<RajahRabbitTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedRajahRabbit);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<AnubisATreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedForsakenAnubis);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<AthenaATreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedAthenaA);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<GreedATreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedGreedA);

        shop_bag.Add(new Item(ModContent.ItemType<EquinoxWormsTreasureBag>()) { shopCustomPrice = 2500000 },AAConditions.downedEquinoxWorms);

        shop_bag.Add(new Item(ModContent.ItemType<SistersOfDiscordTreasureBag>()) { shopCustomPrice = 5000000 },AAConditions.downedSistersOfDiscord);

        shop_bag.Add(new Item(ModContent.ItemType<AkumaTreasureBag>()) { shopCustomPrice = 5000000 },AAConditions.downedAkuma);

        shop_bag.Add(new Item(ModContent.ItemType<YamataTreasureBag>()) { shopCustomPrice = 5000000 },AAConditions.downedYamata);

        shop_bag.Add(new Item(ModContent.ItemType<ZeroTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedZero);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<RajahRabbitATreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedRajahRabbitR);

        shop_bag.Add(new Item(ModContent.ItemType<ShenDoragonTreasureBag>()) { shopCustomPrice = 15000000 },AAConditions.downedShen);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<InfinityZeroTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedInfinityZero);

        // TODO: no reference price, placeholder 10 platinum (10000000)
        shop_bag.Add(new Item(ModContent.ItemType<SoulOfCthulhuTreasureBag>()) { shopCustomPrice = 10000000 },AAConditions.downedSoulOfCthulhu);
        #endregion
    }
}
