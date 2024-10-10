using System;
using System.Collections.Generic;
using System.Text;

namespace BuySell.Model
{
    internal class MyFavoritesModel
    {
       public int PageNumber { get; set; } 
       public int PageSize { get; set; } 
       public int? LoggedUserId { get; set; }
        
    }
}
