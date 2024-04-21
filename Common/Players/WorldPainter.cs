using Terraria.ModLoader;
using Terraria;
using OmniPainter.Content.Items;
using Terraria.GameInput;
using OmniPainter.Common.Systems;
using Terraria.DataStructures;
using OmniPainter.Common.UI.OmniBrushMenu;

namespace OmniPainter.Common.Players
{
    [Autoload(Side = ModSide.Client)]
    public class WorldPainter : ModPlayer
    {
        private bool isMouseRightHandled = false;

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            WorldPaintingSystem.GetInstance().ResetPainting();
        }

        public override void PostUpdate()
        {
            if (Player.whoAmI == Main.myPlayer && Player.inventory[Player.selectedItem].ModItem is not OmniBrush)
            {
                WorldPaintingSystem.GetInstance().ResetPainting();
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            HandleRightClickMenu();
            HandleEscape();
        }

        private void HandleRightClickMenu()
        {
            if (Main.mouseRight && Player.inventory[Player.selectedItem].ModItem is OmniBrush && !isMouseRightHandled)
            {
                isMouseRightHandled = true;
                OmniBrushMenuSystem.GetInstance().Toggle();
                WorldPaintingSystem.GetInstance().ResetPainting();
            }

            if (!Main.mouseRight && isMouseRightHandled)
            {
                isMouseRightHandled = false;
            }
        }

        private static void HandleEscape()
        {
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                WorldPaintingSystem.GetInstance().ResetPainting();
                OmniBrushMenuSystem.GetInstance().Hide();
            }
        }
    }
}
