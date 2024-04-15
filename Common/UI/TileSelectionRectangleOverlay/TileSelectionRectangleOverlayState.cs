using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.TileSelectionRectangleOverlay
{
    [Autoload(Side = ModSide.Client)]
    public class TileSelectionRectangleOverlayState : UIState
    {
        public TileSelectionRectangleOverlay overlay;

        public override void OnInitialize()
        {
            overlay = new TileSelectionRectangleOverlay();

            Append(overlay);
        }
    }
}
