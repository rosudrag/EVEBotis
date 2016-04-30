using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Main.Models;
using InnerSpaceAPI;
using LavishVMAPI;
using RestSharp;
using System.Net.Http;
using Core.Common;
using EVE.ISXEVE.Extensions;

namespace Service.IntelClient
{
  public class IntelClientService : IService
    {
        /// <summary>
        /// Gets or sets the rest client.
        /// </summary>
        /// <value>
        /// The rest client.
        /// </value>
        private RestClient RestClient { get; set; }

        /// <summary>
        /// Gets or sets the endpoint URL.
        /// </summary>
        /// <value>
        /// The endpoint URL.
        /// </value>
        private string EndpointUrl { get; set; }

        /// <summary>
        /// Gets or sets the endpoint url2.
        /// </summary>
        /// <value>
        /// The endpoint url2.
        /// </value>
        private string EndpointUrl2 { get; set; }

        /// <summary>
        /// Gets or sets the last submission time.
        /// </summary>
        /// <value>
        /// The last submission time.
        /// </value>
        private DateTime? LastSubmissionTime { get; set; }

        /// <summary>
        /// The seconds offset
        /// </summary>
        private double secondsOffset = 5;

        public IntelClientService()
        {
            RestClient = new RestClient();
            //EndpointUrl = ConfigurationManager.AppSettings["endpoint"];
            //EndpointUrl2 = ConfigurationManager.AppSettings["endpoint2"];

            EndpointUrl = "http://localhost:44300/api/RestSubmissions";
            EndpointUrl2 = "https://intel.cyno.link/api/RestSubmissions";
        }

        private void StartNeverEndingIntelSubmission(CancellationToken token)
        {
            Task.Factory.StartNew(() => NeverEndingIntelSubmission(token), token);
        }

        private void NeverEndingIntelSubmission(CancellationToken token) {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                SubmitIntel();

                Thread.Sleep(5000);
            }
        }

        private void SubmitIntel()
        {
            var submission = new Submission { Location = "W-Q233", SubmissionText = "Chewy Ruko" };
            using (var frame = new FrameLock(true))
            {
                var ext = new Extension();
                var myEve = ext.EVE();
                var myMe = ext.Me;

                var locationId = myMe.SolarSystemID;

                if (locationId <= 0)
                {
                    return;
                }

                var locationName = ext.InterstellarByID(locationId).Name;

                var pilots = myEve.GetLocalPilots();
                var pilotNames = "";
                if (pilots != null && pilots.Any())
                {
                    pilots.ForEach(p => pilotNames += p.Name + "\n");
                }

                submission.Location = locationName;
                submission.SubmissionText = pilotNames;
            }

            try
            {
                Task.Factory.StartNew(() =>
                {
                    using (var client = new HttpClient())
                    {
                        var result1 = client.PostAsJsonAsync(EndpointUrl, submission).Result;
                    }
                });

            }
            catch (Exception e)
            {

            }

            try
            {
                Task.Factory.StartNew(() =>
                {
                    using (var client = new HttpClient())
                    {
                        var result2 = client.PostAsJsonAsync(EndpointUrl2, submission).Result;
                    }
                });
            }
            catch (Exception e)
            {

            }
        }

        public void Run()
        {
            InnerSpace.Echo("Running intel service...");

            var cts = new CancellationTokenSource();

            StartNeverEndingIntelSubmission(cts.Token);

            //LavishScript.Events.AttachEventTarget(LavishScript.Events.RegisterEvent("ISXEVE_OnFrame"), SubmitIntel);
        }
    }
}
