﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging_Platform.Entities;
public class PostTag
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
