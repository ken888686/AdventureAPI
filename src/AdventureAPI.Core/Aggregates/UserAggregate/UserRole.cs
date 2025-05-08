namespace AdventureAPI.Core.Aggregates.UserAggregate;

/// <summary>
///     Represents the various roles a user can have in the application.
/// </summary>
public enum UserRole
{
    /// <summary>
    ///     <para>
    ///         Full access to all features and functionalities of the application.
    ///     </para>
    ///     <para>
    ///         Responsible for managing users, roles, and application settings.
    ///     </para>
    /// </summary>
    Admin,

    /// <summary>
    ///     <para>
    ///         Standard role for regular users of the application.
    ///     </para>
    ///     <para>
    ///         Can access basic features and functionalities.
    ///     </para>
    /// </summary>
    User,

    /// <summary>
    ///     <para>
    ///         Can manage content and user interactions, such as approving posts
    ///         and moderating comments.
    ///     </para>
    ///     <para>
    ///         Ensures compliance with community guidelines.
    ///     </para>
    /// </summary>
    Moderator,

    /// <summary>
    ///     <para>
    ///         Can create, edit, and delete content, such as articles or blog posts.
    ///     </para>
    ///     <para>
    ///         May have the ability to publish content but not manage users.
    ///     </para>
    /// </summary>
    Editor,

    /// <summary>
    ///     <para>
    ///         Can view content but cannot create or modify it.
    ///     </para>
    ///     <para>
    ///         Typically used for users who consume content without contributing.
    ///     </para>
    /// </summary>
    Viewer,

    /// <summary>
    ///     <para>
    ///         Limited access for unauthenticated users.
    ///     </para>
    ///     <para>
    ///         Can view public content but cannot access user-specific features.
    ///     </para>
    /// </summary>
    Guest,

    /// <summary>
    ///     <para>
    ///         A higher-level admin role with access to all features,
    ///     </para>
    ///     <para>
    ///         including system settings and configurations.
    ///     </para>
    /// </summary>
    SuperAdmin,

    /// <summary>
    ///     <para>
    ///         A role specifically for users or applications that access the API.
    ///     </para>
    ///     <para>
    ///         May have specific permissions tailored for API interactions.
    ///     </para>
    /// </summary>
    ApiUser,
}
