using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _restaurantName;
    private int _cuisineId;

    public Restaurant(string restaurantName, int cuisineId, int Id = 0)
    {
      _id = Id;
      _restaurantName = restaurantName;
      _cuisineId = cuisineId;
    }

    public static void DeleteAll()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", connection);
      cmd.ExecuteNonQuery();
      connection.Close();
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", connection);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int cuisineId = rdr.GetInt32(2);

        Restaurant newRestaurant = new Restaurant(restaurantName, cuisineId);
        allRestaurants.Add(newRestaurant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (connection != null)
      {
        connection.Close();
      }

      return allRestaurants;
    }
  }
}
