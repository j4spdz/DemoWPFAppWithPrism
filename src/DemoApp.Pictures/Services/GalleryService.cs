using DemoApp.Shared.Helper.Extensions;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DemoApp.Gallery.Services
{
    public class GalleryService
    {
        private readonly HttpClient _httpClient;

        public GalleryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ImageSource> GetImageFromURL(string url, IProgress<float> progress, CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream())
            {
                await _httpClient.DownloadAsync(url, ms, progress, cancellationToken);

                //[BugFix] The image cannot be decoded. The image header might be corrupted
                ms.Position = 0;

                // Create a BitmapSource  
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                return bitmap;
            }
        }
    }
}
