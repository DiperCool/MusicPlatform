using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.Common.Exceptions;
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
}