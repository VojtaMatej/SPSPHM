using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SPSPHM.Model
{

    public abstract class BaseModel
    {
        private bool _isDeleted;
        private bool _isDirty = true;
        private bool _isEditable = true;
        private bool _isNew = true;
        private bool _isValid = false;

        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public virtual void Insert() { throw new NotImplementedException("MethodNotImplemented"); }
        public virtual void Update() { throw new NotImplementedException("MethodNotImplemented"); }
        public virtual void Delete() { throw new NotImplementedException("MethodNotImplemented"); }
    }
}
