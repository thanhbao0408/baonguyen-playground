namespace BN.CleanArchitecture.Core.Domain.Entities;

public interface IHasAuditableFields
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}