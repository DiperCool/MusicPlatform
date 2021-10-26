using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Web.Application.Test.Queries.GetTest
{
    public class GetTestQuery: IRequest<string>
    {
        
    }

    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, string>
    {
        public async Task<string> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            return "test";
        }
    }
}