// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuMimi.Extensions
{
    static class StringExtensions
    {
        public static string Fmt(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}
