﻿namespace Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string username) : base($"User with username {username} was not found.")
    {
    }

    public UserNotFoundException() : base("User was not found.")
    {
    }
}