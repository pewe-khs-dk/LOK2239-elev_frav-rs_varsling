using System.Security;

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
        public string PersistenceFolder;
    }
    
    public static class Config
    {   
        public static Values Values { get; set; }

        public static void LoadFromJson(string configJson)
        {
            Values = Newtonsoft.Json.JsonConvert.DeserializeObject<Values>(configJson) ?? throw new System.Exception($"Could not parse config JSON: {configJson}");

            if (!Values.DryRun.HasValue) throw new System.Exception($"Missing value {nameof(Values.DryRun)} from config JSON");
            if (string.IsNullOrEmpty(Values.InstitutionId)) throw new System.Exception($"Missing value {nameof(Values.InstitutionId)} from config JSON");
            if (string.IsNullOrEmpty(Values.EmailAddressSME)) throw new System.Exception($"Missing value {nameof(Values.EmailAddressSME)} from config JSON");
            if (string.IsNullOrEmpty(Values.EmailAddressOnError)) throw new System.Exception($"Missing value {nameof(Values.EmailAddressOnError)} from config JSON");
            if (string.IsNullOrEmpty(Values.PersistenceFolder)) throw new System.Exception($"Missing value {nameof(Values.PersistenceFolder)} from config JSON");
        }
    }
}
