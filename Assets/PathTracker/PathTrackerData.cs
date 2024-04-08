using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System;

public class TrackData
{
	public Vector3[] positions;

	public TrackData(List<Vector3> values)
	{
		positions = values.ToArray();
	}
}

public class PathTrackerData : MonoBehaviour
{
	private static int index = 0;
	private static DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
	private static FileInfo[] files = dir.GetFiles("*.*");

	private static GameObject lineObj;

	private static void Reload()
	{
		dir = new DirectoryInfo(Application.persistentDataPath);
		files = dir.GetFiles("*.*");
	}

	[MenuItem("Tools/PathTracker/Show Files Names", false, 1)]
	static void ShowFilesNames()
	{
		Reload();
		string names = "Files Names (" + files.Length + ") :\n";
		foreach (FileInfo f in files)
		{
			names += f.ToString();
			names += '\n';
		}
		Debug.Log(names);
	}

	[MenuItem("Tools/PathTracker/Hide Lines", false, 2)]
	static void Hide()
	{
		if (lineObj != null)
		{
			DestroyImmediate(lineObj);
		}
	}

	[MenuItem("Tools/PathTracker/First", false, 20)]
	static void First()
	{
		Reload();
		index = 0;
		Debug.Log("File (" + index + ") : " + files[index]);
		GenerateLine(LoadData(files[index].ToString()).positions);
	}

	[MenuItem("Tools/PathTracker/Last", false, 21)]
	static void Last()
	{
		Reload();
		index = files.Length - 1;
		Debug.Log("File (" + index + ") : " + files[index]);
		GenerateLine(LoadData(files[index].ToString()).positions);
	}

	[MenuItem("Tools/PathTracker/Previous", false, 22)]
	static void Previous()
	{
		Reload();
		if (index == 0)
			index = files.Length - 1;
		else
			index--;
		Debug.Log("File (" + index + ") : " + files[index]);
		GenerateLine(LoadData(files[index].ToString()).positions);
	}

	[MenuItem("Tools/PathTracker/Next", false, 23)]
	static void Next()
	{
		Reload();
		if (index == files.Length - 1)
			index = 0;
		else
			index++;
		Debug.Log("File (" + index + ") : " + files[index]);
		GenerateLine(LoadData(files[index].ToString()).positions);
	}

	public static void SaveData(List<Vector3> positions, string fileName)
	{
		string savePath = Application.persistentDataPath + "/" + fileName + ".json";
		Debug.Log("Data saved here : " + savePath);
		string saveData = JsonUtility.ToJson(new TrackData(positions), true);
		Debug.Log("Positions tracked : " + positions.Count);
		File.WriteAllText(savePath, saveData);
		Reload();
	}

	static TrackData LoadData(string path)
	{
		Reload();
		return (JsonUtility.FromJson<TrackData>(File.ReadAllText(path)));
	}

	static void GenerateLine(Vector3[] positions)
	{
		if (lineObj == null)
		{
			lineObj = Instantiate(Resources.Load<GameObject>("PathTracker/PathLine"), Vector3.zero, Quaternion.identity);
		}
		lineObj.GetComponent<PathLine>().DrawLine(positions);
	}
}
