﻿using MediatR;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Customers.Service.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : Request, IRequest<GetCustomerByIdResponse>
    {
        public GetCustomerByIdQuery(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }

        public override Response Response => new GetCustomerByIdResponse(RequestId);
    }
}