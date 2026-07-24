using System.ComponentModel.DataAnnotations;
using TicketManager.Web.Models;
using Xunit;

namespace TicketManager.Web.Tests;

public sealed class TicketCommentValidationTests
{
    [Fact]
    public void TicketComment_WithValidFields_IsValid()
    {
        var comment = CreateValidComment();

        var results = Validate(comment);

        Assert.Empty(results);
    }

    [Fact]
    public void TicketComment_WithoutAuthorName_IsInvalid()
    {
        var comment = CreateValidComment();
        comment.AuthorName = string.Empty;

        var results = Validate(comment);

        Assert.Contains(results, result => result.ErrorMessage == "O nome e obrigatorio.");
    }

    [Theory]
    [InlineData(9)]
    [InlineData(1001)]
    public void TicketComment_WithInvalidContentLength_IsInvalid(int contentLength)
    {
        var comment = CreateValidComment();
        comment.Content = new string('C', contentLength);

        var results = Validate(comment);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(TicketComment.Content)));
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1000)]
    public void TicketComment_WithBoundaryContentLength_IsValid(int contentLength)
    {
        var comment = CreateValidComment();
        comment.Content = new string('C', contentLength);

        var results = Validate(comment);

        Assert.Empty(results);
    }

    private static TicketComment CreateValidComment()
    {
        return new TicketComment
        {
            AuthorName = "Autor teste",
            Content = "Comentario valido com comprimento minimo.",
            CreatedAt = DateTime.UtcNow
        };
    }

    private static List<ValidationResult> Validate(TicketComment comment)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(comment, new ValidationContext(comment), results, validateAllProperties: true);
        return results;
    }
}
