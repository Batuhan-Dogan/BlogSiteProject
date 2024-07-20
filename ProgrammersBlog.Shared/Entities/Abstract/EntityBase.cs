using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedTime { get; set; }= DateTime.Now.ToUniversalTime();
        public virtual DateTime ModifiedTime { get; set; }=DateTime.Now.ToUniversalTime();
        public virtual bool IsDeleted { get; set; } 
        public virtual bool IsActive { get; set; } 
        public virtual string CreatedByName { get; set; } 
        public virtual string ModifiedByName { get; set; }
        public string? Note { get; set; }
    }
}
