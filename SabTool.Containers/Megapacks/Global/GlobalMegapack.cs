﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SabTool.Containers.Megapacks.Global
{
    public class GlobalMegapack : MegaFile
    {
        public GlobalMap Map { get; }

        public GlobalMegapack(GlobalMap map)
        {
            Map = map;
        }

        public void Read(Stream stream)
        {
            using (var br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                if (!ReadHeader(br))
                    throw new Exception("Invalid header found!");
            }
        }

        public void Write(Stream stream)
        {

        }
    }
}
