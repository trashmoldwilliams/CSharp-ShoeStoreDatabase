using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using ShoeStores.Objects;

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

      Get["/brands"] = _ => {
        List<Brand> AllBrands = Brand.GetAll();
        return View["brands.cshtml", AllBrands];
      };

      Post["/stores/new"] = _ => {
        Store newStore = new Store(Request.Form["name"]);
        newStore.Save();
        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Post["/brands/new"] = _ => {
        Brand newBrand = new Brand(Request.Form["name"]);
        newBrand.Save();
        List<Brand> AllBrands = Brand.GetAll();
        return View["brands.cshtml", AllBrands];
      };

      Get["/stores/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(parameters.id);
        List<Brand> StoreBrands = SelectedStore.GetBrands();
        model.Add("store", SelectedStore);
        model.Add("brands", StoreBrands);
        return View["store_brands.cshtml", model];
      };

      Get["/brands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Brand SelectedBrand = Brand.Find(parameters.id);
        List<Store> BrandStores = SelectedBrand.GetStores();
        model.Add("brand", SelectedBrand);
        model.Add("stores", BrandStores);
        return View["brand_stores.cshtml", model];
      };

      Get["/store/add/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store SelectedStore = Store.Find(parameters.id);
        List<Brand> AllBrands = Brand.GetAll();
        model.Add("store", SelectedStore);
        model.Add("brands", AllBrands);
        return View["add_store.cshtml", model];
      };

      Post["/store/add/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        Brand SelectedBrand = Brand.Find(Request.Form["id"]);
        SelectedBrand.AddStore(SelectedStore);

        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Get["/brand/add/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Brand SelectedBrand = Brand.Find(parameters.id);
        List<Store> AllStores = Store.GetAll();
        model.Add("brand", SelectedBrand);
        model.Add("stores", AllStores);
        return View["add_brand.cshtml", model];
      };

      Post["/brand/add/{id}"] = parameters => {
        Brand SelectedBrand = Brand.Find(parameters.id);
        Store SelectedStore = Store.Find(Request.Form["id"]);
        SelectedStore.AddBrand(SelectedBrand);

        List<Brand> AllBrands = Brand.GetAll();
        return View["brands.cshtml", AllBrands];
      };

      Get["/store/edit/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        return View["store_edit.cshtml", SelectedStore];
      };

      Patch["/store/edit/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        SelectedStore.Update(Request.Form["name"]);

        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };

      Delete["/store/delete/{id}"] = parameters => {
        Store SelectedStore = Store.Find(parameters.id);
        SelectedStore.Delete();

        List<Store> AllStores = Store.GetAll();
        return View["stores.cshtml", AllStores];
      };
    }
  }
}
