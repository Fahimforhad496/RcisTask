using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.Interfaces
{
    public interface ITrackable
    {
        DateTimeOffset CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset LastUpdatedAt { get; set; }
        string LastUpdatedBy { get; set; }
    }
}
