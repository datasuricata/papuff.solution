﻿using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using papuff.services.Requests.Base;
using papuff.services.Validators.Notifications;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace papuff.services.Requests {
    public class StorageRequest : BaseRequest {
        public StorageRequest(string baseUri) : base(baseUri) {

        }

        /// <summary>
        /// UploadFile
        /// </summary>
        /// <param name="File">HttpPostedFileBase</param>
        /// <param name="extension"> ".jpeg" or ".pdf" </param>
        /// <returns></returns>
        public async Task<string> UploadFile(FormFile File, string extension, string endpoint, string token = "") {
            try {
                byte[] data;
                var notification = new Notification();

                using (Stream inputStream = File.OpenReadStream()) {
                    if (!(inputStream is MemoryStream memoryStream)) {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                var fileUploadCommand = new FileUpload {
                    Bytes = data,
                    Extension = extension
                };

                var content = new MultipartFormDataContent();

                var fileContent = new ByteArrayContent(fileUploadCommand.Bytes);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                    FileName = Guid.NewGuid().ToString() + fileUploadCommand.Extension
                };

                content.Add(fileContent);

                using (var client = new HttpClient()) {

                    client.BaseAddress = new Uri(baseUri);
                    client.Timeout = TimeSpan.FromMinutes(30);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var response = await client.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode) {

                        notification.Value = await response.Content.ReadAsStringAsync();
                        var message = JsonConvert.DeserializeObject<Notification>(notification.Value);

                        return message.Value;
                    } else {
                        var result = await response.Content.ReadAsStringAsync();
                        throw new Exception(result);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }

    public class FileUpload {
        public byte[] Bytes { get; set; }
        public string Extension { get; set; }

        public static FileUpload CreateFromFile(string path) {
            return new FileUpload {
                Bytes = File.ReadAllBytes(path),
                Extension = Path.GetExtension(path)
            };
        }
    }
}