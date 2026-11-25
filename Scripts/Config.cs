using System;
using System.IO;

namespace LOK2239
{
    /// <summary>
    /// Config values read from assets. Lectio credentials are read from a Credential asset.
    /// </summary>
    public class Values
    {
        public bool? DryRun = true;
        public string InstitutionId;
        public string EmailAddressSME;
        public string EmailAddressOnError;
        public string EmailAddressOnDryRun;
        public string PersistenceFolder;
    }
    
    public static class Config
    {   
        public static Values Values { get; set; }

        public static void LoadFromJson(string configJson)
        {
            Values = Newtonsoft.Json.JsonConvert.DeserializeObject<Values>(configJson) ?? throw new System.Exception($"Could not parse config JSON: {configJson}");

            // There is no simple way to check if we run locally from Studio, so create this path on C:\ and that will automatically be used.
            // If we don't create it we will get a path not found error, since the Orchestrator asset points to a UNC path that is known by
            // our process runners but not by our own PC, where we run studio.
            string localPath = @"C:\RPA\2239";
            bool isRunningLocally = Directory.Exists(localPath);
            if (isRunningLocally)
            {
                Values.PersistenceFolder = localPath;
            }

            // Add current school year (ex 2026-2027) to persistence path.
            DateTime midJuly = new DateTime(DateTime.Now.Year, 7, 15);
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            string schoolYear = DateTime.UtcNow > midJuly ? $"{today.Year}-{today.Year + 1}" : $"{today.Year - 1}-{today.Year}";
            Values.PersistenceFolder = Path.Combine(Values.PersistenceFolder, schoolYear);
            
            if (!Values.DryRun.HasValue) throw new System.Exception($"Missing value {nameof(Values.DryRun)} from config JSON");
            if (string.IsNullOrEmpty(Values.InstitutionId)) throw new System.Exception($"Missing value {nameof(Values.InstitutionId)} from config JSON");
            if (string.IsNullOrEmpty(Values.EmailAddressSME)) throw new System.Exception($"Missing value {nameof(Values.EmailAddressSME)} from config JSON");
            if (string.IsNullOrEmpty(Values.EmailAddressOnError)) throw new System.Exception($"Missing value {nameof(Values.EmailAddressOnError)} from config JSON");
            if (string.IsNullOrEmpty(Values.EmailAddressOnDryRun)) throw new System.Exception($"Missing value {nameof(Values.EmailAddressOnDryRun)} from config JSON");
            if (string.IsNullOrEmpty(Values.PersistenceFolder)) throw new System.Exception($"Missing value {nameof(Values.PersistenceFolder)} from config JSON");
        }
    }
}
