using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IEntity<Tid>
    {
        Tid Id { get; set; }
    }
}
