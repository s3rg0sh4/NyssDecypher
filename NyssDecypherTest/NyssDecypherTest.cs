using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NyssDecypher;

namespace NyssDecypherTest
{
	[TestClass]
	public class NyssDecypherTest
	{
		[TestMethod]
		public void CypherRusDecodeTest()
		{
			string result = MainWindow.Cypher("бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!", "скорпион", false);
			string expected = "поздравляю, ты получил исходный текст!!!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CypherRusEncodeTest()
		{
			string result = MainWindow.Cypher("поздравляю, ты получил исходный текст!!!", "скорпион", true);
			string expected = "бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CypherMixDecodeTest()
		{
			string result = MainWindow.Cypher("юё дявсы эасуьпык бгъуёюк р ссцкюфтжтю ъэуаьхтяуч т ься цд у юбюлянюччбюкоыъй а щбшэщнтэтпцчтю ьбхъи бтжшэьюлчч .Net, ющях вняэцшчп ш сьацыувэдд хсооз!", 
				"скорпион", false);
			string expected = "мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CypherMixEncodeTest()
		{
			string result = MainWindow.Cypher("мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!", 
				"скорпион", true);
			string expected = "юё дявсы эасуьпык бгъуёюк р ссцкюфтжтю ъэуаьхтяуч т ься цд у юбюлянюччбюкоыъй а щбшэщнтэтпцчтю ьбхъи бтжшэьюлчч .Net, ющях вняэцшчп ш сьацыувэдд хсооз!";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CypherEmptyInputTest()
		{
			string result = MainWindow.Cypher("", "скорпион", true);
			string expected = "";
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CypherEmptyKeyTest()
		{
			string result = MainWindow.Cypher("мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!", 
				"", true);
			string expected = "мы хотим пожелать успехов в дальнейшем погружении в мир ит и программирования с использованием стека технологий .Net, море терпения и интересных задач!";
			Assert.AreEqual(expected, result);
		}

	}
}