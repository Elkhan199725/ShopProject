﻿using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Brand:AbstractClass
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
