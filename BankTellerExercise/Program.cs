using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankTellerExercise.Classes;

namespace BankTellerExercise
{
	class Program
	{
		static void Main(string[] args)
		{
			// Initialize a few Bank Accounts, a Bank Customer, and a Bank
			CheckingAccount secondAccount = new CheckingAccount();
			secondAccount.AccountNumber = "C8975034";
			SavingsAccount thirdAccount = new SavingsAccount();
			thirdAccount.AccountNumber = "S3453455";

			BankCustomer andrew = new BankCustomer();
			andrew.AddAccount(secondAccount);
			andrew.AddAccount(thirdAccount);
			andrew.Name = "Andrew";

			Bank bank = new Bank();
			bank.AddCustomer(andrew);

			// Prompt the user for who they are
			BankCustomer user = ChooseUser(bank);

			// Prompt the user for which account to enter
			BankAccount working = PickAnAccount(user);
			Console.Clear();

			// Prompt user for what they would like to do
			string input = PromptUserForMenuChoice(user, working);

			// Evaluate their choice
			while (input != "Q")
			{
				switch (input)
				{
					case "1": // Make a Deposit
						MakeDeposit(working);
						break;
					case "2": // Make a Withdrawl
						MakeWithdrawl(working);
						break;
					case "3": // Make a Transfer
						MakeTransfer(working, user);
						break;
					case "4": // Show Balance
						ShowBalance(working);						
						break;
					case "5": //Change or add Account
						working = PickAnAccount(user);
						break;
					case "6": //Change User info
						ChangeUserInfo(user);
						break;
					case "7": //Change User 
						user = ChooseUser(bank);
						working = PickAnAccount(user);
						break;
				}
				input = PromptUserForMenuChoice(user, working);
			}
		}

