﻿// Copyright (c) 2016 Lukasz Jaskiewicz. All Rights Reserved
// Licenced under the European Union Public Licence 1.1 (EUPL v.1.1)
// See licence.txt in the project root for licence information
// Written by Lukasz Jaskiewicz (lukasz@jaskiewicz.de)

namespace Net.DevDone.Office.ODF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ODFTag
    {
        public static class office
        {
            public static readonly string body = "office:body";
            public static readonly string text = "office:text";
        }

        public static class text
        {
            public static readonly string h = "text:h";
        }
    }
}
