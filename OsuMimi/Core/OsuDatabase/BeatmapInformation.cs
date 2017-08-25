// Copyright (c) 2017-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.IO;

namespace OsuMimi.Core.OsuDatabase
{
    public struct BeatmapInformation
    {
        public int EntrySize { get; set; }

        public string ArtistName { get; set; }

        public string ArtistNameUnicode { get; set; }

        public string SongTitle { get; set; }

        public string SongTitleUnicode { get; set; }

        public string CreatorName { get; set; }

        public string Difficulty { get; set; }

        public string AudioFileName { get; set; }

        public string Md5Hash { get; set; }

        public string NameOfOsuFile { get; set; }

        public byte RankedStatus { get; set; }

        public short HitcirclesCount { get; set; }

        public short SlidersCount { get; set; }

        public short SpinnersCount { get; set; }

        public long LastModificationTime { get; set; }

        public Single ApproachRate { get; set; }

        public Single CircleSize { get; set; }

        public Single HpDrain { get; set; }

        public Single OverallDifficulty { get; set; }

        public double SliderVelocity { get; set; }

        public StarRating StdStars { get; set; }

        public StarRating TaikoStars { get; set; }

        public StarRating CtbStars { get; set; }

        public StarRating ManiaStars { get; set; }

        public int DrainTime { get; set; }

        public int TotalTime { get; set; }

        public int TimeIdkLol { get; set; }

        public TimingPoint[] TimingPoints { get; set; }

        public int BeatmapId { get; set; }

        public int BeatmapSetId { get; set; }

        public int ThreadId { get; set; }

        public byte GradeAchievedOsu { get; set; }

        public byte GradeAchievedTaiko { get; set; }

        public byte GradeAchievedCtb { get; set; }

        public byte GradeAchievedMania { get; set; }

        public short LocalBeatmapOffset { get; set; }

        public Single StackLeniency { get; set; }

        public byte OsuGameplayMode { get; set; }

        public string SongSource { get; set; }

        public string SongTags { get; set; }

        public short OnlineOffset { get; set; }

        public string TitleSongFont { get; set; }

        public bool IsUnplayed { get; set; }

        public long LastPlayTime { get; set; }

        public bool IsOsz2 { get; set; }

        public string FolderName { get; set; }

        public long LastCheckedTime { get; set; }

        public bool IgnoreHitsounds { get; set; }

        public bool IgnoreSkin { get; set; }

        public bool DisableStoryboard { get; set; }

        public bool DisableVideo { get; set; }

        public bool VisualOverride { get; set; }

        public int LastEditTime { get; set; }

        public byte ManiaScrollSpeed { get; set; }

        public static BeatmapInformation ReadFromStream(Stream stream)
        {
            var reader = new DatabaseReader(stream);

            return new BeatmapInformation
            {
                EntrySize = reader.ReadInt(),
                ArtistName = reader.ReadString(),
                ArtistNameUnicode = reader.ReadString(),
                SongTitle = reader.ReadString(),
                SongTitleUnicode = reader.ReadString(),
                CreatorName = reader.ReadString(),
                Difficulty = reader.ReadString(),
                AudioFileName = reader.ReadString(),
                Md5Hash = reader.ReadString(),
                NameOfOsuFile = reader.ReadString(),
                RankedStatus = reader.ReadByte(),
                HitcirclesCount = reader.ReadInt16(),
                SlidersCount = reader.ReadInt16(),
                SpinnersCount = reader.ReadInt16(),
                LastModificationTime = reader.ReadInt64(),
                ApproachRate = reader.ReadSingle(),
                CircleSize = reader.ReadSingle(),
                HpDrain = reader.ReadSingle(),
                OverallDifficulty = reader.ReadSingle(),
                SliderVelocity = reader.ReadDouble(),
                StdStars = StarRating.ReadFromStream(stream),
                TaikoStars = StarRating.ReadFromStream(stream),
                CtbStars = StarRating.ReadFromStream(stream),
                ManiaStars = StarRating.ReadFromStream(stream),
                DrainTime = reader.ReadInt(),
                TotalTime = reader.ReadInt(),
                TimeIdkLol = reader.ReadInt(),
                TimingPoints = TimingPoint.ReadPointsFromStream(stream),
                BeatmapId = reader.ReadInt(),
                BeatmapSetId = reader.ReadInt(),
                ThreadId = reader.ReadInt(),
                GradeAchievedOsu = reader.ReadByte(),
                GradeAchievedTaiko = reader.ReadByte(),
                GradeAchievedCtb = reader.ReadByte(),
                GradeAchievedMania = reader.ReadByte(),
                LocalBeatmapOffset = reader.ReadInt16(),
                StackLeniency = reader.ReadSingle(),
                OsuGameplayMode = reader.ReadByte(),
                SongSource = reader.ReadString(),
                SongTags = reader.ReadString(),
                OnlineOffset = reader.ReadInt16(),
                TitleSongFont = reader.ReadString(),
                IsUnplayed = reader.ReadBoolean(),
                LastPlayTime = reader.ReadInt64(),
                IsOsz2 = reader.ReadBoolean(),
                FolderName = reader.ReadString(),
                LastCheckedTime = reader.ReadInt64(),
                IgnoreHitsounds = reader.ReadBoolean(),
                IgnoreSkin = reader.ReadBoolean(),
                DisableStoryboard = reader.ReadBoolean(),
                DisableVideo = reader.ReadBoolean(),
                VisualOverride = reader.ReadBoolean(),
                LastEditTime = reader.ReadInt(),
                ManiaScrollSpeed = reader.ReadByte()
            };
        }
    }
}
