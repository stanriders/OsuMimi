// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;

namespace OsuMimi.Core.OsuDatabase
{
    class OsuDatabase
    {
        public int OsuVersion { get; set; }

        public int FolderCount { get; set; }

        public bool AccoundUnlocked { get; set; }

        public DateTime UnlockTime { get; set; }

        public string PlayerName { get; set; }

        public int BeatmapsCount { get; set; }

        public BeatmapInformation[] Beatmaps { get; set; }

        public int UnknownInt { get; set; }

        public OsuDatabase(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                using (var reader = new DatabaseReader(stream))
                {
                    OsuVersion = reader.ReadInt();
                    FolderCount = reader.ReadInt();
                    AccoundUnlocked = reader.ReadBoolean();
                    UnlockTime = reader.ReadDateTime();
                    PlayerName = reader.ReadString();
                    BeatmapsCount = reader.ReadInt();
                    var beatmaps = new List<BeatmapInformation>();
                    for (int i = 0; i < BeatmapsCount; i++)
                    {
                        beatmaps.Add(BeatmapInformation.ReadFromStream(stream));
                    }
                    Beatmaps = beatmaps.ToArray();
                    UnknownInt = reader.ReadInt();
                }
            }
        }
    }
}
