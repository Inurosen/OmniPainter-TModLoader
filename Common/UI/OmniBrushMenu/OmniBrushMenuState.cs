using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using OmniPainter.Common.UI.OmniBrushMenu.Buttons;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu
{
    public class OmniBrushMenuState : UIState
    {
        public OmniBrushMenuPanel menuPanel;

        public override void OnInitialize()
        {
            menuPanel = new OmniBrushMenuPanel();
            menuPanel.BackgroundColor *= 0f;
            menuPanel.BorderColor *= 0f;
            menuPanel.Width.Set(630f, 0f);
            menuPanel.Height.Set(630f, 0f);
            Append(menuPanel);

            // Tools

            PaintingToolBtn toolTilesBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1071"), Language.GetTextValue("Mods.OmniPainter.Strings.Tools.Tiles"), CircularButtonVariant.Blue, BrushTool.Tiles);
            toolTilesBtn.Width.Set(30f, 0f);
            toolTilesBtn.Height.Set(30f, 0f);
            toolTilesBtn.HAlign = 0.45f;
            toolTilesBtn.VAlign = 0.42f;
            toolTilesBtn.SetSelected(true);
            menuPanel.Append(toolTilesBtn);

            PaintingToolBtn toolTilesClearBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1071"), Language.GetTextValue("Mods.OmniPainter.Strings.Tools.TilesClear"), CircularButtonVariant.Red, BrushTool.TilesClear);
            toolTilesClearBtn.Width.Set(30f, 0f);
            toolTilesClearBtn.Height.Set(30f, 0f);
            toolTilesClearBtn.HAlign = 0.425f;
            toolTilesClearBtn.VAlign = 0.475f;
            menuPanel.Append(toolTilesClearBtn);

            PaintingToolBtn toolWallsBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1072"), Language.GetTextValue("Mods.OmniPainter.Strings.Tools.Walls"), CircularButtonVariant.Blue, BrushTool.Walls);
            toolWallsBtn.Width.Set(30f, 0f);
            toolWallsBtn.Height.Set(30f, 0f);
            toolWallsBtn.HAlign = 0.55f;
            toolWallsBtn.VAlign = 0.42f;
            menuPanel.Append(toolWallsBtn);

            PaintingToolBtn toolWallsClearBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1072"), Language.GetTextValue("Mods.OmniPainter.Strings.Tools.WallsClear"), CircularButtonVariant.Red, BrushTool.WallsClear);
            toolWallsClearBtn.Width.Set(30f, 0f);
            toolWallsClearBtn.Height.Set(30f, 0f);
            toolWallsClearBtn.HAlign = 0.575f;
            toolWallsClearBtn.VAlign = 0.475f;
            menuPanel.Append(toolWallsClearBtn);

            // Paints

            float hAlign = 0.085f;
            float vAlignTop = 0.55f;
            float vAlignBottom = 0.605f;
            bool isTopRow = true;
            bool isSelectionsSet = false;

            foreach (PaintBucket paint in GetBucketsList())
            {
                string displayName = ContentSamples.ItemsByType[((int)paint)].Name;
                PaintButton paintBtn = new(ModContent.Request<Texture2D>($"Terraria/Images/Item_{(int)paint}"), displayName, CircularButtonVariant.Blue, paint);
                paintBtn.Width.Set(30f, 0f);
                paintBtn.Height.Set(30f, 0f);
                paintBtn.HAlign = hAlign;
                paintBtn.VAlign = isTopRow ? vAlignTop : vAlignBottom;
                if(!isSelectionsSet)
                {
                    paintBtn.SetSelected(true);
                    isSelectionsSet = true;
                }
                menuPanel.Append(paintBtn);
                if (!isTopRow)
                {
                    hAlign += 0.055f;
                }
                isTopRow = !isTopRow;
            }

            // Modes

            ModeButton modeAllBtn = new(ModContent.Request<Texture2D>("OmniPainter/Common/UI/OmniBrushMenu/Buttons/Mode_All"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.All"), CircularButtonVariant.Blue, Mode.All);
            modeAllBtn.Width.Set(30f, 0f);
            modeAllBtn.Height.Set(30f, 0f);
            modeAllBtn.HAlign = 0.675f;
            modeAllBtn.VAlign = 0.475f;
            modeAllBtn.SetSelected(true);
            menuPanel.Append(modeAllBtn);

            ModeButton modeOnlyUnpaintedBtn = new(ModContent.Request<Texture2D>("OmniPainter/Common/UI/OmniBrushMenu/Buttons/Mode_Unpainted"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.UnpaintedTiles"), CircularButtonVariant.Blue, Mode.OnlyUnpainted);
            modeOnlyUnpaintedBtn.Width.Set(30f, 0f);
            modeOnlyUnpaintedBtn.Height.Set(30f, 0f);
            modeOnlyUnpaintedBtn.HAlign = 0.730f;
            modeOnlyUnpaintedBtn.VAlign = 0.475f;
            menuPanel.Append(modeOnlyUnpaintedBtn);

            // Undo

            UndoButton undoBtn = new(ModContent.Request<Texture2D>("Terraria/Images/UI/VK_Backspace"), Language.GetTextValue("Mods.OmniPainter.Strings.Undo"), CircularButtonVariant.Blue);
            undoBtn.Width.Set(30f, 0f);
            undoBtn.Height.Set(30f, 0f);
            undoBtn.HAlign = 0.325f;
            undoBtn.VAlign = 0.475f;
            menuPanel.Append(undoBtn);

        }

        public void DeselectAllToolButtons<T>()
        {
            foreach (CircularButton btn in menuPanel.Children)
            {
                if (btn is T)
                {
                    btn.SetSelected(false);
                }
            }
        }

        private static List<PaintBucket> GetBucketsList()
        {
            return new List<PaintBucket> {
                PaintBucket.Brown,
                PaintBucket.Gray,
                PaintBucket.White,
                PaintBucket.Black,
                PaintBucket.Red,
                PaintBucket.DeepRed,
                PaintBucket.Orange,
                PaintBucket.DeepOrange,
                PaintBucket.Yellow,
                PaintBucket.DeepYellow,
                PaintBucket.Lime,
                PaintBucket.DeepLime,
                PaintBucket.Green,
                PaintBucket.DeepGreen,
                PaintBucket.Teal,
                PaintBucket.DeepTeal,
                PaintBucket.Cyan,
                PaintBucket.DeepCyan,
                PaintBucket.SkyBlue,
                PaintBucket.DeepSkyBlue,
                PaintBucket.Blue,
                PaintBucket.DeepBlue,
                PaintBucket.Purple,
                PaintBucket.DeepPurple,
                PaintBucket.Violet,
                PaintBucket.DeepViolet,
                PaintBucket.Pink,
                PaintBucket.DeepPink,
                PaintBucket.Shadow,
                PaintBucket.Negative,
                PaintBucket.Echo,
                PaintBucket.Illuminant
            };
        }
    }
}
