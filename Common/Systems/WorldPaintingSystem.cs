﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmniPainter.Common.Systems
{
    [Autoload(Side = ModSide.Client)]
    public class WorldPaintingSystem : ModSystem
    {
        private bool isSettingPoints;
        private Point startTile;
        private Point fromTile;
        private List<UndoTileInfo> undoBuffer = new List<UndoTileInfo>();


        private BrushTool CurrentTool = BrushTool.Tiles;
        private PaintBucket CurrentPaintBucket = PaintBucket.Brown;
        private Mode CurrentMode = Mode.All;

        public override void ClearWorld()
        {
            ResetPainting();
        }

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
            SoundEngine.PlaySound(SoundID.MenuTick);
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
            undoBuffer.Clear();
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
                    Tile tile = Main.tile[x, y];
                    undoBuffer.Add(new UndoTileInfo
                    {
                        X = x,
                        Y = y,
                        TileColor = tile.TileColor,
                        WallColor = tile.WallColor,
                        TileFullBright = tile.IsTileFullbright,
                        TileInvisible = tile.IsTileInvisible,
                        WallFullBright = tile.IsWallFullbright,
                        WallInvisible = tile.IsWallInvisible,
                    });
                    switch (CurrentTool)
                    {
                        case BrushTool.Tiles:
                            if (CurrentMode == Mode.OnlyUnpainted && !isBlockUnpainted(tile))
                            {
                                break;
                            }
                            if (isCoating(CurrentPaintBucket))
                            {
                                WorldGen.paintCoatTile(x, y, paint.paintCoating, true);
                            }
                            else
                            {
                                WorldGen.paintTile(x, y, paint.paint, true);
                            }
                            break;
                        case BrushTool.TilesClear:
                            WorldGen.paintTile(x, y, 0, broadCast: true);
                            WorldGen.paintCoatTile(x, y, 0, true);
                            break;
                        case BrushTool.Walls:
                            if (CurrentMode == Mode.OnlyUnpainted && !isWallUnpainted(tile))
                            {
                                break;
                            }
                            if (isCoating(CurrentPaintBucket))
                            {
                                WorldGen.paintCoatWall(x, y, paint.paintCoating, true);
                            }
                            else
                            {
                                WorldGen.paintWall(x, y, paint.paint, true);
                            }
                            break;
                        case BrushTool.WallsClear:
                            WorldGen.paintWall(x, y, 0, broadCast: true);
                            WorldGen.paintCoatWall(x, y, 0, true);
                            break;
                    }
                }
            }
        }

        public void Undo()
        {
            foreach (UndoTileInfo undoTileInfo in undoBuffer)
            {
                int x = undoTileInfo.X;
                int y = undoTileInfo.Y;

                WorldGen.paintTile(x, y, undoTileInfo.TileColor, true);
                WorldGen.paintWall(x, y, undoTileInfo.WallColor, true);
                if (undoTileInfo.TileFullBright)
                {
                    WorldGen.paintCoatTile(x, y, PaintCoatingID.Glow, true);
                }
                else if (undoTileInfo.TileInvisible)
                {
                    WorldGen.paintCoatTile(x, y, PaintCoatingID.Echo, true);
                }
                else
                {
                    WorldGen.paintCoatTile(x, y, PaintCoatingID.None, true);
                }

                if (undoTileInfo.WallFullBright)
                {
                    WorldGen.paintCoatWall(x, y, PaintCoatingID.Glow, true);
                }
                else if (undoTileInfo.WallInvisible)
                {
                    WorldGen.paintCoatWall(x, y, PaintCoatingID.Echo, true);
                }
                else
                {
                    WorldGen.paintCoatWall(x, y, PaintCoatingID.None, true);
                }

            }

            undoBuffer.Clear();
        }

        public bool CanUndo()
        {
            return undoBuffer.Count > 0;
        }

        public void SelectTool(BrushTool tool)
        {
            CurrentTool = tool;
            //Main.NewText($"Setting tool to: {Language.GetTextValue($"Mods.OmniPainter.Strings.Tools.{tool}")}");

        }

        public void SelectPaintBucket(PaintBucket paintBucket)
        {
            CurrentPaintBucket = paintBucket;
        }

        public void SelectMode(Mode mode)
        {
            CurrentMode = mode;
        }


        public void SelectNextTool()
        {
            SelectTool(CurrentTool.NextEnum());
        }

        public bool IsSettingPoints()
        {
            return isSettingPoints;
        }

        public BrushTool GetCurrentTool()
        {
            return CurrentTool;
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

        private bool isBlockUnpainted(Tile tile)
        {
            TileColorCache colorCache = tile.BlockColorAndCoating();
            return colorCache.Color == 0 && !colorCache.Invisible && !colorCache.FullBright;
        }

        private bool isWallUnpainted(Tile tile)
        {
            TileColorCache colorCache = tile.WallColorAndCoating();
            return colorCache.Color == 0 && !colorCache.Invisible && !colorCache.FullBright;
        }

    }

    public enum BrushTool : int
    {
        Tiles = 0,
        Walls = 1,
        TilesClear = 2,
        WallsClear = 3
    }

    public enum Mode : int
    {
        All = 0,
        OnlyUnpainted = 1
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
        Illuminant = ItemID.GlowPaint
    }

    struct UndoTileInfo
    {
        public int X;
        public int Y;
        public byte TileColor;
        public byte WallColor;
        public bool TileInvisible;
        public bool TileFullBright;
        public bool WallInvisible;
        public bool WallFullBright;

    }
}
