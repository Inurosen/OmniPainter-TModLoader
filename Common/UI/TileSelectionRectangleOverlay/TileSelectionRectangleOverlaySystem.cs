using Microsoft.Xna.Framework;
using OmniPainter.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.TileSelectionRectangleOverlay
{
    [Autoload(Side = ModSide.Client)]
    public class TileSelectionRectangleOverlaySystem : ModSystem
    {
        public UserInterface OverlayUI;
        public TileSelectionRectangleOverlayState OverlayState;

        public override void Load()
        {
            OverlayUI = new UserInterface();
            OverlayState = new TileSelectionRectangleOverlayState();
            OverlayState.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (WorldPaintingSystem.GetInstance().IsSettingPoints())
            {
                OverlayUI.SetState(OverlayState);
            }
            else
            {
                OverlayUI.SetState(null);
            }
            OverlayUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TIleSelectionRectangleOverlay",
                    delegate
                    {
                        if (OverlayUI?.CurrentState != null)
                        {
                            OverlayUI.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }

    }
}
