// <copyright file="DomainException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DomainDriven
{
    public class DomainException(string message): Exception
    {
        public string Message { get; set; } = message;
    }
}