		/// <summary>
		/// Shows the account Balance
		/// </summary>
		/// <param name="working">The current account</param>
		static void ShowBalance(BankAccount working)
		{
			Console.WriteLine($"Account#: {working.AccountNumber}");
			Console.WriteLine("Your current balance is: " + working.Balance.ToString("C"));
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Chooses a user
		/// </summary>
		/// <param name="bank">The Bank</param>
		/// <returns>A user</returns>
		static BankCustomer ChooseUser(Bank bank)
		{
			HashSet<string> validChoices = new HashSet<string>();
			string input = "";

			do
			{
				Console.WriteLine("Bank Customers");
				for (int i = 1; i <= bank.Customers.Length; i++)
				{
					validChoices.Add(i.ToString());
					Console.WriteLine($"{i}. User: {bank.Customers[i - 1].Name}");
				}
				Console.WriteLine($"{bank.Customers.Length + 1}. Add a user");
				Console.Write("Pick an option: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
				if (input == (bank.Customers.Length + 1).ToString())
				{
					Console.WriteLine("Adding a User:");
					BankCustomer newCustomer = new BankCustomer();
					Console.Write("Please enter your Name: ");
					newCustomer.Name = Console.ReadLine();
					Console.Write("Please enter your Address: ");
					newCustomer.Address = Console.ReadLine();
					Console.Write("Please enter your Phone Number: ");
					newCustomer.PhoneNumber = Console.ReadLine();
					bank.AddCustomer(newCustomer);
				}
			}
			while (!validChoices.Contains(input));

			int choice = int.Parse(input);
			return bank.Customers[choice - 1];
		}

		/// <summary>
		/// Allows User to update their personal information
		/// </summary>
		/// <param name="customer">The current user</param>
		static void ChangeUserInfo(BankCustomer customer)
		{
			string[] validChoices = { "1", "2", "3", "Q" };
			string input = "";
			while (input != "Q")
			{
				do
				{
					if (customer.IsVIP)
					{
						Console.WriteLine("********VIP********");
					}
					Console.WriteLine("      TE Bank");
					Console.WriteLine("-------------------");
					Console.WriteLine("       MENU");
					Console.WriteLine($"1. Change Name. Current Name: {customer.Name}");
					Console.WriteLine($"2. Change Address. Current Address: {customer.Address}");
					Console.WriteLine($"3. Change Phone Number. Current: {customer.PhoneNumber}");
					Console.WriteLine("Q. Quit to Main Menu");
					Console.WriteLine();
					Console.Write("Make a choice: ");
					input = Console.ReadLine().ToUpper();
					Console.Clear();
				}
				while (!validChoices.Contains(input));

				switch (input)
				{
					case "1": // Change Name
						Console.Write("Please enter your Name: ");
						customer.Name = Console.ReadLine();
						break;
					case "2": // Change Address
						Console.Write("Please enter your Address: ");
						customer.Address = Console.ReadLine();
						break;
					case "3": // Change Phone Number
						Console.Write("Please enter your Phone Number: ");
						customer.PhoneNumber = Console.ReadLine();
						break;
				}
			}
		}

		/// <summary>
		/// Allows the user to create a new bank account
		/// </summary>
		/// <param name="customer">The current user</param>
		static void AddAnAccount(BankCustomer customer)
		{
			string[] validChoices = { "1", "2", "Q" };
			string input = "";
			while (true)
			{
				do
				{
					if (customer.IsVIP)
					{
						Console.WriteLine("********VIP********");
					}
					Console.WriteLine("      TE Bank");
					Console.WriteLine("-------------------");
					Console.WriteLine("What Type of Account would you like to Add?");
					Console.WriteLine("1. Checking Account");
					Console.WriteLine("2. Savings Account");
					Console.WriteLine("Q. Quit to Main Menu");
					Console.WriteLine();
					Console.Write("Make a choice: ");
					input = Console.ReadLine().ToUpper();
					Console.Clear();
				}
				while (!validChoices.Contains(input));

				if (input == "Q")
				{
					break;
				}
				Random random = new Random();
				string type = (input == "1") ? "C" : "S";
				int accountNumber = random.Next(1000000, 10000000);
				Console.WriteLine($"Your new account is : {type}{accountNumber}");
				switch (input)
				{
					case "1": // Checking Account
						CheckingAccount newCheckingAccount = new CheckingAccount();
						newCheckingAccount.AccountNumber = type + accountNumber;
						customer.AddAccount(newCheckingAccount);
						break;
					case "2": // Savings Account
						SavingsAccount newSavingsAccount = new SavingsAccount();
						newSavingsAccount.AccountNumber = type + accountNumber;
						customer.AddAccount(newSavingsAccount);
						break;
				}
			}
		}

		/// <summary>
		/// Prompts the user with a Menu until the enter a valid option
		/// </summary>
		/// <returns>The users choice</returns>
		static string PromptUserForMenuChoice(BankCustomer user, BankAccount account)
		{
			string[] validChoices = { "1", "2", "3", "4", "5", "6", "7", "Q" };
			string input = "";

			do
			{
				Console.WriteLine($"User: {user.Name}");
				Console.WriteLine($"Account#: {account.AccountNumber}");
				if (user.IsVIP)
				{
					Console.WriteLine("********VIP********");
				}
				Console.WriteLine("      TE Bank");
				Console.WriteLine("-------------------");
				Console.WriteLine("     MAIN MENU");
				Console.WriteLine("1. Make a Deposit");
				Console.WriteLine("2. Make a Withdrawl");
				Console.WriteLine("3. Make a Transfer");
				Console.WriteLine("4. Show Balance");
				Console.WriteLine("5. Change or Add Account");
				Console.WriteLine("6. Change User Info");
				Console.WriteLine("7. Change User");
				Console.WriteLine("Q. Quit");
				Console.WriteLine();
				Console.Write("Make a choice: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
			}
			while (!validChoices.Contains(input));

			return input;
		}

		/// <summary>
		/// Handles the Deposit transaction
		/// </summary>
		/// <param name="userAccount">The User Account to change</param>
		static void MakeDeposit(BankAccount userAccount)
		{
			Console.Write("How much are you depositing?: ");
			decimal ammountToDeposit = GetANumber();
			userAccount.Deposit(ammountToDeposit);
			Console.WriteLine("Deposit Successful!");
			Console.Beep(988, 75);
			Console.Beep(1319, 525);
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Handles the Withdrawl transaction
		/// </summary>
		/// <param name="userAccount">The User Account to change</param>
		static void MakeWithdrawl(BankAccount userAccount)
		{
			Console.Write("How much are you Withdrawing?: ");
			decimal ammountToWithdraw = GetANumber();

			decimal initialBalance = userAccount.Balance;
			decimal finalBalance = userAccount.Withdraw(ammountToWithdraw);

			if (initialBalance != finalBalance)
			{
				Console.WriteLine("Withdrawl Successful!");
			}
			else
			{
				Console.WriteLine("Withdrawl Failed!");
			}
			if (initialBalance - ammountToWithdraw != finalBalance)
			{
				Console.WriteLine("Fee Assesed.");
				Console.Beep(800, 100);
				Console.Beep(500, 500);

			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Handles the Transfer transaction
		/// </summary>
		/// <param name="userAccount">The User Account to change</param>
		static void MakeTransfer(BankAccount userAccount, BankCustomer user)
		{
			Console.WriteLine();
			Console.WriteLine("Which account would you like to transfer to?");
			BankAccount chosenAccount = PickAnAccount(user);


			Console.WriteLine($"Transfer to Account: {chosenAccount.AccountNumber}");
			Console.Write("How much would you like to transfer?: ");
			decimal ammountToTransfer = GetANumber();

			decimal initialBalance = userAccount.Balance;
			userAccount.Transfer(chosenAccount, ammountToTransfer);
			decimal finalBalance = userAccount.Balance;

			if (initialBalance != finalBalance)
			{
				Console.WriteLine("Transfer Successful!");
			}
			else
			{
				Console.WriteLine("Transfer Failed!");
			}

			if (initialBalance - ammountToTransfer != finalBalance)
			{
				Console.WriteLine("Fee Assesed.");
				Console.Beep(800, 100);
				Console.Beep(500, 500);
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Choose an account from a list
		/// </summary>
		/// <param name="accounts">A list of accounts to choose from</param>
		/// <returns>The chosen account</returns>
		static BankAccount PickAnAccount(BankCustomer user)
		{
			HashSet<string> validChoices = new HashSet<string>();
			string input = "";

			do
			{
				Console.WriteLine();
				for (int i = 1; i <= user.Accounts.Length; i++)
				{
					validChoices.Add(i.ToString());
					Console.WriteLine($"{i}. Account#: {user.Accounts[i - 1].AccountNumber}");
				}
				Console.WriteLine($"{user.Accounts.Length + 1}. Add an account");
				Console.Write("Pick an option: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
				if (input == (user.Accounts.Length + 1).ToString())
				{
					AddAnAccount(user);
				}
			} while (!validChoices.Contains(input));

			int choice = int.Parse(input);
			return user.Accounts[choice - 1];
		}

		/// <summary>
		/// Prompts the user for a number
		/// </summary>
		/// <returns>A number chosen by the user</returns>
		static decimal GetANumber()
		{
			List<string> messages = new List<string>(){"Please enter a NUMBER: ",
													   "No, a NUMBER: ",
													   "Really, I just need a NUMBER: ",
													   "It's not that hard. You can even use a decimal point: ",
													   "Do you want to be here all day? Just give me a number: ",
													   "Seriously!? You've heard of numbers, right?\nThey look like this: 0439618752\nJust type a few in: ",
													   "Are you completely stupid?! It's just a number: ",
													   "Just so we're clear, this is your last chance. \nI refuse to keep doing this.\nGive me a number: "};
			string input = Console.ReadLine();
			decimal output;
			int counter = 0;
			while (!decimal.TryParse(input, out output))
			{
				Console.Write(messages[counter]);
				input = Console.ReadLine();
				counter++;
				if (counter == messages.Count)
				{
					Console.Clear();
					Console.WriteLine("I told you");
					Environment.Exit(0);
				}
			}
			output = decimal.Parse(input);

			return output;
		}
	}
}

