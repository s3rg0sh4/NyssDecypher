using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NyssDecypher.Tests
{
	[TestClass]
	public class MainWindowTests
	{
		[TestMethod]
		public void Cypher_RusABC_Decrypt()
		{
			string result = MainWindow.Cypher("бщцфаирщри", "скорпион", false);
			string expected = "поздравляю";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Cypher_RusABC_Encrypt()
		{
			string result = MainWindow.Cypher("поздравляю", "скорпион", true);
			string expected = "бщцфаирщри";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Cypher_MixABC_Decrypt()
		{
			string result = MainWindow.Cypher("юё дявсы эасуьпык бгъуёюк р ссцкюфтжтю ъэуаьхтяуч т ься цд у юбюлянюччбюкоыъй а щбшэщнтэтпцчтю ьбхъи бтжшэьюлчч .Net, ющях вняэцшчп ш сьацыувэдд хсооз!", 
				"скорпион", false);
			string expected = "мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Cypher_MixABC_Encrypt()
		{
			string result = MainWindow.Cypher("мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!", 
				"скорпион", true);
			string expected = "юё дявсы эасуьпык бгъуёюк р ссцкюфтжтю ъэуаьхтяуч т ься цд у юбюлянюччбюкоыъй а щбшэщнтэтпцчтю ьбхъи бтжшэьюлчч .Net, ющях вняэцшчп ш сьацыувэдд хсооз!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Cypher_EmptyInput_Encrypt()
		{
			string result = MainWindow.Cypher("", "скорпион", true);
			string expected = "";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Cypher_EmptyKey_Encrypt()
		{
			string result = MainWindow.Cypher("успехов", "", true);
			string expected = "успехов";
			Assert.AreEqual(expected, result);
		}

	}
}