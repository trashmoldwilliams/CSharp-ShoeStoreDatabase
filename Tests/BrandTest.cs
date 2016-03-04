using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores.Objects
{
  public class BrandTest : IDisposable
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

    [Fact]
    public void Test_AddStore_AddsStoreToBrand()
    {
      //Arrange
      Brand testBrand = new Brand("Nike");
      testBrand.Save();

      Store testStore = new Store("Target");
      testStore.Save();

      //Act
      testBrand.AddStore(testStore);

      List<Store> result = testBrand.GetStores();
      List<Store> testList = new List<Store>{testStore};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetStores_ReturnsAllBrandStores()
    {
      //Arrange
      Brand testBrand = new Brand("Nike");
      testBrand.Save();

      Store testStore1 = new Store("Target");
      testStore1.Save();

      Store testStore2 = new Store("Macy's");
      testStore2.Save();

      testBrand.AddStore(testStore1);
      testBrand.AddStore(testStore2);


      List<Store> resultList = testBrand.GetStores();
      List<Store> compareList = new List<Store> {testStore1, testStore2};

      //Assert
      Assert.Equal(resultList, compareList);
    }

    public void Dispose()
    {
      Brand.DeleteAll();
      Store.DeleteAll();
    }
  }
}
