﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    /// <summary>
    /// Receving Order
    /// </summary>
    [NotMapped]
    public sealed class MMH : MMDHA
    {
        [JsonProperty("items")]
        public List<MMDHB> Items { get; set; }
    }
}