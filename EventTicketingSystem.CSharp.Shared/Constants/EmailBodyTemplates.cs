﻿namespace EventTicketingSystem.CSharp.Shared.Constants;

public class EmailBodyTemplates
{
    public static string Otp { get; } = "Your Verification Code is: <b>(@otp)</b>.";
}