//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
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
        public string Image { get; set; }
        public Nullable<int> Stock { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodOrder_MenuCard> FoodOrder_MenuCard { get; set; }
    }
}
