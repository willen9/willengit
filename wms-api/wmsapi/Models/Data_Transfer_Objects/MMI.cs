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
    public sealed class MMI : MMDKA
    {
        [JsonProperty("items")]
        public List<MMDKB> Items { get; set; }
    }
}