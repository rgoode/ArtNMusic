﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMerge.Data;
using MetaBrainz.MusicBrainz;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using System.Text.Json;

namespace MusicMerge
{
    public class GenerateArt
    {
        public string? photo { get; set; }
        public string? api_Key { get; set; }
        public string? style { get; set; }
    }

    public class GeneratedArtInProcess
    {
        public string? result { get; set; }
        public string? photo_Id { get; set; }
        public string? filterjob_id { get; set; }
    }

    public class GeneratedArtProgress
    {
        public string? result { get; set; }
        public string status { get; set; }
        public int progress { get; set; }
        public string? url { get; set; }

    }


    [Route("api/[controller]")]
    [ApiController]
    public class NeuralStyleController : ControllerBase
    {
        private readonly MusicMergeContext _context;

        public NeuralStyleController(MusicMergeContext context)
        {
            _context = context;
        }


        [HttpGet("/generateArt")]
        public  GeneratedArtInProcess GenerateAlbumArt(string photo_url, int styleid)
        {
            var url = $"https://neuralstyle.art/api?photo_url={photo_url}.json?api_key=NSHEBJXXFIOYFOIFXFSMIPJOVGXYZJLHYNKOASKTFLOUANXZ?style_id={styleid}";
            var client = new HttpClient();

            var httpResponse = client.GetAsync(url).Result;
            var albumArtMergeRequest = httpResponse.Content.ReadAsStringAsync().Result;

            var response = JsonSerializer.Deserialize<NeuralStyleResponse>(albumArtMergeRequest);
            var imageInProcess = response;

            var albumArtInProcess = new GeneratedArtInProcess()
            {
                result = response.result,
                photo_Id = response.pid,
                filterjob_id = response.filterjob_id

            };

            return albumArtInProcess;  
        }

        [HttpGet("/render/{photo_url}")]
        public GeneratedArtProgress QueryAlbumArtProgress(string filterjob_id)
        {
            var url = $"https://neuralstyle.art/api/{filterjob_id}.json?api_key=NSHEBJXXFIOYFOIFXFSMIPJOVGXYZJLHYNKOASKTFLOUANXZ";
            var client = new HttpClient();

            var httpResponse = client.GetAsync(url).Result;
            var albumArtMergeRequest = httpResponse.Content.ReadAsStringAsync().Result;

            var response = JsonSerializer.Deserialize<NeuralStyleProgressResponse>(albumArtMergeRequest);
            var imageInProcess = response;

            var albumArtInProcess = new GeneratedArtProgress()
            {
                result = response.result,
                status = response.status,
                progress = response.progress,
                url = response.url
            };

            return albumArtInProcess;
        }

        private bool AlbumArtExists(int id)
        {
            return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}