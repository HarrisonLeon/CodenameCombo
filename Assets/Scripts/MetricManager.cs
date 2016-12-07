using UnityEngine;
using System.Collections;
using System.IO;

// This class encapsulates all of the metrics that need to be tracked in your game. These may range
// from number of deaths, number of times the player uses a particular mechanic, or the total time
// spent in a level. These are unique to your game and need to be tailored specifically to the data
// you would like to collect. The examples below are just meant to illustrate one way to interact
// with this script and save data.
public class MetricManager : MonoBehaviour
{
	// You'll have more interesting metrics, and they will be better named.
	private int a_111;
	private int a_131;
	private int a_133;
	private int a_333;
	private int a_311;

	private int c_113;
	private int c_331;

	private int s_2;
	private int s_4;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	// Public method to add to Metric 1.
	public void AddCombo(string attackString)
	{
		if (attackString.Equals("AAA")) { a_111++; }
		else if (attackString.Equals("AXA")) { a_131++; }
		else if (attackString.Equals("AXX")) { a_133++; }
		else if (attackString.Equals("XXX")) { a_333++; }
		else if (attackString.Equals("XAA")) { a_311++; }
		else if (attackString.Equals("AXX")) { a_133++; }
		else if (attackString.Equals("AAX")) { c_113++; }
		else if (attackString.Equals("XXA")) { c_331++; }
	}

	// Public method to add to Metric 2.
	public void AddSpecial(int button)
	{
		if (button == 2) { s_2++; }
		if (button == 4) { s_4++; }
	}

	// Converts all metrics tracked in this script to their string representation
	// so they look correct when printing to a file.
	private string ConvertMetricsToStringRepresentation()
	{
		string metrics = "Here are my metrics:\n";
		metrics += "AAA             : " + a_111.ToString() + "\n";
		metrics += "AXA             : " + a_131.ToString() + "\n";
		metrics += "AXX             : " + a_133.ToString() + "\n";
		metrics += "XXX             : " + a_333.ToString() + "\n";
		metrics += "XAA             : " + a_311.ToString() + "\n";
		metrics += "AXX             : " + a_133.ToString() + "\n";
		metrics += "AAX (Uppercut)  : " + c_113.ToString() + "\n";
		metrics += "XXA (Dash)      : " + c_331.ToString() + "\n";
		// metrics += "Special         : " + s_2.ToString() + "\n";
		metrics += "Ultimate        : " + s_4.ToString() + "\n";
		return metrics;
	}

	// Uses the current date/time on this computer to create a uniquely named file,
	// preventing files from colliding and overwriting data.
	private string CreateUniqueFileName()
	{
		string dateTime = System.DateTime.Now.ToString();
		dateTime = dateTime.Replace("/", "_");
		dateTime = dateTime.Replace(":", "_");
		dateTime = dateTime.Replace(" ", "___");
		return "Metrics/CodenameCombo_AttackMetrics" + dateTime + ".txt";
	}

	// Generate the report that will be saved out to a file.
	public void WriteMetricsToFile()
	{
		string totalReport = "Report generated on " + System.DateTime.Now + "\n\n";
		totalReport += "Total Report:\n";
		totalReport += ConvertMetricsToStringRepresentation();
		totalReport = totalReport.Replace("\n", System.Environment.NewLine);
		string reportFile = CreateUniqueFileName();

#if !UNITY_WEBPLAYER
		File.WriteAllText(reportFile, totalReport);
#endif
	}

	// The OnApplicationQuit function is a Unity-Specific function that gets
	// called right before your application actually exits. You can use this
	// to save information for the next time the game starts, or in our case
	// write the metrics out to a file.
	private void OnApplicationQuit()
	{
		if (!Directory.Exists("Metrics"))
		{
			//if it doesn't, create it
			Directory.CreateDirectory("Metrics");
		}
		WriteMetricsToFile();
	}
}
