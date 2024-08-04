

 namespace Kader_System.Domain.Extensions
{
    public static class StringExtenstions
    {

       
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
        public static string Concater(this List<int> intergers ) {
        
        
            if (intergers is null)
            {
                return "";
            }
            return string.Join(',', intergers);

        }
        
    }
}
