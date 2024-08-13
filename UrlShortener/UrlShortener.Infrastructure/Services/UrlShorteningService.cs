using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Interfaces.Services;

namespace UrlShortener.Infrastructure.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random = new Random();
        private readonly IUrlRepository _urlRepository;

        public UrlShorteningService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];

            while (true)
            {
                for (int i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    int randomIndex = _random.Next(Alphabet.Length);
                    codeChars[i] = Alphabet[randomIndex];
                }
                var code = new string(codeChars);
                if (await _urlRepository.GetByCode(code) == null)
                {
                    return code;
                }
            }
        }
    }
}
