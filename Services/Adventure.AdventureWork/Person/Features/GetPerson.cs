using System;

using Carter;

using FluentValidation;

using MediatR;

namespace Adventure.AdventureWork.Person.Features;

public static class GetPerson
{
    internal sealed class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, GetPersonResponse>
    {
        private readonly IValidator<GetPersonQuery> _validator;

        public GetPersonQueryHandler(IValidator<GetPersonQuery> validator)
        {
            _validator = validator;
        }

        public async Task<GetPersonResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await Task.Run(() => { }, cancellationToken); // Simulate async work

            return new GetPersonResponse();
        }
    }

    public class Validator : AbstractValidator<GetPersonQuery>
    {
        public Validator()
        {
            RuleFor(x => x.BusinessEntityId).GreaterThan(0).WithMessage("BusinessEntityId must be greater than 0.");
        }
    }

    public class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/person/{businessEntityId:int}", async (int businessEntityId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new GetPersonQuery { BusinessEntityId = businessEntityId };
                var response = await mediator.Send(query, cancellationToken);
                return Results.Ok(response);
            });
        }
    }

    public class GetPersonQuery : IRequest<GetPersonResponse>
    {
        /// <summary>
        /// The unique identifier (Primary Key) for the Person to retrieve.
        /// Corresponds to Person.Person.BusinessEntityID.
        /// </summary>
        public int BusinessEntityId { get; set; }
    }

    public class GetPersonResponse
    {
        /// <summary>
        /// Primary key for Person records.
        /// </summary>
        public int BusinessEntityId { get; set; }

        /// <summary>
        /// Type of person (e.g., SC = Store Contact, IN = Individual customer, EM = Employee).
        /// </summary>
        public string PersonType { get; set; } = string.Empty;

        /// <summary>
        /// 0 = Western style (first name, last name). 1 = Eastern style (last name, first name).
        /// </summary>
        public bool NameStyle { get; set; }

        /// <summary>
        /// A courtesy title. For example, Mr. or Ms.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// First name of the person.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Middle name or initial of the person.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Last name of the person.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Surname suffix. For example, Sr. or Jr.
        /// </summary>
        public string? Suffix { get; set; }

        /// <summary>
        /// Email promotion preference (0, 1, or 2).
        /// </summary>
        public int EmailPromotion { get; set; }

        /// <summary>
        /// Primary email address for the person.
        /// </summary>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Date and time the record was last updated. Useful for caching/concurrency.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        // Note: Rowguid is often excluded from response DTOs unless specifically needed
        // by the client for a particular reason (e.g., specific replication scenarios).
        // public Guid Rowguid { get; set; }
    }
}
