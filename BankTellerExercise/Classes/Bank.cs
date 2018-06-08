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
		private List<BankCustomer> customers= new List<BankCustomer>();

		/// <summary>
		/// Represents the Customers of the bank
		/// </summary>
		public BankCustomer[] Customers {get { return customers.ToArray(); } }

		/// <summary>
		/// Represents the banks total assets
		/// </summary>
		public decimal Assests { get; private set; }

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
