using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Neudesic.Schoolistics.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Services
{
    public class RestService : IRestService
    {
        public async void MakeRequest<T>(string requestUrl, string verb, T postObject, Action<ResponseMessage<Object>> successAction, Action<Exception> errorAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);

            request.Method = verb;
            request.ContentType = "application/json";
            request.Accept = "application/json";

            if (Utils.Utils.AuthToken != null)
            {
                request.Headers["Authorization"] = "Basic " + Utils.Utils.AuthToken;
            }

            if (postObject != null && verb == "POST")
            {
                var requestStream = await request.GetRequestStreamAsync();
                var streamWriter = new StreamWriter(requestStream);

                string json = JsonConvert.SerializeObject(postObject);

                streamWriter.Write(json);
                streamWriter.Flush();

            }

            MakeRequest(request, (response) =>
            {
                if (successAction != null)
                {
                    ResponseMessage<Object> toReturn;
                    try
                    {
                        toReturn = Deserialize<ResponseMessage<Object>>(response);
                    }
                    catch (Exception e)
                    {
                        errorAction(e);
                        return;
                    }

                    successAction(toReturn);
                }
            },
            (error) =>
            {
                if (errorAction != null)
                {
                    errorAction(error);
                }
            }
            );
        }

        private void MakeRequest(HttpWebRequest request, Action<string> successAction, Action<Exception> errorAction)
        {
            request.BeginGetResponse(token =>
                {
                    try
                    {
                        using (var response = request.EndGetResponse(token))
                        {
                            using (var stream = response.GetResponseStream())
                            {
                                var reader = new StreamReader(stream);
                                successAction(reader.ReadToEnd());
                            }

                        }
                    }
                    catch (WebException e)
                    {
                        Mvx.Error("Error in Making web request -" + request.RequestUri + "  " + e.ToString());
                        errorAction(e);
                    }

                }, null);
        }

        private T Deserialize<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }

        //public async void UploadFilesToServer(string url, Dictionary<string, string> data, string paramName, string fileName, string fileContentType, byte[] fileData, Action<ResponseMessage<Object>> successAction, Action<Exception> errorAction)
        //{
        //    string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

        //    HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
        //    httpWebRequest2.ContentType = "multipart/form-data; boundary=" +
        //    boundary;
        //    httpWebRequest2.Method = "POST";
        //    httpWebRequest2.Credentials =
        //    System.Net.CredentialCache.DefaultCredentials;

        //    if (Utils.Utils.AuthToken != null)
        //    {
        //        httpWebRequest2.Headers["Authorization"] = "Basic " + Utils.Utils.AuthToken;
        //    }

        //    Stream requestStream = await httpWebRequest2.GetRequestStreamAsync();

        //    byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");


        //    string formdataTemplate = "\r\n--" + boundary +
        //    "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

        //    foreach (string key in data.Keys)
        //    {
        //        string formitem = string.Format(formdataTemplate, key, data[key]);
        //        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
        //        requestStream.Write(formitembytes, 0, formitembytes.Length);
        //    }


        //    requestStream.Write(boundarybytes, 0, boundarybytes.Length);

        //    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
            
        //    string header = string.Format(headerTemplate, "picture", fileName);

        //    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

        //    requestStream.Write(headerbytes, 0, headerbytes.Length);


        //    requestStream.Write(fileData, 0, fileData.Length);

        //    requestStream.Dispose();
        //    MakeRequest(httpWebRequest2, (response) =>
        //    {
        //        if (successAction != null)
        //        {
        //            ResponseMessage<Object> toReturn;
        //            try
        //            {
        //                toReturn = Deserialize<ResponseMessage<Object>>(response);
        //            }
        //            catch (Exception e)
        //            {
        //                errorAction(e);
        //                return;
        //            }

        //            successAction(toReturn);
        //        }
        //    },
        //    (error) =>
        //    {
        //        if (errorAction != null)
        //        {
        //            errorAction(error);
        //        }
        //    }
        //    );
        //}

        public async void UploadFilesToServer(string url, Dictionary<string, string> data, string paramName, string fileName, string fileContentType, byte[] fileData, Action<ResponseMessage<Object>> successAction, Action<Exception> errorAction)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
           
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (Utils.Utils.AuthToken != null)
            {
                wr.Headers["Authorization"] = "Basic " + Utils.Utils.AuthToken;
            }

            Stream rs = await wr.GetRequestStreamAsync();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in data.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, data[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, fileName, fileContentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            rs.Write(fileData, 0, fileData.Length);

            byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            //await rs.FlushAsync();

            MakeRequest(wr, (response) =>
            {
                if (successAction != null)
                {
                    ResponseMessage<Object> toReturn;
                    try
                    {
                        toReturn = Deserialize<ResponseMessage<Object>>(response);
                    }
                    catch (Exception e)
                    {
                        errorAction(e);
                        return;
                    }

                    successAction(toReturn);
                }
            },
            (error) =>
            {
                if (errorAction != null)
                {
                    errorAction(error);
                }
            }
            );
        }

        private void WriteMultipartForm(Stream s, string boundary, Dictionary<string, string> data, string fileName, string fileContentType, byte[] fileData)
        {
            /// The first boundary
            byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
            /// the last boundary.
            byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "–-\r\n");
            /// the form data, properly formatted
            string formdataTemplate = "Content-Disposition; name=\"{0}\"\r\n\r\n{1}";
            /// the form-data file upload, properly formatted
            string fileheaderTemplate = "Content-Disposition; name=\"{0}\"; filename=\"{1}\";\r\nContent-Type: {2}\r\n\r\n";

            /// Added to track if we need a CRLF or not.
            bool bNeedsCRLF = false;

            if (data != null)
            {
                foreach (string key in data.Keys)
                {
                    /// if we need to drop a CRLF, do that.
                    if (bNeedsCRLF)
                        WriteToStream(s, "\r\n");

                    /// Write the boundary.
                    WriteToStream(s, boundarybytes);

                    /// Write the key.
                    WriteToStream(s, string.Format(formdataTemplate, key, data[key]));
                    bNeedsCRLF = true;
                }
            }

            /// If we don't have keys, we don't need a crlf.
            if (bNeedsCRLF)
                WriteToStream(s, "\r\n");

            WriteToStream(s, boundarybytes);
            WriteToStream(s, string.Format(fileheaderTemplate, "file", fileName, fileContentType));
            /// Write the file data to the stream.
            WriteToStream(s, fileData);
            WriteToStream(s, trailer);
        }

        /// <summary>
        /// Writes string to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, string txt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            s.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes byte array to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, byte[] bytes)
        {
            s.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Returns byte array from StorageFile. Author : Farhan Ghumra
        /// </summary>

    }   
}
