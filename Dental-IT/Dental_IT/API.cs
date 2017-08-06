using Dental_IT.Model;
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
        //  Get ClinicHospitals
        public async Task<List<Hospital>> GetClinicHospitals()
        {
            List<Hospital> hospitalList = new List<Hospital>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getAllHospitals));
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

                            Hospital hospital = new Hospital()
                            {
                                ID = obj["ID"],
                                HospitalName = obj["ClinicHospitalName"],
                                Address = obj["Address"],
                                Telephone = obj["Telephone"],
                                Email = obj["Email"],
                                OpeningHours = obj["OpenHours"]
                            };

                            hospitalList.Add(hospital);
                        }
                    }
                }

                return hospitalList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return hospitalList;
            }
        }

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
        }

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
                                Price = obj["Price"],
                                Address = obj["Address"],
                                Telephone = obj["Telephone"],
                                Email = obj["Email"],
                                OpeningHours = obj["OpenHours"]
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

        //  Get Treatments by ClinicHospital
        public async Task<List<Treatment>> GetTreatmentsByClinicHospital(int id)
        {
            List<Treatment> treatmentList = new List<Treatment>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getTreatmentsByCHId + id));
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
                                Price = obj["Price"]
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
        }

        //  Get Appointments
        public async Task<List<Appointment>> GetAppointments(string accessToken)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            List<string> treatmentsList = new List<string>();
            string treatments;

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getAppt));
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers.Add("Authorization", "bearer " + accessToken);

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

                            treatmentsList.Clear();

                            foreach (JsonObject treatment in obj["listOfTreatments"])
                            {
                                treatmentsList.Add(treatment["TreatmentName"]);
                            }

                            treatments = String.Join(", ", treatmentsList);

                            Appointment appointment = new Appointment()
                            {
                                ID = obj["ID"],
                                Treatments = treatments,
                                ClinicHospital = obj["ClinicHospitalName"],
                                Dentist = obj["DoctorDentistName"],
                                Date = DateTime.Parse(obj["AppointmentDate"]),
                                Time = obj["AppointmentTime_s"],
                                Status = obj["ApprovalState"]
                            };

                            appointmentList.Add(appointment);
                        }
                    }
                }

                return appointmentList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return appointmentList;
            }
        }

        // POST Appointment
        public int PostAppointment(string apptJson)
        {
            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_createUser));
                request.ContentType = "application/JSON";
                request.Method = "POST";

                byte[] buffer = Encoding.Default.GetBytes(apptJson);
                if (buffer != null)
                {
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                WebResponse wr = request.GetResponse();
                return 1;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);

                // The remote server returned an error: (400) Bad Request.
                if (e.Message.Contains("400"))
                {
                    return 2;
                }
                //  Error: ConnectFailure (Network is unreachable)
                else if (e.Message.Contains("unreachable"))
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }

        //  Get Dentists by ClinicHospital
        public async Task<List<Dentist>> GetDentistsByClinicHospital(int id)
        {
            List<Dentist> dentistList = new List<Dentist>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getDocDentistsByCHid + id));
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

                            Dentist den = new Dentist();
                            den.DentistID = (int)(obj["ID"]);
                            den.DentistName = obj["Name"];
                        }
                    }
                }

                return dentistList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return dentistList;
            }
        }

        //  Get Sessions by ClinicHospital
        public async Task<List<string>> GetSessionsByClinicHospital(int id)
        {
            List<string> sessionList = new List<string>();

            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getClinicHospitalTimeSlotByCHid + id));
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

                            //  Check if empty
                            if (obj["TimeRangeSlotString"].ToString().Length != 2)
                            {
                                sessionList.Add(obj["TimeRangeSlotString"]);
                            }                            
                        }
                    }
                }

                return sessionList;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return sessionList;
            }
        }

        //  Post User Credentials for Token
        public int PostUserForToken(string email, string password)
        {
            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_postToken));
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.Timeout = 10000;

                byte[] buffer = Encoding.Default.GetBytes("grant_type=password&username=" + email + "&password=" + password);
                if (buffer != null)
                {
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = JsonValue.Load(stream);
                        System.Diagnostics.Debug.WriteLine("JSON doc: " + jsonDoc.ToString());

                        UserAccount.AccessToken = jsonDoc["access_token"];
                    }
                }

                return 1;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);

                // The remote server returned an error: (400) Bad Request.
                if (e.Message.Contains("400"))
                {
                    return 2;
                }
                //  Error: ConnectFailure (Network is unreachable)
                else if (e.Message.Contains("unreachable"))
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }

        //  Get User Account by Username and Access Token
        public bool GetUserAccount(string accessToken)
        {
            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_getUser));
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers.Add("Authorization", "bearer " + accessToken);
                request.Timeout = 10000;

                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = JsonValue.Load(stream);
                        System.Diagnostics.Debug.WriteLine("JSON doc: " + jsonDoc.ToString());

                        if (jsonDoc.Count == 0)
                        {
                            return false;
                        }

                        UserAccount.Name = jsonDoc[0]["Name"];
                    }
                }

                return true;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return false;
            }
        }

        //  Post User Account
        public int PostUserAccount(string userJson)
        {
            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Web_Config.global_connURL_createUser));
                request.ContentType = "application/JSON";
                request.Method = "POST";
                request.Timeout = 10000;

                byte[] buffer = Encoding.Default.GetBytes(userJson);
                if (buffer != null)
                {
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                WebResponse wr = request.GetResponse();
                return 1;
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);

                // The remote server returned an error: (400) Bad Request.
                if (e.Message.Contains("400"))
                {
                    return 2;
                }
                //  Error: ConnectFailure (Network is unreachable)
                else if (e.Message.Contains("unreachable"))
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }
    }
}
