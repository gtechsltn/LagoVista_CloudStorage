﻿using LagoVista.Core.Models;
using LagoVista.Core;
using LagoVista.Core.Models.UIMetaData;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ThrowAway
{
    class Program
    {

        static async void GetRecords()
        {
            var repo = new UsageMetricsRepo("nuviotdev", "Za6PpxUbXjXic8rhK3lbcWyUQyVY2NVsgXRRD1rVj2LAjXUnji5/ooJx7u0ob9cPKTkPu/woa74DBE6IVKsLQA==", new JunkLogger());

            var request = new ListRequest();
            request.PageSize = 50;
            request.PageIndex = 1;
            request.StartDate = DateTime.UtcNow.AddHours(-1).ToJSONString();
            //request.EndDate = DateTime.UtcNow.ToJSONString();

            ListResponse<UsageMetrics> response = null;
            do
            {
                response = await repo.GetByPage("5E78188E767349D681898F0AD8CD1FFC", request);

                foreach(var req in response.Model)
                {
                    Debug.WriteLine(req.RowKey + " " + req.EndTimeStamp.ToDateTime().ToInverseTicksRowKey() + " " + request.StartDate + " " + request.EndDate);
                    Console.WriteLine(req.RowKey + " " + req.EndTimeStamp.ToDateTime().ToInverseTicksRowKey()  + " "  + " " + req.EndTimeStamp + " " + request.StartDate + "  " + request.EndDate);
                }

                Console.WriteLine(response.PageSize);
                Console.WriteLine(response.PageIndex);
                Console.WriteLine(response.NextPartitionKey);
                Console.WriteLine(response.NextRowKey);

                request.NextRowKey = response.NextRowKey;
                request.NextPartitionKey = response.NextPartitionKey;
                request.PageIndex = response.PageIndex + 1;
            }
            while (response.HasMoreRecords && false);


        }

        static void Main(string[] args)
        {

            GetRecords();

            Console.ReadKey();
        }
    }

    public class UsageMetrics : TableStorageEntity
    {
        [JsonProperty("startTimeStamp")]
        public String StartTimeStamp { get; set; }
        [JsonProperty("endTimeStamp")]
        public String EndTimeStamp { get; set; }
        [JsonProperty("elapsedMS")]
        public double ElapsedMS { get; set; }
        [JsonProperty("messagesPerSecond")]
        public double MessagesPerSecond { get; set; }
        [JsonProperty("averageProcessingMS")]
        public double AvergeProcessingMs { get; set; }
        [JsonProperty("version")]
        public String Version { get; set; }
        [JsonProperty("instanceId")]
        public String InstanceId { get; set; }

        [JsonProperty("hostId")]
        public String HostId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("pipelineModuleId")]
        public String PipelineModuleId { get; set; }
        [JsonProperty("messagesProcessed")]
        public int MessagesProcessed { get; set; }
        [JsonProperty("deadLetterCount")]
        public int DeadLetterCount { get; set; }
        [JsonProperty("bytesProccessed")]
        public long BytesProcessed { get; set; }
        [JsonProperty("errorCount")]
        public int ErrorCount { get; set; }
        [JsonProperty("warningCount")]
        public int WarningCount { get; set; }
        [JsonProperty("activeCount")]
        public int ActiveCount { get; set; }
        [JsonProperty("processingMS")]
        public double ProcessingMS { get; set; }
    }
}