using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Salt.Stars.End2End;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Hero_E2E_Tests : PageTest
{
    [Test]
    public async Task Should_show_hero_page()
    {
      // Arrange
      var testId = "Marcus";

      // Act
      await Page.GotoAsync($"http://localhost:6001/Hero?id={testId}");
      var result = Page.GetByText(new Regex($"Skywalker.*"));

      // Asseert
      await Expect(result).ToBeVisibleAsync();
    }

    [Test]
    public async Task Should_update_star_rating_for_hero()
    {
      
      // Arrange
      var testId = 1;
      await Page.GotoAsync($"http://localhost:6001/Hero?id={testId}");
  
      // Act
      await Page.GetByLabel("Star Rating").fill("4");
      await Page.GetByRole(AriaRole.button, new() { Name = "Set Rating" }).Click();
      var result = await Page.GetByLabel("Star Rating");

      // Asseert
      await Expect(result).ToHaveValueAsync("4");
    }
}
