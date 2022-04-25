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

		//Вообще, планировал сделать метод Cypher() приватным, но раз нужны тесты пришлось сделать публичным
		//есть мысли вынести в другой класс, хз как
		public static string Cypher(string input, string key, bool encypher)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			if (string.IsNullOrEmpty(key))
				return input;

			//Знаки препинания, и прочие элементы не относящиеся к алфавиту сообщения не изменяются.
			//В сообщении используется десятичная система счисления и русский алфавит.
			List<char> abc = new List<char>("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
			int[] steps = new int[key.Length];
			for (int i = 0; i < key.Length; i++)
				steps[i] = abc.FindIndex(s => s == key[i]);


			int offset;
			int stepsCounter = 0;
			string encrypted = "";
			for (int i = 0; i < input.Length; i++) //допиленный цезарь
			{
				offset = encypher ? steps[stepsCounter] : (steps[stepsCounter] * -1);
				if (abc.Contains(char.ToLower(input[i])))
				{
					bool isUpper = char.GetUnicodeCategory(input[i]) == UnicodeCategory.UppercaseLetter;

					var abcI = abc.IndexOf(char.ToLower(input[i]));
					if (abcI + offset < 0)
						encrypted += isUpper ? char.ToUpper(abc[abcI + offset + 33]) : abc[abcI + offset + 33];
					else if (abcI + offset > 32)
						encrypted += isUpper ? char.ToUpper(abc[abcI + offset - 33]) : abc[abcI + offset - 33];
					else
						encrypted += isUpper ? char.ToUpper(abc[abcI + offset]) : abc[abcI + offset];

					stepsCounter++;
				}
				else { encrypted += input[i]; }

				if (stepsCounter >= steps.Length) { stepsCounter = 0; }
			}
			return encrypted;
		}
		public void Encrypt(object sender, RoutedEventArgs e) => OutputTB.Text = Cypher(InputTB.Text, CypherKeyTB.Text, true);
		public void Decrypt(object sender, RoutedEventArgs e) => OutputTB.Text = Cypher(InputTB.Text, CypherKeyTB.Text, false);

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
			//Подумал, а в чем смысл не сохранять пустой файл
			//if (string.IsNullOrEmpty(OutputTB.Text))
			//{
			//	MessageBox.Show("А нечего сохранять, ага да!((");
			//	return;
			//}

			SaveFileDialog save = new SaveFileDialog
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 1,
				InitialDirectory = "c:\\",
				RestoreDirectory = true,
				FileName = "Output"
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
