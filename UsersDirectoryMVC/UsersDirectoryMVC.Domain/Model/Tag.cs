﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UsersDirectoryMVC.Domain.Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AssignmentTag> AssignmentTags { get; set; }
    }
}
