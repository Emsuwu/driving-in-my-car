using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildVersion : MonoBehaviour
{
    public static BuildVersion Current;
    [SerializeField] private TMPro.TMP_Text _versionText;
    public enum BUILDTYPE { Dev, Alpha, Beta, PreRelease, Release }
    private Dictionary<BUILDTYPE, string> _buildTypeDict = new Dictionary<BUILDTYPE, string>();

    [BoxGroup("Scheme")] public SCHEME Scheme;

    [BoxGroup("Build Version")]
    public string Version = "";
    [BoxGroup("Build Version"), Min(0)] public int MinorVersion = 0;
    [BoxGroup("Build Version"), ShowIf("ShowMajorNumber")] public int MajorVersion = 0;

    [BoxGroup("Patch")]
    public bool IncludePatchAfterMinor = false;
    [BoxGroup("Patch"), ShowIf("IncludePatchAfterMinor")] public string PatchNumber = "";

    [BoxGroup("Build Type")] public bool IncludeBuildType = true;
    [BoxGroup("Build Type"), ShowIf("IncludeBuildType")] public BUILDTYPE BuildType = BUILDTYPE.Dev;
    [BoxGroup("Build Type"), ShowIf("IncludeBuildType")] public string BuildTypeDelimiter = "-";


    private bool ShowMajorNumber()
    {
        if (Scheme == SCHEME.YearMajorMinorBuildType) return true;
        if (Scheme == SCHEME.MajorMinorBuildType) return true;
        return false;
    }

    public enum SCHEME
    {
        [InspectorName("Year.Month.Day.Minor-BuildType")]
        YearMonthDayMinorBuildType,
        [InspectorName("Month.Day.Minor-BuildType")]
        MonthDayMinorBuildType,
        [InspectorName("Year.Major.Minor-BuildType")]
        YearMajorMinorBuildType,
        [InspectorName("Major.Minor-BuildType")]
        MajorMinorBuildType
    }

    [Button("Generate version number")]
    private void SetVersionButton()
    {
        SetVersion();
    }

    [Button("Increment Minor Version")]
    private void IncrementMinorVersion()
    {
        MinorVersion += 1;
        SetVersion();
    }
    [Button("Decrement Minor Version")]
    private void DecrementMinorVersion()
    {
        MinorVersion -= 1;
        MinorVersion = Math.Clamp(MinorVersion, 0, int.MaxValue);
        SetVersion();
    }



    private void BuildDict()
    {
        _buildTypeDict.Add(BUILDTYPE.Dev, "dev"); _buildTypeDict.Add(BUILDTYPE.Alpha, "alpha");
        _buildTypeDict.Add(BUILDTYPE.Beta, "beta"); _buildTypeDict.Add(BUILDTYPE.PreRelease, "pre-release");
        _buildTypeDict.Add(BUILDTYPE.Release, "release");
    }

    public void SetVersion()
    {
        if (!_buildTypeDict.ContainsKey(BUILDTYPE.Dev)) BuildDict();

        switch (Scheme)
        {
            case SCHEME.YearMonthDayMinorBuildType:
                Version = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}.{MinorVersion}";
                break;
            case SCHEME.MonthDayMinorBuildType:
                Version = $"{DateTime.Now.Month}.{DateTime.Now.Day}.{MinorVersion}";
                break;
            case SCHEME.YearMajorMinorBuildType:
                Version = $"{DateTime.Now.Year}.{MajorVersion}.{MinorVersion}";
                break;
            case SCHEME.MajorMinorBuildType:
                Version = $"{MajorVersion}.{MinorVersion}";
                break;
        }

        if (IncludePatchAfterMinor)
        {
            Version += $".{PatchNumber}";
        }

        string buildtype;
        _buildTypeDict.TryGetValue(BuildType, out buildtype);
        if (IncludeBuildType) Version += BuildTypeDelimiter + buildtype;
        _versionText.text = Version;

    }
    private void Awake()
    {
        _versionText.text = Version;
    }
}

[ExecuteInEditMode]
public class Autorun : MonoBehaviour
{
    private void Awake()
    {
        BuildVersion.Current.SetVersion();
        Debug.Log("Automatically set build number");
    }
}