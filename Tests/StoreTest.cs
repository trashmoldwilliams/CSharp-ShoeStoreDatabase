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

    public void Dispose()
    {
      Store.DeleteAll();
    }
  }
}
