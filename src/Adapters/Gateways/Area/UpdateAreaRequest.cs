using Adapters.Gateways.Base;
using Domain.Contracts.Area;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Area;
public class UpdateAreaRequest : UpdateAreaInput, IRequest { }