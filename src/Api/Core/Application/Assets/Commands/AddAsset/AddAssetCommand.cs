using CSharpFunctionalExtensions;
using MediatR;
using System;

namespace Api.Core.Application.Assets.Commands.AddAsset
{
    public class AddAssetCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Broker { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }
    }
}
