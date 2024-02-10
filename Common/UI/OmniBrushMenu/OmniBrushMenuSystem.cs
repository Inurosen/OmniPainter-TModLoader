using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu
{
    public class OmniBrushMenuSystem : ModSystem
    {
        public UserInterface OmniBrushMenuUI;
        public OmniBrushMenuState OmniBrushMenuState;
        public override void Load()
        {
            OmniBrushMenuUI = new UserInterface();
            OmniBrushMenuState = new OmniBrushMenuState();
            OmniBrushMenuState.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            OmniBrushMenuUI?.Update(gameTime);
        }

        public void Show()
        {
            if (OmniBrushMenuUI.CurrentState == null)
            {
                OmniBrushMenuUI.SetState(OmniBrushMenuState);
            }
        }

        public void Hide()
        {
            if (OmniBrushMenuUI.CurrentState != null)
            {
                OmniBrushMenuUI.SetState(null);
            }
        }

        public void Toggle()
        {
            if (OmniBrushMenuUI.CurrentState == null)
            {
                Show();
            } else
            {
                Hide();
            }
        }

        public bool IsShowing()
        {
            return OmniBrushMenuUI.CurrentState != null;
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
                        if (OmniBrushMenuUI?.CurrentState != null)
                        {
                            OmniBrushMenuUI.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public static OmniBrushMenuSystem GetInstance()
        {
            return ModContent.GetInstance<OmniBrushMenuSystem>();
        }
    }
}
