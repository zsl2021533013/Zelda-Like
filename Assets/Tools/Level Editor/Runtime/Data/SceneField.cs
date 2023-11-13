using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Level_Editor.Runtime
{
	[System.Serializable]
	public class SceneField
	{
		[SerializeField]
		private Object sceneAsset;

		[SerializeField]
		private string sceneName = "";

		[HideInInspector, SerializeField] 
		private string scenePath = "";
	
		public string ScenePath => scenePath;
		public string SceneName => sceneName;
		
		public SceneField(Object sceneAsset)
		{
			this.sceneAsset = sceneAsset;
			this.sceneName = sceneAsset.name;
			this.scenePath = AssetDatabase.GetAssetPath(sceneAsset);
		}

		// makes it work with the existing Unity methods (LoadLevel/LoadScene)
		public static implicit operator string( SceneField sceneField )
		{
			return sceneField.SceneName;
		}
	}
}

