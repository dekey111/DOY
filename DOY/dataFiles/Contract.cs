//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOY.dataFiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract
    {
        public int ID_Contract { get; set; }
        public Nullable<int> id_Children { get; set; }
        public Nullable<System.DateTime> DateContract { get; set; }
        public Nullable<int> id_Parent { get; set; }
        public Nullable<int> Pay { get; set; }
        public Nullable<int> id_Group { get; set; }
    
        public virtual Children Children { get; set; }
        public virtual Group Group { get; set; }
        public virtual Parent Parent { get; set; }
    }
}
