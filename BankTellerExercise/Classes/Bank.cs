using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTellerExercise.Classes
{
	public class Bank
	{
		/// <summary>
		/// Priavtely holds the list of customers
		/// </summary>
		private List<BankCustomer> customers = new List<BankCustomer>();

		/// <summary>
		/// Represents the Customers of the bank
		/// </summary>
		public BankCustomer[] Customers { get { return customers.ToArray(); } }

		/// <summary>
		/// Represents the banks total assets
		/// </summary>
		public decimal Assests
		{
			get
			{
				decimal totalAssets = 0;
				foreach (BankCustomer customer in Customers)
				{
					foreach (BankAccount account in customer.Accounts)
					{
						totalAssets += account.Balance;
					}
				}
				return totalAssets;
			}
		}

		public List<string> AccountNumbers
		{
			get
			{
				List<string> accounts = new List<string>();
				foreach (BankCustomer customer in Customers)
				{
					foreach (BankAccount account in customer.Accounts)
					{
						accounts.Add(account.AccountNumber);
					}
				}
				return accounts;
			}
		}

		/// <summary>
		/// Add a Customer to the bank
		/// </summary>
		/// <param name="newCustomer">A customer to be added</param>
		public void AddCustomer(BankCustomer newCustomer)
		{
			customers.Add(newCustomer);
		}
	}
}
