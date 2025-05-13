﻿namespace F_ECommerce.Core.ViewModels.ProductVMs;

public class ProductParams
{
   //string sort, int? CategoryId, int pageSize, int PageNumber
   public string Sort { get; set; }
   public int? CategoryId { get; set; }

   public int TotatlCount { get; set; }

   public string Search { get; set; }
   public int MaxPageSize { get; set; } = 6;
   private int _pageSize = 3;

   public int PageSize
   {
      get { return _pageSize; }
      set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
   }
   public int PageNumber { get; set; } = 1;

}
