using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace NyssDecypher
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public static string Cypher(string input, string key, bool encypher)
		{
			List<char> abc = new List<char>("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
			int[] steps = new int[key.Length];
			for (int i = 0; i < key.Length; i++)
				steps[i] = abc.FindIndex(s => s == key[i]);


			int offset;
			int j = 0;
			string encrypted = "";
			for (int i = 0; i < input.Length; i++)
			{
				offset = encypher ? steps[j] : (steps[j] * -1);
				if (abc.Contains(Char.ToLower(input[i])))
				{
					bool isUpper = Char.GetUnicodeCategory(input[i]) == UnicodeCategory.UppercaseLetter ? true : false;

					var abcI = abc.IndexOf(Char.ToLower(input[i]));
					if (abcI + offset < 0)
						encrypted += isUpper ? Char.ToUpper(abc[abcI + offset + 33]) : abc[abcI + offset + 33];
					else if (abcI + offset > 32)
						encrypted += isUpper ? Char.ToUpper(abc[abcI + offset - 33]) : abc[abcI + offset - 33];
					else
						encrypted += isUpper ? Char.ToUpper(abc[abcI + offset]) : abc[abcI + offset];

					j++;
				}
				else { encrypted += input[i]; }

				if (j >= steps.Length) { j = 0; }

			}

			return encrypted;
		}

		public void Encrypt(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(CypherKeyTB.Text))
			{
				MessageBox.Show("А ключ то куда пропал?");
				return;
			}
			OutputTB.Text = Cypher(InputTB.Text, CypherKeyTB.Text, true); 
		}
		public void Decrypt(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(CypherKeyTB.Text))
			{
				MessageBox.Show("А ключ то куда пропал?");
				return;
			}
			OutputTB.Text = Cypher(InputTB.Text, CypherKeyTB.Text, false); 
		}

		private void Upload(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				RestoreDirectory = true,
				InitialDirectory = "c:\\",
				FilterIndex = 1
			};
			
			if (openFileDialog.ShowDialog() == true)
			{
				using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), Encoding.GetEncoding(1251)))
				{
					InputTB.Text = reader.ReadToEnd();
				}
			}
		}

		private void Download(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(OutputTB.Text))
			{
				MessageBox.Show("А нечего сохранять, ага да!((");
				return;
			}

			SaveFileDialog save = new SaveFileDialog
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 1,
				InitialDirectory = "c:\\",
				RestoreDirectory = true
			};

			if (save.ShowDialog() == true)
			{
				using (StreamWriter writer = new StreamWriter(save.OpenFile()))
				{ 
					writer.Write(OutputTB.Text); 
				}
			}
		}
	}
}
