

using Kader_System.Domain.DTOs.Response;

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
        public static string Concater(this List<int> intergers)
        {


            if (intergers is null)
            {
                return "";
            }
            return string.Join(',', intergers);

        }
        public static string NulalbleConcater(this List<int?> intergers)
        {


            if (intergers is null)
            {
                return "";
            }
            return string.Join(',', intergers);

        }
        public static Dictionary<string, bool> CreateNewPermission(this string res, string mainActions, string userActions, string permNames)
        {
            // Split the input strings into arrays
            var mainActionsArr = mainActions.Split(',').ToArray(); // [1,2,3,4]
            var userActionsArr = userActions.Split(',').ToArray(); // [2,3]
            var permNamesArr = permNames.Split(',').ToArray(); // [view,add,edit,delete]

            // Create the action-to-permission map using LINQ
            var actionToPermNameMap = mainActionsArr
                .Select((actionId, index) => new { actionId, permName = permNamesArr[index] })
                .ToDictionary(x => x.actionId, x => x.permName);

            // Create the result dictionary using LINQ
            var result = permNamesArr
                .ToDictionary(
                    permName => permName,
                    permName => actionToPermNameMap
                        .FirstOrDefault(x => x.Value == permName).Key != null
                        && userActionsArr.Contains(actionToPermNameMap.FirstOrDefault(x => x.Value == permName).Key)
                );

            return result;

        }


    }
}
