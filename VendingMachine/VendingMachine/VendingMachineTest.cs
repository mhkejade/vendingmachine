using System;
using NUnit.Framework;

namespace VendingMachine
{
    [TestFixture]
    public class VendingMachineTest
    {
        //initial scenario 
        [Test]
        public void WhenVendingMachineStarts_ThenCurrentBalanceShouldBeZero()
        {
            var vendingmachine = new VendingMachine();

            Assert.AreEqual(0, vendingmachine.GetCurrentBalance());
        }

        //initial scenario 
        [Test]
        public void WhenVendingMachineStarts_ThenSelectedProductIsNullOrEmpty()
        {
            var vendingmachine = new VendingMachine();

            Assert.IsEmpty(vendingmachine.GetSelectedProduct());
        }


        // 1. Accepts bills of 100,50 & 20 then coins of 10,5,1,50 & 25 Cents  
        [TestCase(150)]
        [TestCase(15.0)]
        public void WhenUserInsertsAmount_IfInvalidAmount_ThenItShouldThrowInvalidAmountException(decimal amount)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var vendingmachine = new VendingMachine();
                vendingmachine.InsertAmount(amount);
            });
        }

        //1. Accepts bills of 100,50 & 20 then coins of 10,5,1,50 & 25 Cents 
        [TestCase(100.0)]
        [TestCase(50.0)]
        [TestCase(20.0)]
        [TestCase(10.0)]
        [TestCase(5.0)]
        [TestCase(1.0)]
        [TestCase(0.5)]
        [TestCase(0.25)]
        public void WhenUserInsertsAmount_ThenVendingMachingOnlyAcceptsValidAmount_100_50_20_10_5_1_050_025(decimal expected)
        {
            var vendingmachine = new VendingMachine();

            Assert.IsTrue(vendingmachine.isValidAmount(expected));

        }


        // 2. Allow user to select products Coke(25), Pepsi(35), Soda(45),chocolate bar (20.25),Chewing gum (10.50), Bottled water (15) 
        [TestCase("Bbq")]
        [TestCase("Siomai")]
        public void WhenUserSelectsProduct_IfInvalidProduct_ThenThrowInvalidProductException(string productname)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var vendingmachine = new VendingMachine();
                vendingmachine.SelectProduct(productname);
            });
        }

        // 2. Allow user to select products Coke(25), Pepsi(35), Soda(45),chocolate bar (20.25),Chewing gum (10.50), Bottled water (15) 
        [TestCase("Coke")]
        [TestCase("Pepsi")]
        [TestCase("Soda")]
        [TestCase("ChocolateBar")]
        [TestCase("ChewingGum")]
        [TestCase("BottledWater")]
        public void WhenUserSelectsProduct_ThenCanSelectFromValidList(string expected)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.SelectProduct(expected);

            Assert.AreEqual(expected, vendingmachine.GetSelectedProduct());
        }
        
        //3. Allow user to take refund by canceling the request. 
        [TestCase(150, 100, 50)]
        public void WhenUserCancel_ThenReturnTotalAmountInserted(decimal expected, decimal amount1, decimal amount2)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount1);
            vendingmachine.InsertAmount(amount2);

            Assert.AreEqual(expected, vendingmachine.GetCurrentBalance());
        }


        //3. Allow user to take refund by canceling the request. 
        [TestCase(0)]
        public void WhenUserCancel_ThenCurrentBalanceBecomesZero(decimal expected)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.CancelAndRefund();

            Assert.AreEqual(expected, vendingmachine.GetCurrentBalance());
        }


        // 4. Return selected product and remaining change if any
        [TestCase("Coke", 100)]
        [TestCase("Pepsi", 50)]
        public void WhenUserBuysSelectedProduct_IfAmountInsertedIsSufficient_ThenDispenseProduct(string expected, decimal amount)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount);
            vendingmachine.BuySelectedProduct(expected);

            Assert.AreEqual(expected, vendingmachine.GetSelectedProduct());
        }

        // 4. Return selected product and remaining change if any
        [TestCase("Soda", 50, 20)]
        [TestCase("ChocolateBar", 20, 0.25)]
        public void WhenUserBuysSelectedProduct_IfMultipleAmountInsertedIsSufficient_ThenDispenseProduct(string expected, decimal amount1, decimal amount2)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount1);
            vendingmachine.InsertAmount(amount2);
            vendingmachine.BuySelectedProduct(expected);

            Assert.AreEqual(expected, vendingmachine.GetSelectedProduct());
        }
        

        // 4. Return selected product and remaining change if any
        [TestCase("Coke", 100, 75)]
        [TestCase("Pepsi", 50, 15)]
        public void WhenUserBuysSelectedProduct_IfAmountInsertedIsSufficient_ThenProvideChangeIfAny(string productname, decimal amount, decimal change)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount);
            vendingmachine.BuySelectedProduct(productname);

            Assert.AreEqual(change, vendingmachine.GetCurrentBalance());
        }


        // 4. Return selected product and remaining change if any
        [TestCase("ChewingGum", 10, 1, 0.5)]
        [TestCase("BottledWater", 10, 10, 5)]
        public void WhenUserBuysSelectedProduct_IfMultipleAmountInsertedIsSufficient_ThenProvideChangeIfAny(string productname, decimal amount1, decimal amount2, decimal change)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount1);
            vendingmachine.InsertAmount(amount2);
            vendingmachine.BuySelectedProduct(productname);

            Assert.AreEqual(change, vendingmachine.GetCurrentBalance());
        }


        //5. Return message if inserted money is insufficient to the selected product
        [TestCase("Coke", 10)]
        [TestCase("Pepsi", 5)]
        public void WhenUserBuysSelectedProduct_IfAmountInsertIsInsufficient_ThenReturnException(string productname, decimal amount)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount);

            Assert.Throws<InvalidOperationException>(() =>
            {
                vendingmachine.BuySelectedProduct(productname);
            });
        }

        //5. Return message if inserted money is insufficient to the selected product
        [TestCase("Coke", 10, 1)]
        [TestCase("Pepsi", 5, 5)]
        public void WhenUserBuysSelectedProduct_IfMultipleAmountInsertIsInsufficient_ThenReturnException(string productname, decimal amount1, decimal amount2)
        {
            var vendingmachine = new VendingMachine();

            vendingmachine.InsertAmount(amount1);
            vendingmachine.InsertAmount(amount2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                vendingmachine.BuySelectedProduct(productname);
            });
        }
    }
}
