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
    
    public partial class FoodOrder_SetMenu
    {
        public string IDFoodOrderFoodOrder { get; set; }
        public string KeySetMenuSetMenu { get; set; }
        public int Quantity { get; set; }
        public int SellingPrice { get; set; }
        public Nullable<int> UnitPrice { get; set; }
    
        public virtual FoodOrder FoodOrder { get; set; }
        public virtual SetMenu SetMenu { get; set; }
    }
}
