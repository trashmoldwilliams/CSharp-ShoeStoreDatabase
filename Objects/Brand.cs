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

    public Brand(string BrandName, int Id = 0)
    {
      _id = Id;
      _brand_name = BrandName;
    }

    public override bool Equals(System.Object otherBrand)
    {
      if(!(otherBrand is Brand))
      {
        return false;
      }
      else
      {
        var newBrand = (Brand) otherBrand;
        bool idEquality = this.GetId() == newBrand.GetId();
        bool nameEquality = this.GetName() == newBrand.GetName();
        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _brand_name;
    }

    public static List<Brand> GetAll()
    {
      var AllBrands = new List<Brand>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM brands", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int BrandId = rdr.GetInt32(0);
        string BrandName = rdr.GetString(1);

        var newBrand = new Brand(BrandName, BrandId);
        AllBrands.Add(newBrand);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }

      return AllBrands;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      var cmd = new SqlCommand("INSERT INTO brands (name) OUTPUT INSERTED.id VALUES (@BrandName);", conn);

      var nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BrandName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Brand Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM brands WHERE id = @BrandId;", conn);
      var brandIdParameter = new SqlParameter();
      brandIdParameter.ParameterName = "@BrandId";
      brandIdParameter.Value = id.ToString();
      cmd.Parameters.Add(brandIdParameter);
      rdr = cmd.ExecuteReader();

      int foundBrandId = 0;
      string foundBrandName = null;

      while(rdr.Read())
      {
        foundBrandId = rdr.GetInt32(0);
        foundBrandName = rdr.GetString(1);
      }

      var foundBrand = new Brand(foundBrandName, foundBrandId);

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }

      return foundBrand;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM brands", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
