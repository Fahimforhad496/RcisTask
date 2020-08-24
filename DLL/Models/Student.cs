using System;
using System.Collections.Generic;
using System.Text;
using DLL.Models.Interfaces;

namespace DLL.Models
{
    public class Student:ISoftDeletable
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
