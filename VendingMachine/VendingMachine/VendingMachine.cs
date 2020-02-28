using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine
    {
        private decimal _currentbalance { get; set; }
        private string _selectedProduct { get; set; }
        public class Product
        {
            public string ProductName { get; set; }
            public decimal Price { get; set; }
        }

        private List<decimal> _validAmounts { get; }

        private List<Product> _productList { get; set; }

        public VendingMachine()
        {
            _currentbalance = 0;
            _selectedProduct = string.Empty;
            _productList = new List<Product>{ 
                new Product{ProductName = "Coke", Price = 25.0M },
                new Product{ProductName = "Pepsi", Price = 35.0M },
                new Product{ProductName = "Soda", Price = 45.0M },
                new Product{ProductName = "ChocolateBar", Price = 20.25M },
                new Product{ProductName = "ChewingGum", Price = 10.50M },
                new Product{ProductName = "BottledWater", Price = 15.0M  }
            };

            _validAmounts = new List<decimal> {100,50,20,10,5,1,0.5M,0.25M};
        }
               
        public  void InsertAmount(decimal amount)
        {
            //1. Accepts bills of 100,50 & 20 then coins of 10,5,1,50 & 25 Cents
            if (isValidAmount(amount))
            {
                _currentbalance += amount;
            }
            else
            {
                throw new InvalidOperationException("Invalid Amount");                
            }            
        }


        public  string GetSelectedProduct()
        {
            return _selectedProduct;
        }

        public bool isValidAmount(decimal amount)
        {
            foreach(var _amount in _validAmounts)
            {
                if (_amount == amount)
                    return true;
            }

            return false;
        }

        public void SelectProduct(string productname)
        {
            if (_productList.Any(p => p.ProductName == productname))
            {
                _selectedProduct = _productList.Where(p => p.ProductName == productname).FirstOrDefault().ProductName;
            }
            else
            {
                throw new InvalidOperationException("Invalid Product Name");
            }
        }
        
        public decimal GetCurrentBalance()
        {
            return _currentbalance;
        }

        public void CancelAndRefund()
        {
            _currentbalance = 0;
        }

        public void BuySelectedProduct(string productname)
        {
            if (_productList.Any(p => p.ProductName == productname))
            {
                var price = _productList.Where(p => p.ProductName == productname).FirstOrDefault().Price;

                if(price > _currentbalance)
                {
                    throw new InvalidOperationException("Insufficient Amount Inserted");
                }
                else
                {
                    _currentbalance = _currentbalance - price;
                }

                _selectedProduct = _productList.Where(p => p.ProductName == productname).FirstOrDefault().ProductName;
            }
            else
            {
                throw new InvalidOperationException("Invalid Product Name");
            }
        }
    }
}
