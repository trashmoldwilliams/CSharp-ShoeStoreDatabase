using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores.Objects
{
  public class StoreTest : IDisposable
  {
    public void StoreTestDB()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_stores_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StoresEmptyAtFirst()
    {
      int result = Store.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverride_TrueForSameName()
    {
      var storeOne = new Store("Target");
      var storeTwo = new Store("Target");

      Assert.Equal(storeOne, storeTwo);
    }

    [Fact]
    public void Test_Save_SavesStoreDataBase()
    {
      var testStore = new Store("Target");
      testStore.Save();

      var testList = new List<Store>{testStore};
      var result = Store.GetAll();

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToOBjects()
    {
      var testStore = new Store("Target");
      testStore.Save();

      var savedStore = Store.GetAll()[0];

      int result = savedStore.GetId();
      int testId = testStore.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindStoreInDataBase()
    {
      var testStore = new Store("Target");
      testStore.Save();

      var foundStore = Store.Find(testStore.GetId());

      Assert.Equal(testStore, foundStore);
    }

    [Fact]
    public void Test_AddBrand_AddsBrandToStore()
    {
      //Arrange
      Store testStore = new Store("Target");
      testStore.Save();

      Brand testBrand = new Brand("Nike");
      testBrand.Save();

      Brand testBrand2 = new Brand("Adidas");
      testBrand2.Save();

      //Act
      testStore.AddBrand(testBrand);
      testStore.AddBrand(testBrand2);

      List<Brand> result = testStore.GetBrands();
      List<Brand> testList = new List<Brand>{testBrand, testBrand2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetBrands_ReturnsAllStoreBrands()
    {
      //Arrange
      Store testStore = new Store("Target");
      testStore.Save();

      Brand testBrand1 = new Brand("Nike");
      testBrand1.Save();

      Brand testBrand2 = new Brand("Adidas");
      testBrand2.Save();

      testStore.AddBrand(testBrand1);
      testStore.AddBrand(testBrand2);


      List<Brand> resultList = testStore.GetBrands();
      List<Brand> compareList = new List<Brand> {testBrand1, testBrand2};

      //Assert
      Assert.Equal(resultList, compareList);
    }

    [Fact]
    public void Test_Update_UpdatesStoreInDataBase()
    {
      var storeNameOne = "Target";
      var testStore = new Store(storeNameOne);
      testStore.Save();

      var storeNameTwo = "Macy's";
      testStore.Update(storeNameTwo);

      Store result = new Store(testStore.GetName());

      Assert.Equal(result.GetName(), storeNameTwo);
    }

    [Fact]
    public void Test_Delete_DeletesStoreFromDatabase()
    {

      string nameOne = "Huckleberry Finn";
      Brand testBrand = new Brand(nameOne);
      testBrand.Save();

      Store testStore1 = new Store("Target");
      testStore1.Save();
      Store testStore2 = new Store("Macy's");
      testStore2.Save();

      testBrand.AddStore(testStore1);
      testBrand.AddStore(testStore2);

      testStore2.Delete();

      List<Store> resultStores = testBrand.GetStores();
      List<Store> testStoreList = new List<Store> {testStore1};

      List<Store> storesList = Store.GetAll();

      Assert.Equal(testStoreList, storesList);
      Assert.Equal(testStoreList, resultStores);
    }

    public void Dispose()
    {
      Brand.DeleteAll();
      Store.DeleteAll();
    }
  }
}
