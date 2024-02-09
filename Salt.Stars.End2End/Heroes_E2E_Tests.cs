using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Salt.Stars.End2End;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Heroes_E2E_Tests : PageTest
{
    [Test]
    public async Task Should_show_heroes_page()
    {
      // Act
      await Page.GotoAsync($"http://localhost:6001/heroes");
      var result = Page.GetByText(new Regex($"The Heroes list.*"));

      // Asseert
      await Expect(result).ToBeVisibleAsync();
    }
}
