using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Model.Utilities {
    public enum UserRole {
        Cajero,
        [Display(Name = "Jefe de cocina")]
        JefeCocina,
        [Display(Name = "Personal de cocina")]
        PersonalCocina,
        Invalid,
        Gerente
    }

    
}

