using System.Data;
using System.Text;

namespace Shirehorse.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary> Returns a value indicating whether a specified substring occurs within this string. </summary>
        ///
        /// <param name="value"> The string to seek. </param>
        /// <param name="comp"> One of the enumeration values that determines how this string and value are compared. </param>
        ///     
        /// <returns> true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false. </returns>
        ///     
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool Contains(this string str, string value, StringComparison comp)
        {
            if (value == null)
                throw new ArgumentNullException("substring", "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison", "comp");

            return str.IndexOf(value, comp) >= 0;
        }

        /// <summary> Returns a value indicating whether any specified substring in the collection occurs within this string. </summary>
        ///
        /// <param name="substrings"> A collection of substrings to seek. </param>
        ///     
        /// <returns> true if the any specified substring in the collection occurs within this string, or if value is the empty string (""); otherwise, false. </returns>
        ///     
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Contains(this string str, IEnumerable<string> substrings)
        {
            if (substrings == null) throw new ArgumentNullException("substring", "substrings cannot be null.");

            foreach (string substring in substrings) if (str.Contains(substring)) return true;

            return false;
        }

        public static string Replace(this string s, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (s == null)
                return null;

            if (String.IsNullOrEmpty(oldValue))
                return s;

            StringBuilder result = new StringBuilder(Math.Min(4096, s.Length));
            int pos = 0;

            while (true)
            {
                int i = s.IndexOf(oldValue, pos, comparisonType);
                if (i < 0)
                    break;

                result.Append(s, pos, i - pos);
                result.Append(newValue);

                pos = i + oldValue.Length;
            }
            result.Append(s, pos, s.Length - pos);

            return result.ToString();
        }

        /// <summary> If digits already exist, increment the digits until the string is unique.
        /// If not digits exist, append a digit to the string</summary>
        ///
        /// <param name="existingNames"> A collection of substrings to seek. </param>
        ///     
        /// <returns> Returns a new unique string. </returns>
        ///     
        /// <exception cref="ArgumentNullException"></exception>
        public static string Rename(this string str, IEnumerable<string> existingNames)
        {   // scan for existing digits, increment digits, confirm key doesn't exist

            if (str is null) throw new ArgumentNullException("str", "str cannot be null.");

            if (existingNames is null) throw new ArgumentNullException("existingNames", "existingNames cannot be null.");

            if (!existingNames.Contains(str)) return str;

            var chrs = str.ToCharArray();
            int last_digit = -1;
            int idx;
            for (idx = chrs.Length - 1; idx >= 0; idx--)
            {
                if (char.IsDigit(chrs[idx]))
                {
                    if (last_digit == -1) last_digit = idx;
                }
                else
                {
                    if (last_digit != -1) break;
                }
            }
            idx++;

            if (last_digit == -1)
            {
                str += '1';
                last_digit = chrs.Length;
                idx = last_digit;
            }

            var leading_string = str.Substring(0, idx);
            var trailing_string = (str.Length - 1 == last_digit)
                ? string.Empty
                : str.Substring(last_digit + 1, str.Length - last_digit - 1);

            int length = last_digit - idx + 1;
            int.TryParse(str.Substring(idx, length), out int number);

            string new_string = string.Empty;
            bool already_exists = true;
            while (already_exists)
            {
                new_string = leading_string + (number++).ToString($"D{length}") + trailing_string;
                already_exists = existingNames.Contains(new_string);
            }

            return new_string;
        }

        /// <summary> 
        /// Returns a unique file path.
        /// If digits already exist anywhere in the file name, increment the digits until the path is unique.
        /// If no digits exist, append a digit to the string
        /// </summary>
        ///
        /// <returns> Returns a unique file path. </returns>
        ///     
        /// <exception cref="ArgumentNullException"></exception>
        public static string RenameFile(this string str)
        {
            if (str is null) throw new ArgumentNullException("str", "str cannot be null.");

            string? directoryPath = Path.GetDirectoryName(str);

            if (Directory.Exists(directoryPath))
            {
                string currentFileName = Path.GetFileNameWithoutExtension(str);

                var existingFiles = Directory.GetFileSystemEntries(directoryPath, $"*").Select(path => Path.GetFileNameWithoutExtension(path));
                var ext = Path.GetExtension(str);
                ext = ext is null ? "" : ext;

                string newName = Rename(currentFileName, existingFiles);

                return Path.Combine(directoryPath, $"{newName}{ext}");
            }
            else throw new DirectoryNotFoundException($"Directory does not exist: {str}");
        }



        /// <param name="length"> The string to seek. </param>
        /// <returns> Returns a string that wraps to a new line '\n' if a line exceeds <paramref name="length"/>  </returns>
        public static string WrapNewLine(this string str, int length)
        {
            var result = new StringBuilder();
            var line = new StringBuilder();
            var paragraphs = str.Split('\n');

            foreach (string parag in paragraphs)
            {
                var parts = parag.Split(' ');
                line.Clear();

                foreach (string word in parts)
                {
                    if (line.Length + word.Length >= length)
                    {
                        result.AppendLine(line.ToString());
                        line.Clear();
                    }
                    line.Append(word + ' ');
                }
                result.AppendLine(line.ToString());
            }
            return result.ToString();
        }

        /// <returns> Returns a string with a character 'space' before every upper case character </returns>
        public static string CamelCaseAddSpaces(this string str)
        {
            var result = new StringBuilder();

            foreach (char c in str)
            {
                if (char.IsUpper(c)) result.Append(" ");

                result.Append(c);
            }

            return result.ToString().Trim();
        }

        /// <param name="fullPath"> A file path. </param>
        /// <returns> Returns an exact case sensitive file path</returns>
        public static string PathToExactCase(this string fullPath)
        {
            if (!(File.Exists(fullPath) || Directory.Exists(fullPath)))
                return fullPath;

            var di = new DirectoryInfo(fullPath);

            if (di.Parent != null)
            {
                return Path.Combine(
                    PathToExactCase(di.Parent.FullName),
                    di.Parent.GetFileSystemInfos(di.Name)[0].Name);
            }
            else
            {
                return di.Name;
            }
        }

        /// <param name="character"> Remove adjactent duplicates of this character . </param>
        /// <returns> Returns a string with duplicate adjacent characters removed, e.g. Mississippi returns Misisipi. example application is before a 'Split' of a command line argument</returns>
        public static string RemoveDuplicateCharacter(this string s, char character = ' ')
        {
            if (s is null) throw new ArgumentNullException("s", "string cannot be null.");

            StringBuilder result = new(Math.Min(4096, s.Length));
            char lastChar = char.MinValue;

            for (int index = 0; index < s.Length; index++)
            {
                if (s[index] == character && character == lastChar)
                {
                    // skip
                }
                else
                {
                    result.Append(s[index]);
                }
                lastChar = s[index];
            }

            return result.ToString();
        }

    }

}






