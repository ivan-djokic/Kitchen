// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Linq;
using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

namespace Kitchen.Management.Managers
{
	public class RecordsManager : UnityObject
	{
		private const int FIRST = 0;

		protected override void OnEnable()
		{
			SoundManager.Instance.Play(Sounds.Records, inPause: true);
			var users = FileHelper.GetFiles(typeof(Options).Name).Select(name => User.Load(name)).OrderByDescending(user => user.Record).ToArray();
			var records = transform.GetChild(FIRST);

			for (var i = 0; i < users.Length; i++)
			{
				if (i >= records.childCount)
				{
					return;
				}

				var item = GetTextualItem(records.GetChild(i));

				if (item == null)
				{
					continue;
				}

				item.gameObject.SetActive(true);
				item.text = $"{i + 1}. {users[i].Name}";
				GetTextualItem(item.transform.GetChild(FIRST)).text = users[i].VisualRecord;
			}
		}

		private static TMP_Text GetTextualItem(Transform transform)
		{
			if (transform.TryGetComponent<TextMeshProUGUI>(out var text))
			{
				return text;
			}

			if (transform.TryGetComponent<TMP_SubMeshUI>(out var subText))
			{
				return subText.textComponent;
			}

			return null;
		}
	}
}
