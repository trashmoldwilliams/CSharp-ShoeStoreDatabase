using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores.Objects
{
  public class StoreTest : IDisposable
  {
    public void BrandTestDB()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_brands_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_BrandsEmptyAtFirst()
    {
      int result = Brand.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverride_TrueForSameName()
    {
      var brandOne = new Brand("Nike");
      var brandTwo = new Brand("Nike");

      Assert.Equal(brandOne, brandTwo);
    }

    [Fact]
    public void Test_Save_SavesBrandDataBase()
    {
      var testBrand = new Brand("Nike");
      testBrand.Save();

      var testList = new List<Brand>{testBrand};
      var result = Brand.GetAll();

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToOBjects()
    {
      var testBrand = new Brand("Nike");
      testBrand.Save();

      var savedBrand = Brand.GetAll()[0];

      int result = savedBrand.GetId();
      int testId = testBrand.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindBrandInDataBase()
    {
      var testBrand = new Brand("Nike");
      testBrand.Save();

      var foundBrand = Brand.Find(testBrand.GetId());

      Assert.Equal(testBrand, foundBrand);
    }

    public void Dispose()
    {
      Brand.DeleteAll();
    }
  }
}
