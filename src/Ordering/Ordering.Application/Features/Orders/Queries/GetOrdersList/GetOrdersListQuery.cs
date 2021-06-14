using System;
using System.Collections.Generic;
using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<OrdersViewModel>>
    {
        public GetOrdersListQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public string Username { get; set; }
    }
}