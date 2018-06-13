using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BankTellerExercise.Classes;

namespace BankTellerExercise
{
	class Program
	{
		static void Main(string[] args)
		{
			// Read Bank from file
			string filePath = Path.Combine(Environment.CurrentDirectory, "bank.txt");
			Bank bank = GetBankFromFile(filePath);

			// Prompt the user for who they are
			BankCustomer user = ChooseUser(bank, true);

			// Prompt the user for which account to enter
			BankAccount working = PickAnAccount(bank, user, true);
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
						MakeTransfer(bank, working);
						break;
					case "4": // Show Balance
						ShowBalance(working);
						break;
					case "5": //Change or add Account
						working = PickAnAccount(bank, user, true);
						break;
					case "6": //Change User info
						ChangeUserInfo(user);
						break;
					case "7": //Change User 
						user = ChooseUser(bank, true);
						working = PickAnAccount(bank, user, true);
						break;
					case "8": //Save Changes
						WriteBankToFile(bank, filePath);
						break;
				}
				input = PromptUserForMenuChoice(user, working);
			}
			WriteBankToFile(bank, filePath);
			Console.Clear();
			Console.WriteLine("Thanks for banking with TE Bank.");
			Console.WriteLine("Have a great day.");
		}

		/// <summary>
		/// Chooses a user
		/// </summary>
		/// <param name="bank">The Bank</param>
		/// <returns>A user</returns>
		static BankCustomer ChooseUser(Bank bank, bool allowAdd)
		{
			HashSet<string> validChoices = new HashSet<string>();
			string input = "";

			do
			{
				Console.Clear();
				Console.WriteLine("Bank Customers");
				for (int i = 1; i <= bank.Customers.Length; i++)
				{
					validChoices.Add(i.ToString());
					Console.WriteLine($"{i}. User: {bank.Customers[i - 1].Name}");
				}
				if (allowAdd)
				{
					Console.WriteLine($"{bank.Customers.Length + 1}. Add a user");
				}
				Console.Write("Pick an option: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
				if (allowAdd && input == (bank.Customers.Length + 1).ToString())
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
		/// Choose an account from a list
		/// </summary>
		/// <param name="accounts">A list of accounts to choose from</param>
		/// <returns>The chosen account</returns>
		static BankAccount PickAnAccount(Bank bank, BankCustomer user, bool allowAdd)
		{
			HashSet<string> validChoices = new HashSet<string>();
			string input = "";

			do
			{
				Console.WriteLine("Accounts:");
				for (int i = 1; i <= user.Accounts.Length; i++)
				{
					validChoices.Add(i.ToString());
					Console.WriteLine($"{i}. Account#: {user.Accounts[i - 1].AccountNumber}");
				}
				if (allowAdd)
				{
					Console.WriteLine($"{user.Accounts.Length + 1}. Add an account");
				}
				Console.Write("Pick an option: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
				if (allowAdd && input == (user.Accounts.Length + 1).ToString())
				{
					AddAnAccount(bank, user);
				}
			} while (!validChoices.Contains(input));

			int choice = int.Parse(input);
			return user.Accounts[choice - 1];
		}

		/// <summary>
		/// Allows the user to create a new bank account
		/// </summary>
		/// <param name="customer">The current user</param>
		static void AddAnAccount(Bank bank, BankCustomer customer)
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
				int accountNumber = 0;
				do
				{
					accountNumber = random.Next(1000000, 10000000);
				} while (bank.AccountNumbers.Contains(type + accountNumber));

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
				if (initialBalance - ammountToWithdraw != finalBalance)
				{
					Console.WriteLine("Fee Assesed.");
					Console.Beep(800, 100);
					Console.Beep(500, 500);
				}
			}
			else
			{
				Console.WriteLine("Withdrawl Failed!");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Handles the Transfer transaction
		/// </summary>
		/// <param name="userAccount">The User Account to change</param>
		static void MakeTransfer(Bank bank, BankAccount userAccount)
		{
			Console.WriteLine();


			Console.WriteLine("Which account would you like to transfer to?");
			BankCustomer chosenUser = ChooseUser(bank, false);
			BankAccount chosenAccount = PickAnAccount(bank, chosenUser, false);
			while (chosenAccount == userAccount)
			{
				Console.WriteLine("You cannot transfer to your own account.");
				System.Threading.Thread.Sleep(1000);
				chosenUser = ChooseUser(bank, false);
				chosenAccount = PickAnAccount(bank, chosenUser, false);
				Console.Clear();
			}
			Console.Clear();
			Console.WriteLine($"Transfer to {chosenUser.Name}'s Account: {chosenAccount.AccountNumber}");
			Console.Write("How much would you like to transfer?: ");
			decimal ammountToTransfer = GetANumber();

			decimal initialBalance = userAccount.Balance;
			userAccount.Transfer(chosenAccount, ammountToTransfer);
			decimal finalBalance = userAccount.Balance;

			if (initialBalance != finalBalance)
			{
				Console.WriteLine("Transfer Successful!");
				if (initialBalance - ammountToTransfer != finalBalance)
				{
					Console.WriteLine("Fee Assesed.");
					Console.Beep(800, 100);
					Console.Beep(500, 500);
				}
			}
			else
			{
				Console.WriteLine("Transfer Failed!");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
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
		/// Reads the bank from a text file
		/// </summary>
		/// <param name="filepath">The file path to read from</param>
		/// <returns>The bank</returns>
		static Bank GetBankFromFile(string filePath)
		{
			Bank bank = new Bank();
			try
			{
				using (StreamReader sr = new StreamReader(filePath))
				{
					int numberOfCustomers = int.Parse(sr.ReadLine().Split('|')[1]);

					for (int i = 0; i < numberOfCustomers; i++)
					{
						string[] line = sr.ReadLine().Split('|');
						BankCustomer bankCustomer = new BankCustomer();
						bankCustomer.Name = line[1];
						bankCustomer.Address = line[2];
						bankCustomer.PhoneNumber = line[3];
						int numberOfAccounts = int.Parse(line[4]);
						for (int j = 0; j < numberOfAccounts; j++)
						{
							line = sr.ReadLine().Split('|');
							if (line[0][0] == 'C')
							{
								CheckingAccount checkingAccount = new CheckingAccount();
								checkingAccount.AccountNumber = line[0];
								checkingAccount.Deposit(decimal.Parse(line[1]));
								bankCustomer.AddAccount(checkingAccount);
							}
							else
							{
								SavingsAccount savingsAccount = new SavingsAccount();
								savingsAccount.AccountNumber = line[0];
								savingsAccount.Deposit(decimal.Parse(line[1]));
								bankCustomer.AddAccount(savingsAccount);
							}
						}
						bank.AddCustomer(bankCustomer);
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return bank;
		}

		/// <summary>
		/// Writes the bank back out to a text file to save it
		/// </summary>
		/// <param name="bank">The bank to write out</param>
		/// <param name="filePath">The file path to write to</param>
		static void WriteBankToFile(Bank bank, string filePath)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(filePath, false))
				{
					int numberOfCustomers = bank.Customers.Length;
					sw.WriteLine($"Bank|{numberOfCustomers}");
					for (int i = 0; i < numberOfCustomers; i++)
					{
						BankCustomer customer = bank.Customers[i];
						sw.WriteLine($"BankCustomer|{customer.Name}|{customer.Address}|{customer.PhoneNumber}|{customer.Accounts.Length}");
						for (int j = 0; j < customer.Accounts.Length; j++)
						{
							sw.WriteLine($"{customer.Accounts[j].AccountNumber}|{customer.Accounts[j].Balance}");
						}
					}
				}
			}

			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.Clear();
			Console.WriteLine("Changes Saved");
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Prompts the user with a Menu until the enter a valid option
		/// </summary>
		/// <returns>The users choice</returns>
		static string PromptUserForMenuChoice(BankCustomer user, BankAccount account)
		{
			string[] validChoices = { "1", "2", "3", "4", "5", "6", "7", "8", "Q" };
			string input = "";

			do
			{
				Console.WriteLine($"User: {user.Name}");
				Console.WriteLine($"Account#: {account.AccountNumber}");
				if (user.IsVIP)
				{
					Console.WriteLine("**********VIP***********");
				}
				Console.WriteLine("       TE Bank");
				Console.WriteLine("------------------------");
				Console.WriteLine("      MAIN MENU");
				Console.WriteLine("1. Make a Deposit");
				Console.WriteLine("2. Make a Withdrawl");
				Console.WriteLine("3. Make a Transfer");
				Console.WriteLine("4. Show Balance");
				Console.WriteLine("5. Change or Add Account");
				Console.WriteLine("6. Change User Info");
				Console.WriteLine("7. Change User");
				Console.WriteLine("8. Save Changes");
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
		/// Prompts the user for a number
		/// </summary>
		/// <returns>A number chosen by the user</returns>
		static decimal GetANumber()
		{
			string input = Console.ReadLine();
			decimal output;

			while (!decimal.TryParse(input, out output))
			{
				Console.Write("Please enter a NUMBER: ");
				input = Console.ReadLine();
			}
			output = decimal.Parse(input);

			return output;
		}
	}
}

