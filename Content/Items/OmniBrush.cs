using OmniPainter.Common.Players;
using OmniPainter.Common.Systems;
using OmniPainter.Common.UI.OmniBrushMenu;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmniPainter.Content.Items
{
    public class OmniBrush : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.maxStack = 1;
            Item.value = Item.sellPrice(gold: 3);
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Paintbrush)
                .AddIngredient(ItemID.PaintRoller)
                .AddIngredient(ItemID.PaintScraper)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && !OmniBrushMenuSystem.GetInstance().IsShowing())
            {
                WorldPaintingSystem.GetInstance().UseBrush();
            }

            return true;
        }

    }
}
