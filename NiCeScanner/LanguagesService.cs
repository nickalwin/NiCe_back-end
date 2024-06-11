using NiCeScanner;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Links
{
	[JsonPropertyName("self")]
	public string Self { get; set; }
}

public class Meta
{
	[JsonPropertyName("count")]
	public int Count { get; set; }
}

public class Language
{
	[JsonPropertyName("langCode")]
	public string LangCode { get; set; }

	[JsonPropertyName("langEnglishName")]
	public string LangEnglishName { get; set; }

	[JsonPropertyName("langNativeName")]
	public string LangNativeName { get; set; }
}

public class ApiResponse
{
	[JsonPropertyName("links")]
	public Links Links { get; set; }

	[JsonPropertyName("meta")]
	public Meta Meta { get; set; }

	[JsonPropertyName("data")]
	public List<Language> Data { get; set; }
}

public class LanguagesService
{
    private readonly HttpClient _httpClient;
    private List<Language> _languages;

    public LanguagesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<byte[]> GetCompressedLanguagesAsync()
    {
	    var request = new HttpRequestMessage(HttpMethod.Get, "https://languages-data.p.rapidapi.com/languages");
	    
	    request.Headers.Add("X-RapidAPI-Key", "c81e2a0abfmsh7fefcc85f8eae1fp1173fdjsn1a6c1d5c7ea6");
	    request.Headers.Add("X-RapidAPI-Host", "languages-data.p.rapidapi.com");
	    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
	    
	    var response = await _httpClient.SendAsync(request);
	    response.EnsureSuccessStatusCode();

	    var compressedContent = await response.Content.ReadAsByteArrayAsync();
	    return compressedContent;
    }

	public async Task<List<Language>> FetchLanguagesAsync()
	{
		if (_languages != null)
		{
			return _languages;
		}
		
		byte[] compressedContent = await GetCompressedLanguagesAsync();
		string jsonResponse;

		byte[] firstTwoBytes = compressedContent.Take(2).ToArray();

		if (firstTwoBytes[0] == 0x1F && firstTwoBytes[1] == 0x8B)
		{
			using var decompressedStream = new MemoryStream();
			using (var compressedStream = new MemoryStream(compressedContent))
			using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
			{
				await gzipStream.CopyToAsync(decompressedStream);
			}

			decompressedStream.Seek(0, SeekOrigin.Begin);

			using var reader = new StreamReader(decompressedStream);
			jsonResponse = await reader.ReadToEndAsync();
		}
		else
		{
			jsonResponse = System.Text.Encoding.UTF8.GetString(compressedContent);
		}

		if (jsonResponse.StartsWith('\uFEFF'))
		{
			jsonResponse = jsonResponse.Substring(1);
		}

		if (string.IsNullOrWhiteSpace(jsonResponse))
		{
			throw new Exception("The JSON response is empty or null.");
		}

		_languages = JsonSerializer.Deserialize<ApiResponse>(jsonResponse).Data;
		
		return _languages;
	}
}
