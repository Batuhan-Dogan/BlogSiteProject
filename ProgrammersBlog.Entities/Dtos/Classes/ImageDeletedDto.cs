﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Classes
{
    public class ImageDeletedDto
    {
        public string FullName { get; set; }
        public string Exstension { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
