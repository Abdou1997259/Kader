using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kader_System.Api.Helpers
{
    public static class StringExtenstions
    {

        public static Permission CastToPerssmison(this int str)
        {
            var permission=(Permission)Enum.Parse(typeof(Permission),str.ToString());
            return permission;
        }
        public static List<int> Splitter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<int>();
            }

            var splittedStr = str.Trim().Trim(',').Split(',');
            var result = new List<int>();

            foreach (var s in splittedStr)
            {
                if (int.TryParse(s, out int number))
                {
                    result.Add(number);
                }
                else
                {
                    throw new Exception("this parsing went wrong");
                }
            }
            return result;

        }
        
    }
}
