﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OmniPainter.Common.Systems
{
    [Autoload(Side = ModSide.Client)]
    public class WorldPaintingSystem : ModSystem
    {
        private bool isSettingPoints;
        private Point startTile;
        private Point fromTile;


        private BrushMode CurrentMode = BrushMode.Tiles;
        private PaintBucket CurrentPaintBucket = PaintBucket.Brown;

        public void UseBrush()
        {
            Point mousePos = Main.MouseWorld.ToTileCoordinates();
            SetPoint(mousePos);
            if (!isSettingPoints)
            {
                Paint(startTile, fromTile);
            }
        }

        public void ResetPainting()
        {
            if (isSettingPoints)
            {
                isSettingPoints = false;
                //Main.NewText($"Resetting");
            }
        }

        private void SetPoint(Point point)
        {
            if (!isSettingPoints)
            {
                isSettingPoints = true;
                startTile = point;
                //Main.NewText($"Starting at {point.X}, {point.Y}");
            }
            else
            {
                isSettingPoints = false;
                fromTile = point;
                //Main.NewText($"Stopping at {point.X}, {point.Y}");
            }
        }
        private void Paint(Point from, Point to)
        {
            int xFactor = 1;
            int yFactor = 1;
            if (from.X > to.X)
            {
                xFactor = -1;
            }

            if (from.Y > to.Y)
            {
                yFactor = -1;
            }

            for (int x = from.X; x != to.X + xFactor; x += xFactor)
            {
                for (int y = from.Y; y != to.Y + yFactor; y += yFactor)
                {
                    Item paint = ContentSamples.ItemsByType[((int)CurrentPaintBucket)];
                    switch (CurrentMode)
                    {
                        case BrushMode.Tiles:
                            if(isCoating(CurrentPaintBucket))
                            {
                                WorldGen.paintCoatTile(x, y, paint.paintCoating, true);
                            }
                            else
                            {
                                WorldGen.paintTile(x, y, paint.paint, true);
                            }
                            break;
                        case BrushMode.TilesClear:
                            WorldGen.paintTile(x, y, 0, broadCast: true);
                            WorldGen.paintCoatTile(x, y, 0, true);
                            break;
                        case BrushMode.Walls:
                            if (isCoating(CurrentPaintBucket))
                            {
                                WorldGen.paintCoatWall(x, y, paint.paintCoating, true);
                            }
                            else
                            {
                                WorldGen.paintWall(x, y, paint.paint, true);
                            }
                            break;
                        case BrushMode.WallsClear:
                            WorldGen.paintWall(x, y, 0, broadCast: true);
                            WorldGen.paintCoatWall(x, y, 0, true);
                            break;
                    }
                }
            }
        }

        public void SelectMode(BrushMode mode)
        {
            CurrentMode = mode;
            //Main.NewText($"Setting mode to: {Language.GetTextValue($"Mods.OmniPainter.Strings.Modes.{mode}")}");

        }

        public void SelectPaintBucket(PaintBucket paintBucket)
        {
            CurrentPaintBucket = paintBucket;
        }


        public void SelectNextMode()
        {
            SelectMode(CurrentMode.NextEnum());
        }

        public bool IsSettingPoints()
        {
            return isSettingPoints;
        }

        public BrushMode GetMode()
        {
            return CurrentMode;
        }

        public Vector2 GetScreenPositionStart()
        {
            Point mousePos = Main.MouseWorld.ToTileCoordinates();
            int xFactor = -8;
            int yFactor = -8;
            if (startTile.X > mousePos.X)
            {
                xFactor = 8;
            }

            if (startTile.Y > mousePos.Y)
            {
                yFactor = 8;
            }

            return new Vector2(startTile.ToWorldCoordinates().X - Main.screenPosition.X + xFactor, startTile.ToWorldCoordinates().Y - Main.screenPosition.Y + yFactor);
        }

        public Vector2 GetScreenPositionEnd()
        {
            Point mousePos = Main.MouseWorld.ToTileCoordinates();
            int xFactor = 8;
            int yFactor = 8;
            if (startTile.X > mousePos.X)
            {
                xFactor = -8;
            }

            if (startTile.Y > mousePos.Y)
            {
                yFactor = -8;
            }


            return new Vector2(mousePos.ToWorldCoordinates().X - Main.screenPosition.X + xFactor, mousePos.ToWorldCoordinates().Y - Main.screenPosition.Y + yFactor);
        }


        public static WorldPaintingSystem GetInstance()
        {
            return ModContent.GetInstance<WorldPaintingSystem>();
        }
        
        private bool isCoating(PaintBucket bucket)
        {
            return bucket == PaintBucket.Echo || bucket == PaintBucket.Illuminant;
        }

    }

    public enum BrushMode : int
    {
        Tiles = 0,
        Walls = 1,
        TilesClear = 2,
        WallsClear = 3
    }

    public enum PaintBucket : int
    {
        Brown = ItemID.BrownPaint,
        Gray = ItemID.GrayPaint,
        White = ItemID.WhitePaint,
        Black = ItemID.BlackPaint,
        Red = ItemID.RedPaint,
        DeepRed = ItemID.DeepRedPaint,
        Orange = ItemID.OrangePaint,
        DeepOrange = ItemID.DeepOrangePaint,
        Yellow = ItemID.YellowPaint,
        DeepYellow = ItemID.DeepYellowPaint,
        Lime = ItemID.LimePaint,
        DeepLime = ItemID.DeepLimePaint,
        Green = ItemID.GreenPaint,
        DeepGreen = ItemID.DeepGreenPaint,
        Teal = ItemID.TealPaint,
        DeepTeal = ItemID.DeepTealPaint,
        Cyan = ItemID.CyanPaint,
        DeepCyan = ItemID.DeepCyanPaint,
        SkyBlue = ItemID.SkyBluePaint,
        DeepSkyBlue = ItemID.DeepSkyBluePaint,
        Blue = ItemID.BluePaint,
        DeepBlue = ItemID.DeepBluePaint,
        Purple = ItemID.PurplePaint,
        DeepPurple = ItemID.DeepPurplePaint,
        Violet = ItemID.VioletPaint,
        DeepViolet = ItemID.DeepVioletPaint,
        Pink = ItemID.PinkPaint,
        DeepPink = ItemID.DeepPinkPaint,
        Shadow = ItemID.ShadowPaint,
        Negative = ItemID.NegativePaint,

        Echo = ItemID.EchoCoating,
        Illuminant = 4668 // No ItemID for some reason
    }
}
