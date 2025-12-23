using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var dirPath = Path.Combine(home, "dev/working/lucide/icons");


foreach (var filePath in Directory.GetFiles(dirPath))
{
    // filename
    var fileName = Path.GetFileName(filePath);
    var nameWords = fileName.Split(".").First().Split("-").Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()).ToList();
    var componentName = string.Join("", nameWords) + "Icon";
    var razorFileName = componentName + ".razor";
    var testFileName = componentName + "Tests.cs";

    // read file
    var content = File.ReadAllText(filePath);

    // path count
    string search = "<path";
    int count = 0;
    int index = 0;

    while ((index = content.IndexOf(search, index, StringComparison.Ordinal)) != -1)
    {
        count++;
        index += search.Length;
    }

    GenerateComponentFile(content, razorFileName);
    GenerateTestFile(count, componentName, testFileName);

    Console.WriteLine($"Added icon: {componentName}");
}

void GenerateTestFile(int count, string componentName, string testFileName)
{
    var testFileContent = """
using RendrUI.Icons;
using RendrUI.IconsTests.Components.Base;
using Shouldly;

namespace RendrUI.IconsTests.Components;

public class [ICONNAME]Tests
    : IconContractTests<[ICONNAME]>
{
    protected override int ExpectedPathCount => [PATHCOUNT];

    [Fact]
    public void Icon_Passes_Through_Arbitrary_Attributes()
    {
        var cut = Render<[ICONNAME]>(p =>
            p.AddUnmatched("aria-hidden", "true"));

        cut.Find("svg")
            .GetAttribute("aria-hidden")
            .ShouldBe("true");
    }
}
""";

    testFileContent = testFileContent.Replace("[ICONNAME]", componentName);
    testFileContent = testFileContent.Replace("[PATHCOUNT]", count.ToString());

    var testFilePath = $"../tests/RendrUI.IconsTests/Components/Icons/{testFileName}";

    using (StreamWriter outputFile = new StreamWriter(testFilePath))
    {
        outputFile.Write(testFileContent);
    }
}

void GenerateComponentFile(string content, string razorFileName)
{
    content = Regex.Replace(content, @"\s+width=""[^""]*""", "");
    content = Regex.Replace(content, @"\s+height=""[^""]*""", "");
    content = content.Replace("<svg", "<svg @attributes=\"AdditionalAttributes\" ");
    content = Regex.Replace(content, @"stroke=""[^""]*""", "stroke=\"@Color\"");
    content = Regex.Replace(content, @"stroke-width=""[^""]*""", "stroke-width=\"@StrokeWidth\"");
    if (content.Contains("class="))
    {
        content = Regex.Replace(content, @"class\s*=\s*""[^""]*""", "class=\"@CssClass\"");
    }
    else
    {
        content = content.Replace("<svg @attributes=\"AdditionalAttributes\" ", "<svg @attributes=\"AdditionalAttributes\" class=\"@CssClass\" ");
    }

    var razorFileContent = $"""
@inherits IconBase
@namespace RendrUI.Icons

{content}
""";

    var razorFilePath = $"../src/RendrUI.Icons/Components/{razorFileName}";

    using (StreamWriter outputFile = new StreamWriter(razorFilePath))
    {
        outputFile.Write(razorFileContent);
    }
}