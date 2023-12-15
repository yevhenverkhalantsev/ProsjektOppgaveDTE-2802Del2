namespace ProsjektOppgaveWebAPI.Common;

public static class Errors
{
    public const string GENERATE_JWT_TOKEN_ERROR = "Error while generating JWT token";
    public const string USER_NOT_FOUND_ERROR = "User not found";
    public const string USER_ALREADY_EXISTS_ERROR = "User already exists";
    public const string BLOG_NOT_FOUND_ERROR = "Blog not found";
    public const string BLOG_ALREADY_EXISTS_ERROR = "Blog already exists";
    public const string POST_NOT_FOUND_ERROR = "Post not found";
    public const string POST_ALREADY_EXISTS_ERROR = "Post already exists";
    public const string CANT_CREATE_POST_ERROR = "Can't create post";
    public const string CANT_UPDATE_POST_ERROR = "Can't update post";
    public const string CANT_DELETE_BLOG_ERROR = "Can't delete blog";
    public const string CANT_CREATE_BLOG_ERROR = "Can't create blog";
    public const string COMMENT_ALREADY_CREATED_ERROR = "Comment already created";
    public const string CANT_CREATE_COMENT_ERROR = "Can't create comment";
    public const string CANT_DELETE_POST_ERROR = "Can't delete post";
    public const string COMMENT_NOT_FOUND_ERROR = "Comment not found";
    public const string CANT_DELETE_COMMENT_ERROR = "Can't delete comment";
    public const string CANT_UPDATE_COMMENT_ERROR = "Can't update comment";
    public const string TAG_ALREADY_EXISTS = "Tag already exists";
    public const string CANT_CREATE_TAG_ERROR = "Can't create tag";
}