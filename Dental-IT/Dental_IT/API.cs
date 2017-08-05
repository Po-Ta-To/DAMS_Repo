﻿using Dental_IT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dental_IT
{
    public class API
    {
        //  Get Treatments
        public async Task<List<Treatment>> GetTreatments()
        {
            List<Treatment> treatmentList = new List<Treatment>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getTreatment));
                request.ContentType = "application/json";
                request.Method = "GET";

                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonValue.Load(stream));
                        System.Diagnostics.Debug.WriteLine("JSON doc: " + jsonDoc.ToString());

                        //  Create objects from json value and populate lists
                        foreach (JsonObject obj in jsonDoc)
                        {
                            System.Diagnostics.Debug.WriteLine("Obj: " + obj.ToString());

                            Treatment treatment = new Treatment()
                            {
                                ID = obj["ID"],
                                TreatmentName = obj["TreatmentName"],
                                TreatmentDesc = obj["TreatmentDesc"],
                                Price = obj["Price"],
                                Price_d = obj["Price_d"],
                                PriceLow = obj["PriceLow"],
                                PriceHigh = obj["PriceHigh"]
                            };

                            treatmentList.Add(treatment);
                        }
                    }
                }

                return treatmentList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return treatmentList;
            }
        } // End of GetTreatments() method

        //  Get ClinicHospitals by Treatment
        public async Task<List<Hospital>> GetClinicHospitalsByTreatment(int id)
        {
            List<Hospital> hospList = new List<Hospital>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getClinicHospitalsByTreatmentID + id));
                request.ContentType = "application/json";
                request.Method = "GET";

                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonValue.Load(stream));
                        System.Diagnostics.Debug.WriteLine("JSON doc: " + jsonDoc.ToString());

                        //  Create objects from json value and populate lists
                        foreach (JsonObject obj in jsonDoc)
                        {
                            System.Diagnostics.Debug.WriteLine("Obj: " + obj.ToString());

                            Hospital hosp = new Hospital()
                            {
                                ID = obj["ID"],
                                HospitalName = obj["ClinicHospitalName"],
                                Price = obj["Price"]
                            };

                            hospList.Add(hosp);
                        }
                    }
                }

                return hospList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return hospList;
            }
        }
    }
}