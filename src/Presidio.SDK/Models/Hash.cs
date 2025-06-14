﻿using Presidio.Enums;

namespace Presidio.Models;

public class Hash : IAnonymizer
{
    /// <inheritdoc />
    public Operators Type => Operators.hash;

    /// <summary>
    /// The hashing algorithm.
    /// Allowed values: "sha256", "sha512". Default: "sha256".
    /// </summary>
    public HashTypes HashType { get; set; } = HashTypes.sha256;
}