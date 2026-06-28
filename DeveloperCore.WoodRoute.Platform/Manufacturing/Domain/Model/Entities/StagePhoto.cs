using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;

/// <summary>
///     A photo attached to a production stage.
///     Allows the carpenter to document the work done at each step (e.g. a photo of the raw cut wood).
/// </summary>
public class StagePhoto : IAuditableEntity
{
    private StagePhoto()
    {
        Url = string.Empty;
    }

    public StagePhoto(int stageId, string url, string? description = null)
    {
        StageId = stageId;
        Url = url;
        Description = description;
        UploadedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int StageId { get; private set; }

    /// <summary>Public URL to the photo stored in an external service (e.g. S3, Cloudinary).</summary>
    public string Url { get; private set; }

    /// <summary>Optional description explaining what the photo shows.</summary>
    public string? Description { get; private set; }

    public DateTimeOffset UploadedAt { get; private set; }

    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
