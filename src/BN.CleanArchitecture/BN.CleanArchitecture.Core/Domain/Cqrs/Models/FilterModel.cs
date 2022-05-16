namespace BN.CleanArchitecture.Core.Domain.Cqrs;

public record FilterModel(string FieldName, string Comparision, string FieldValue);