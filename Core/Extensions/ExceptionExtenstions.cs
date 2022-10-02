using System.Collections;
using System.Reflection;
using System.Text;

namespace Shirehorse.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToHierarchicalString(this Exception outerException)
        {
            StringBuilder result = new ();

            void AppendLine(int indent, string line)
            {
                for (int i = 0; i < indent; i++) 
                    result.Append('\t');

                result.AppendLine(line);
            }

            void GetExceptionInfo(int indent, Exception ex)
            {
                try
                {
                    foreach (PropertyInfo propertyInfo in ex.GetType().GetProperties())
                    {
                        object? value = propertyInfo.GetValue(ex, null);

                        if (value != null)
                        {
                            AppendLine(indent, $"{propertyInfo.Name} :");

                            switch (propertyInfo.Name)
                            {
                                case "InnerException":
                                    if (value is Exception inner)
                                    {
                                        GetExceptionInfo(indent + 2, inner);
                                    }

                                    break;

                                case "Data":
                                    foreach (DictionaryEntry kv in ex.Data)
                                    {
                                        AppendLine(indent + 1, $"{kv.Key} | {kv.Value}");
                                    }

                                    break;

                                case "StackTrace":
                                    foreach (string line in value.ToString().Split('\n'))
                                    {
                                        AppendLine(indent + 1, line.Replace("\r", "")[6..]);
                                    }
                                    break;

                                default:
                                    AppendLine(indent + 1, value.ToString());
                                    break;
                            }
                        }
                    }
                }
                catch (Exception reflectionEx)
                {
                    // should not reach here
                    result.AppendLine($"Failed to get exception properties with error: {reflectionEx.Message}");
                }

            }

            AppendLine(0, $"{outerException.GetType()}");

            GetExceptionInfo(1, outerException);

            return result.ToString();
        }



    }
}
