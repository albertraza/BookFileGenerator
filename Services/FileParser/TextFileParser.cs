using System.Text.RegularExpressions;

namespace Books.Services
{
    public class TextFileParser : FileParser
    {
        public override string FileExt => ".txt";

        protected override async Task<IEnumerable<string>> GetContent(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync();

            var containsSpecialCharacters = Regex.IsMatch(content, "[^\\w\\s,]");
            if (containsSpecialCharacters) throw new InvalidOperationException("The file contains special characters");

            var resultList = new List<string>();
            var lines = content.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var dataList = line.Split(',');
                resultList.AddRange(dataList);
            }
            return resultList;
        }
    }
}
