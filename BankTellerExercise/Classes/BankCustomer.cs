using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTellerExercise.Classes
{
	public class BankCustomer
	{
		/// <summary>
		/// Priavtely holds the list of customer's bank accounts
		/// </summary>
		private List<BankAccount> accounts = new List<BankAccount>();

		/// <summary>
		/// Represents the customer's Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Represents the customer's address
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Represents the customer's phone number
		/// </summary>
		public string PhoneNumber { get; set; }
		
		/// <summary>
		/// Holds the customers accounts
		/// </summary>
		public BankAccount[] Accounts { get { return accounts.ToArray(); } }

		/// <summary>
		/// Represents whether the customer is a VIP
		/// </summary>
		public bool IsVIP { get
			{
				decimal totalWorth = 0;
				foreach (BankAccount account in accounts)
				{
					totalWorth += account.Balance;
				}
				return (totalWorth >= 25000M);
			}
		}

		/// <summary>
		/// Adds an account to the customer's accounts
		/// </summary>
		/// <param name="newAccount">An account to be added</param>
		public void AddAccount(BankAccount newAccount)
		{
			accounts.Add(newAccount);
		}
	}
}
