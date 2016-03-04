using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace ShoeStores
{
  public class HomeModule : NancyModule
  {

    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/stores"] = _ => {
        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Post["/stores/new"] = _ => {
        Store newStore = new Store(Request.Form["store-name"]);
        newStore.Save();
        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Get["/stores/delete_all"] = _ => {
        Store.DeleteAll();
        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Get["/stores/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(parameters.id);
        List<Brand> StoreBrand = SelectedStore.GetBrands();
        List<Store> AllStores = Store.GetAll();
        model.Add("store", SelectedStore);
        model.Add("brands", StoreBrand);
        model.Add("stores", AllStores);
        return View["brands.cshtml", model];
      };

      Post["/stores/{id}/new"] = parameters => {
        Brand newBrand = new Brand(Request.Form["name"], Request.Form["store-id"]);
        newBrand.Save();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(parameters.id);
        List<Brand> StoreBrand = SelectedStore.GetBrands();
        List<Store> AllStores = Store.GetAll();
        model.Add("store", SelectedStore);
        model.Add("brands", StoreBrand);
        model.Add("stores", AllStores);
        return View["brands.cshtml", model];
      };

      Get["/store/edit/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        return View["store_edit.cshtml", SelectedStore];
      };

      Patch["/store/edit/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        SelectedStore.Update(Request.Form["store-name"]);

        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Delete["/store/delete/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        SelectedStore.Delete();

        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Get["/brand/edit/{id}"] = parameters => {
        Brand SelectedBrand = Brand.Find(parameters.id);
        return View["brand_edit.cshtml", SelectedBrand];
      };

      Patch["/brand/edit/{id}"] = parameters => {
        Brand SelectedBrand = Brand.Find(parameters.id);
        SelectedBrand.Update(Request.Form["brand-name"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(SelectedBrand.GetStoreId());
        List<Brand> StoreBrand = SelectedStore.GetBrands();
        List<Store> AllStores = Store.GetAll();
        model.Add("store", SelectedStore);
        model.Add("brands", StoreBrand);
        model.Add("stores", AllStores);
        return View["brands.cshtml", model];
      };

      Delete["/brand/delete/{id}"] = parameters => {
        Brand SelectedBrand = Brand.Find(parameters.id);
        SelectedBrand.Delete();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(SelectedBrand.GetStoreId());
        List<Brand> StoreBrand = SelectedStore.GetBrands();
        List<Store> AllStores = Store.GetAll();
        model.Add("store", SelectedStore);
        model.Add("brands", StoreBrand);
        model.Add("stores", AllStores);
        return View["brands.cshtml", model];
      };
    }
  }
}
