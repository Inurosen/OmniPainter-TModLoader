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

            PaintingModeBtn modeTilesBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1071"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.Tiles"), CircularButtonVariant.Blue, BrushMode.Tiles);
            modeTilesBtn.Width.Set(30f, 0f);
            modeTilesBtn.Height.Set(30f, 0f);
            modeTilesBtn.HAlign = 0.45f;
            modeTilesBtn.VAlign = 0.42f;
            menuPanel.Append(modeTilesBtn);

            PaintingModeBtn modeTilesClearBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1071"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.TilesClear"), CircularButtonVariant.Red, BrushMode.TilesClear);
            modeTilesClearBtn.Width.Set(30f, 0f);
            modeTilesClearBtn.Height.Set(30f, 0f);
            modeTilesClearBtn.HAlign = 0.425f;
            modeTilesClearBtn.VAlign = 0.475f;
            menuPanel.Append(modeTilesClearBtn);

            PaintingModeBtn modeWallsBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1072"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.Walls"), CircularButtonVariant.Blue, BrushMode.Walls);
            modeWallsBtn.Width.Set(30f, 0f);
            modeWallsBtn.Height.Set(30f, 0f);
            modeWallsBtn.HAlign = 0.55f;
            modeWallsBtn.VAlign = 0.42f;
            menuPanel.Append(modeWallsBtn);

            PaintingModeBtn modeWallsClearBtn = new(ModContent.Request<Texture2D>("Terraria/Images/Item_1072"), Language.GetTextValue("Mods.OmniPainter.Strings.Modes.WallsClear"), CircularButtonVariant.Red, BrushMode.WallsClear);
            modeWallsClearBtn.Width.Set(30f, 0f);
            modeWallsClearBtn.Height.Set(30f, 0f);
            modeWallsClearBtn.HAlign = 0.575f;
            modeWallsClearBtn.VAlign = 0.475f;
            menuPanel.Append(modeWallsClearBtn);

            // Paints
            float hAlign = 0.085f;
            float vAlignTop = 0.55f;
            float vAlignBottom = 0.605f;
            bool isTopRow = true;

            foreach (PaintBucket paint in GetBucketsList())
            {
                string displayName = ContentSamples.ItemsByType[((int)paint)].Name;
                PaintButton paintBtn = new(ModContent.Request<Texture2D>($"Terraria/Images/Item_{(int)paint}"), displayName, CircularButtonVariant.Blue, paint);
                paintBtn.Width.Set(30f, 0f);
                paintBtn.Height.Set(30f, 0f);
                paintBtn.HAlign = hAlign;
                paintBtn.VAlign = isTopRow ? vAlignTop : vAlignBottom;
                menuPanel.Append(paintBtn);
                if(!isTopRow)
                {
                    hAlign += 0.055f;
                }
                isTopRow = !isTopRow;
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
