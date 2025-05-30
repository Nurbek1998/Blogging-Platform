﻿using Blogging_Platform.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blogging_Platform.DTOs.PostDTOs;

public class PostModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Status { get; set; } = "Draft";

    public Guid CategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
}
