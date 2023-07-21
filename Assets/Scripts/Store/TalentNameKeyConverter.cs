using System.Collections.Generic;
using System.Text;
using Skills;

namespace Store
{
    public static class TalentNameKeyConverter
    {

        public static HashSet<TalentNameKey> GetKeysFromString(string keys)
        {
            var result = new HashSet<TalentNameKey>();

            foreach(var key in keys.Split(','))
            {
                var talentNameKey = (TalentNameKey) int.Parse(key);
                result.Add(talentNameKey);
            }

            return result;
        }

        public static string GetKeysInString(IEnumerable<TalentNameKey> talentNameKeys)
        {
            var result = new StringBuilder();
            var isFirstElement = true;
            foreach(var key in talentNameKeys)
            {
                if (!isFirstElement) result.Append(",");
                var keyValueInString = ((int)key).ToString();
                result.Append(keyValueInString);
                isFirstElement = false;
            }

            return result.ToString();
        }
    }
}