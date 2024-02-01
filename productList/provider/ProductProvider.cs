using Npgsql;
using Npgsql.Internal.Postgres;
using productList.model;
using System;
using System.Collections.Generic;
using System.IO;

namespace productList.provider
{
    public  class ProductProvider
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=product_list;User Id=postgres;Password=0987");

        public List<Product> GetAllProducts()
        {
            connection.Open();

            string commandBase = "SELECT p.id, p.article, p.title, p.description, p.production_shop_number, p.cost, tp.title as type, p.image FROM products p " +
                "INNER JOIN type_of_product tp ON tp.id = p.type_id";

            var command = new NpgsqlCommand(commandBase, connection);

            var reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
            while(reader.Read())
            {
                var product = new Product();
                product.Id = (int)reader["id"];
                product.Article = (string)reader["article"];
                product.Title = (string)reader["title"];
                product.Description = (string)reader["description"];
                product.ProductShopNumber = (int)reader["production_shop_number"];
                product.Cost = (decimal)reader["cost"];
                product.Type = (string)reader["type"];
                if (reader["image"] != DBNull.Value)
                {
                    product.Image = (byte[])reader["image"];

                }
                products.Add(product);
            }
            reader.Close();
            connection.Close();
            return products;
        }


        public List<string> GetAllTypes()
        {
            connection.Open();
            var command = new NpgsqlCommand("SELECT t.title FROM type_of_product t", connection);
            var reader = command.ExecuteReader();

            List<string> types = new List<string>();

            while (reader.Read()) 
            {
                types.Add((string)reader["title"]);
            }

            reader.Close();
            connection.Close();

            return types;
        }
    }
}
