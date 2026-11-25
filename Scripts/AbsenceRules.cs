using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace LOK2239
{
    /// <summary>
    /// Parsing of Lectio absence cells and the thresholds that decide when a student is in violation.
    /// </summary>
    public static class AbsenceRules
    {
        /// <summary>Students with this many modules or fewer must have at least SmallModulePercent to count.</summary>
        public const float SmallModuleLimit = 5;
        public const float SmallModulePercent = 80f;
        /// <summary>New student (no file): Opgjort percent must be strictly above this.</summary>
        public const float NewStudentPercent = 25f;
        /// <summary>Student with history: Periode percent since the latest letter must be strictly above this.</summary>
        public const float RepeatPercent = 10f;

        /// <summary>Parses a Lectio percent cell such as "20,0%" or "7.5 %".</summary>
        public static float ParsePercent(string text)
        {
            var cleaned = (text ?? "").Replace("%", "").Replace(",", ".").Trim();
            if (!float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out var percent) || percent < 0f || percent > 100f)
                throw new FormatException($"Invalid absence percent: '{text}'");
            return percent;
        }

        /// <summary>Parses a Lectio module cell in the format "{x}/{moduleCount}" and returns moduleCount.</summary>
        public static float ParseModuleCount(string text)
        {
            var match = Regex.Match(text ?? "", @"^\s*[\d.,]+\s*/\s*(\d+)(?:[.,]0+)?\s*$");
            if (!match.Success)
                throw new FormatException($"Invalid module count, expected '{{x}}/{{moduleCount}}': '{text}'");
            return float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        }

        /// <summary>Special rule: with SmallModuleLimit modules or fewer the percent must be at least SmallModulePercent.</summary>
        public static bool PassesSmallModuleRule(float percent, float moduleCount)
            => moduleCount > SmallModuleLimit || percent >= SmallModulePercent;

        /// <summary>No prior history: Opgjort percent above 25 (and the special rule).</summary>
        public static bool IsNewStudentViolation(float percent, float moduleCount)
            => PassesSmallModuleRule(percent, moduleCount) && percent > NewStudentPercent;

        /// <summary>Prior history: Periode percent since latest letter above 10 (and the special rule).</summary>
        public static bool IsRepeatViolation(float percent, float moduleCount)
            => PassesSmallModuleRule(percent, moduleCount) && percent > RepeatPercent;

        /// <summary>Timestamp prefix used in log lines and Messages, e.g. "[2026-11-20 14:05]".</summary>
        public static string Timestamp() => "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) + "]";

        /// <summary>Date in the format Lectio's Periode fields display, e.g. "21/10-2026".</summary>
        public static string FormatLectioDate(DateOnly date) => date.ToString("d/M-yyyy", CultureInfo.InvariantCulture);

        /// <summary>Student JSON without null/default values, so a new file holds only the fields that are set.</summary>
        public static string ToJson(ClassStudent student)
            => JsonConvert.SerializeObject(student, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            });

        public static ClassStudent FromJson(string json, string expectedId)
        {
            var student = JsonConvert.DeserializeObject<ClassStudent>(json);
            if (student == null || student.Id != expectedId)
                throw new InvalidOperationException($"Saved student JSON does not match student ID {expectedId}");
            if (!Enum.IsDefined(typeof(StudentState), student.State))
                throw new InvalidOperationException("Saved student JSON contains an unknown state.");
            return student;
        }
    }
}
