//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KomalliServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuCard()
        {
            this.FoodOrder_MenuCard = new HashSet<FoodOrder_MenuCard>();
        }
    
        public string KeyCard { get; set; }
        public string NameFood { get; set; }
        public string Section { get; set; }
        public int Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodOrder_MenuCard> FoodOrder_MenuCard { get; set; }
    }
}
