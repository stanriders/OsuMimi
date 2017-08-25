// Copyright (c) 2017-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System.Collections.Generic;
using System.IO;

namespace OsuMimi.Core.OsuDatabase
{
    public struct StarRating
    {
        private Dictionary<long, double> ratings;

        public static StarRating ReadFromStream(Stream stream)
        {
            var reader = new DatabaseReader(stream);

            var result = new StarRating();
            result.ratings = new Dictionary<long, double>();

            int count = reader.ReadInt();
            for (int i = 0; i < count; i++)
            {
                byte control = reader.ReadByte();
                int mods = reader.ReadInt();
                control = reader.ReadByte();
                double rating = reader.ReadDouble();
                result.ratings.Add(mods, rating);
            }

            return result;
        }

        public double GetRating(long mods)
        {
            if (!ratings.ContainsKey(mods))
            {
                return -1;
            }
            return ratings[mods];
        }
    }
}
