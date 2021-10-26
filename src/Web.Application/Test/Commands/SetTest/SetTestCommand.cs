using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web.Application.Common.Security;

namespace Web.Application.Test.Commands.SetTest
{
    [Authorize]
    public class SetTestCommand: IRequest<string>
    {
        public string Test { get; set; }
    }

    public class SetTestHandler : IRequestHandler<SetTestCommand, string>
    {
        public async Task<string> Handle(SetTestCommand request, CancellationToken cancellationToken)
        {
            return request.Test;
        }
    }
}