using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

class TumblrApiFetcher
{
    /// <summary>
    /// Fetches the JSON response from Tumblr API v1 for a specific blog and start index.
    /// </summary>
    static async Task<string> GetResponseFromTumblrApi(string blogName, int start = 0)
    {
        string url = $"https://{blogName}.tumblr.com/api/read/json?type=photo&num=50&start={start}";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    Match match = Regex.Match(responseText, @"var tumblr_api_read = ({.*?});");
                    if (match.Success)
                    {
                        return match.Groups[1].Value; // Extract JSON from the response
                    }
                }
                Console.WriteLine("Failed to fetch data from the API.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return null;
    }

    /// <summary>
    /// Fetches and displays basic information about the Tumblr blog.
    /// </summary>
    static async Task GetBlogInfo(string blogName)
    {
        string jsonResponse = await GetResponseFromTumblrApi(blogName);
        if (jsonResponse != null)
        {
            JObject blogData = JObject.Parse(jsonResponse);
            string title = blogData["tumblelog"]?["title"]?.ToString() ?? "N/A";
            string name = blogData["tumblelog"]?["name"]?.ToString() ?? "N/A";
            string description = blogData["tumblelog"]?["description"]?.ToString() ?? "N/A";
            string totalPosts = blogData["posts-total"]?.ToString() ?? "N/A";

            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Number of Posts: {totalPosts}");
            Console.WriteLine(new string('-', 50));
        }
        else
        {
            Console.WriteLine("Could not retrieve blog information.");
        }
    }

    /// <summary>
    /// Fetches and displays image URLs for posts within a specified range.
    /// </summary>
    static async Task GetAllImagesForSpecificPostRange(string blogName, string postRange)
    {
        string[] rangeParts = postRange.Split('-');
        int startOfRange = int.Parse(rangeParts[0]);
        int endOfRange = int.Parse(rangeParts[1]);
        int prevOffset = -1;

        for (int i = startOfRange; i <= endOfRange; i++)
        {
            int offset = (int)Math.Ceiling(i / 50.0) - 1;

            if (prevOffset != offset)
            {
                string jsonResponse = await GetResponseFromTumblrApi(blogName, offset * 50);
                if (jsonResponse != null)
                {
                    JObject blogData = JObject.Parse(jsonResponse);
                    JArray posts = (JArray)blogData["posts"];
                    foreach (var post in posts)
                    {
                        if (post["photo-url-1280"] != null)
                        {
                            Console.WriteLine($"{i}. {post["photo-url-1280"]}");
                        }
                        if (post["photos"] != null)
                        {
                            foreach (var photo in post["photos"]) 
                            {
                                Console.WriteLine($"{i}. {photo["photo-url-1280"]}");
                            }
                        }
                    }
                }
            }
            prevOffset = offset;
        }
    }

    /// <summary>
    /// Main method to execute the program.
    /// </summary>
    static async Task Main(string[] args)
    {
        string blogName = "good"; 
        Console.WriteLine($"Using fixed blog name: {blogName}");

        Console.Write("Enter the range (e.g., 1-5): ");
        string postRange = Console.ReadLine();

        await GetBlogInfo(blogName);
        await GetAllImagesForSpecificPostRange(blogName, postRange);
    }
}
