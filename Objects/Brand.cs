using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores.Objects
{
  public class Brand
  {
    private int _id;
    private string _brand_name;

    public Author(string BrandName, int Id = 0)
    {
      _id = Id;
      _brand_name = BrandName;
    }
  }
}
