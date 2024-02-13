// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace Kitchen.Utils
{
	public static class FileHelper
	{
		private const string FILE_EXTENSION = ".xml";
		private const string GAME_NAME = "Kitchen";

		private static readonly string s_defaultFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), GAME_NAME);

		public static IEnumerable<string> GetFiles(params string[] excludes)
		{
			var files = new DirectoryInfo(s_defaultFilePath).GetFiles().OrderByDescending(f => f.LastWriteTime);
			return files.Select(file => Path.GetFileNameWithoutExtension(file.Name)).Where(file => !excludes.Contains(file));
		}

		public static T Load<T>(string fileName) where T : new()
		{
			try
			{
				using var stream = new FileStream(CreatePath(fileName), FileMode.Open, FileAccess.Read, FileShare.None);
				using var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());

				return (T) new DataContractSerializer(typeof(T)).ReadObject(reader, true);
			}
			catch
			{
				return new T();
			}
		}

		public static bool Save<T>(string fileName, T contract)
		{
			try
			{
				fileName = CreatePath(fileName);
				new FileInfo(fileName).Directory.Create();

				using var writer = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
				new DataContractSerializer(typeof(T)).WriteObject(writer, contract);

				return true;
			}
			catch
			{
				return false;
			}
		}

		private static string CreatePath(string fileName)
		{
			return Path.Combine(s_defaultFilePath, fileName + FILE_EXTENSION);
		}
	}
}
