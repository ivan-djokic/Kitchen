// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Runtime.Serialization;
using Kitchen.Utils;
using Kitchen.Utils.Levels;

namespace Kitchen.Management.Administration
{
	[DataContract]
	public class User
	{
		private const string ICON = "<sprite=6> ";
		private const string RECORD_ICON = " <sprite=7>";

		[DataMember]
		private string m_name;
		[DataMember]
		private int m_record;

		private static Lazy<User> s_instance;

		public static User Instance
		{
			get => s_instance.Value;
		}

		[DataMember]
		public Level Level { get; set; } = new();

		public string Name
		{
			get => ConstructName(m_name);
		}

		[DataMember]
		public int Record
		{
			get => m_record;
			
			set
			{
				if (m_record > value)
				{
					return;
				}

				m_record = value;
				RecordChanged?.Invoke();
			}
		}

		public string VisualRecord
		{
			get => $"{Record}{RECORD_ICON}";
		}

		private Action RecordChanged { get; set; }

		public static string ConstructName(string name)
		{
			return ICON + name;
		}

		public static void Instatiate(string name)
		{
			name = name.Replace(ICON, string.Empty);
			s_instance = new(Load(name));

			Instance.Level.Changed += Instance.Save;
			Instance.m_name = name;
			Instance.RecordChanged += Instance.SaveRecord;
		}

		public static User Load(string name)
		{
			return FileHelper.Load<User>(name);
		}

		public void Reload()
		{
			Instatiate(m_name);
		}

		public void Reset()
		{
			Level = new();
			Level.Changed += Save;

			Save();
		}

		public void Save()
		{
			FileHelper.Save(m_name, this);
		}

		private void SaveRecord()
		{
			var user = Load(m_name);
			user.m_record = m_record;
			
			user.Save();
		}
	}
}
