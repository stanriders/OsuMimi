// Copyright (c) 2017-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System.Collections.Generic;
using System.IO;

namespace OsuMimi.Core.OsuDatabase
{
    public struct TimingPoint
    {
        public double Bpm { get; set; }

        public double Offset { get; set; }

        public bool PointType { get; set; }

        public static TimingPoint[] ReadPointsFromStream(Stream stream)
        {
            var reader = new DatabaseReader(stream);
            var result = new List<TimingPoint>();
            int count = reader.ReadInt();

            for (int i = 0; i < count; i++)
            {
                result.Add(ReadPointFromStream(stream));
            }

            return result.ToArray();
        }

        public static TimingPoint ReadPointFromStream(Stream stream)
        {
            var reader = new DatabaseReader(stream);

            double bpm = reader.ReadDouble();
            double offset = reader.ReadDouble();
            bool type = reader.ReadBoolean();

            return new TimingPoint
            {
                Bpm = bpm,
                Offset = offset,
                PointType = type
            };
        }
    }
}
