using System.Text.RegularExpressions;

namespace JobService.API.Services.CreateCategories;

public static class SlugGenerator
{
    public static string Generate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var slug = input.ToLowerInvariant().Trim();

        slug = Regex.Replace(slug, @"\s+", "-");          // spaces → -
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");   // remove invalid chars
        slug = Regex.Replace(slug, @"\-{2,}", "-");       // multiple - → single

        return slug;
    }
}

