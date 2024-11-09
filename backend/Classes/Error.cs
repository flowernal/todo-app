﻿using backend.Enums;

namespace backend.Classes;

public class Error
{
    public ErrorCode Code { get; set; }
    public string Message { get; set; }
}