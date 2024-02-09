using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Salt.Stars.End2End;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Index_E2E_Tests : PageTest
{
    [Test]
    public async Task Index_Page_Shows_without_name()
    {
      // Act
      await Page.GotoAsync($"https://localhost:6001/");
      var result = Page.GetByText("Get your greeting below");

      // Asseert
      await Expect(result).ToBeVisibleAsync();
    }

    [Test]
    public async Task Index_Page_Shows_for_name()
    {
      // Arrange
      var testName = "Marcus";

      // Act
      await Page.GotoAsync($"https://localhost:6001/?DeveloperName={testName}");
      var result = Page.GetByText(new Regex($"Greetings, {testName}.*"));

      // Asseert
      await Expect(result).ToBeVisibleAsync();
    }
}
