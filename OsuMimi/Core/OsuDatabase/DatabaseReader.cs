using System;
using System.IO;
using System.Text;

namespace OsuMimi.Core.OsuDatabase
{
    class DatabaseReader : BinaryReader
    {
        public DatabaseReader(Stream stream)
            : base(stream, Encoding.UTF8)
        {
        }

        public override string ReadString()
        {
            if (ReadByte() == 0)
            {
                return null;
            }
            return base.ReadString();
        }

        public DateTime ReadDateTime()
        {
            long ticks = ReadInt64();
            if (ticks < 0)
            {
                throw new Exception("ticks < 0");
            }
            return new DateTime(ticks, DateTimeKind.Utc);
        }

        public int ReadInt()
        {
            return ReadInt32();
        }
    }
}
