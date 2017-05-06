﻿using System;
using System.IO;
using System.IO.Compression;
using Terraria;
using TShockAPI;

namespace WorldEdit.Commands
{
	public class Rotate : WECommand
	{
		private const int BUFFER_SIZE = 1048576;
		private int degrees;

		public Rotate(TSPlayer plr, int degrees)
			: base(0, 0, 0, 0, plr)
		{
			this.degrees = degrees;
		}

		public override void Execute()
		{
			string clipboardPath = Tools.GetClipboardPath(plr.User.ID);

			if (Tools.NewClipboardStruct(clipboardPath))
			{
				Tuple<Tile, string, Item, Item[]>[,] tiles = Tools.LoadWorldDataNew(clipboardPath);
				int width = tiles.GetLength(0);
				int height = tiles.GetLength(1);

				using (var writer =
					new BinaryWriter(
						new BufferedStream(
							new GZipStream(File.Open(clipboardPath, FileMode.Create), CompressionMode.Compress), BUFFER_SIZE)))
				{
					writer.Write(0);
					writer.Write(0);
					// TODO: don't rotate furniture
					switch (((degrees / 90) % 4 + 4) % 4)
					{
						case 0:
							writer.Write(width);
							writer.Write(height);
							for (int i = 0; i < width; i++)
							{
								for (int j = 0; j < height; j++)
									writer.WriteTileNew(tiles[i, j]);
							}
							break;
						case 1:
							writer.Write(height);
							writer.Write(width);
							for (int j = height - 1; j >= 0; j--)
							{
								for (int i = 0; i < width; i++)
								{
									if (tiles[i, j].Item1.slope() == 0)
										writer.WriteTileNew(tiles[i, j]);
									else if (tiles[i, j].Item1.slope() == 1)
									{
										tiles[i, j].Item1.slope(3);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 2)
									{
										tiles[i, j].Item1.slope(1);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 3)
									{
										tiles[i, j].Item1.slope(4);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 4)
									{
										tiles[i, j].Item1.slope(2);
										writer.WriteTileNew(tiles[i, j]);
									}
								}
							}
							break;
						case 2:
							writer.Write(width);
							writer.Write(height);
							for (int i = width - 1; i >= 0; i--)
							{
								for (int j = height - 1; j >= 0; j--)
								{
									if (tiles[i, j].Item1.slope() == 0)
										writer.WriteTileNew(tiles[i, j]);
									else if (tiles[i, j].Item1.slope() == 1)
									{
										tiles[i, j].Item1.slope(4);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 2)
									{
										tiles[i, j].Item1.slope(3);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 3)
									{
										tiles[i, j].Item1.slope(2);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 4)
									{
										tiles[i, j].Item1.slope(1);
										writer.WriteTileNew(tiles[i, j]);
									}
								}
							}
							break;
						case 3:
							writer.Write(height);
							writer.Write(width);
							for (int j = 0; j < height; j++)
							{
								for (int i = width - 1; i >= 0; i--)
								{
									if (tiles[i, j].Item1.slope() == 0)
										writer.WriteTileNew(tiles[i, j]);
									else if (tiles[i, j].Item1.slope() == 1)
									{
										tiles[i, j].Item1.slope(2);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 2)
									{
										tiles[i, j].Item1.slope(4);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 3)
									{
										tiles[i, j].Item1.slope(1);
										writer.WriteTileNew(tiles[i, j]);
									}
									else if (tiles[i, j].Item1.slope() == 4)
									{
										tiles[i, j].Item1.slope(3);
										writer.WriteTileNew(tiles[i, j]);
									}
								}
							}
							break;
					}

				}
			}
			else
			{
				Tile[,] tiles = Tools.LoadWorldDataOld(clipboardPath);
				int width = tiles.GetLength(0);
				int height = tiles.GetLength(1);

				using (var writer =
					new BinaryWriter(
						new BufferedStream(
							new GZipStream(File.Open(clipboardPath, FileMode.Create), CompressionMode.Compress), BUFFER_SIZE)))
				{
					writer.Write(0);
					writer.Write(0);

					switch (((degrees / 90) % 4 + 4) % 4)
					{
						case 0:
							writer.Write(width);
							writer.Write(height);
							for (int i = 0; i < width; i++)
							{
								for (int j = 0; j < height; j++)
									writer.WriteTileOld(tiles[i, j]);
							}
							break;
						case 1:
							writer.Write(height);
							writer.Write(width);
							for (int j = height - 1; j >= 0; j--)
							{
								for (int i = 0; i < width; i++)
								{
									if (tiles[i, j].slope() == 0)
										writer.WriteTileOld(tiles[i, j]);
									else if (tiles[i, j].slope() == 1)
									{
										tiles[i, j].slope(3);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 2)
									{
										tiles[i, j].slope(1);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 3)
									{
										tiles[i, j].slope(4);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 4)
									{
										tiles[i, j].slope(2);
										writer.WriteTileOld(tiles[i, j]);
									}
								}
							}
							break;
						case 2:
							writer.Write(width);
							writer.Write(height);
							for (int i = width - 1; i >= 0; i--)
							{
								for (int j = height - 1; j >= 0; j--)
								{
									if (tiles[i, j].slope() == 0)
										writer.WriteTileOld(tiles[i, j]);
									else if (tiles[i, j].slope() == 1)
									{
										tiles[i, j].slope(4);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 2)
									{
										tiles[i, j].slope(3);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 3)
									{
										tiles[i, j].slope(2);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 4)
									{
										tiles[i, j].slope(1);
										writer.WriteTileOld(tiles[i, j]);
									}
								}
							}
							break;
						case 3:
							writer.Write(height);
							writer.Write(width);
							for (int j = 0; j < height; j++)
							{
								for (int i = width - 1; i >= 0; i--)
								{
									if (tiles[i, j].slope() == 0)
										writer.WriteTileOld(tiles[i, j]);
									else if (tiles[i, j].slope() == 1)
									{
										tiles[i, j].slope(2);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 2)
									{
										tiles[i, j].slope(4);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 3)
									{
										tiles[i, j].slope(1);
										writer.WriteTileOld(tiles[i, j]);
									}
									else if (tiles[i, j].slope() == 4)
									{
										tiles[i, j].slope(3);
										writer.WriteTileOld(tiles[i, j]);
									}
								}
							}
							break;
					}
				}
			}

			plr.SendSuccessMessage("Rotated clipboard {0} degrees.", degrees);
		}
	}
}