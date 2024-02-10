using Terraria.UI;

namespace OmniPainter.Common.UI.TileSelectionRectangleOverlay
{
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
