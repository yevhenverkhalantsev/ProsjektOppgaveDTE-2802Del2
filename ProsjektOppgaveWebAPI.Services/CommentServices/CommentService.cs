using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public class CommentService : ICommentService
{
    private readonly BlogDbContext _db;
    private readonly UserManager<IdentityUser> _manager;
    private readonly IGenericRepository<Comment> _commentRepository;
    
    public CommentService(UserManager<IdentityUser> userManager, BlogDbContext db, IGenericRepository<Comment> commentRepository)
    {
        _manager = userManager;
        _db = db;
        _commentRepository = commentRepository;
    }
    
    public async Task<IEnumerable<Comment>> GetCommentsForPost(int postId)
    {
        return null;
    }

    public Comment? GetComment(int id)
    {
        var c = (from comment in _db.Comment
                where comment.CommentId == id
                select comment)
            .FirstOrDefault();
        return c;
    }
    
    public async Task<ResponseService<int>> Save(CreateCommentHttpPostModel vm)
    {
        Comment comment = await _commentRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Text == vm.Text 
                                      && x.PostId == vm.PostId);
        if (comment != null)
        {
            throw new Exception(Errors.COMMENT_ALREADY_CREATED_ERROR);
        }
        
        comment = new Comment()
        {
            Text = vm.Text,
            PostId = vm.PostId
        };

        try
        {
            await _commentRepository.Create(comment);

        }
        catch (Exception e)
        {
            throw new Exception(Errors.CANT_CREATE_COMENT_ERROR);
        }
        return ResponseService<int>.Ok(comment.CommentId);
        
    }
    
    public async Task<ResponseService<bool>> Delete(int id)
    {
        var comment = await _commentRepository.GetAll()
            .FirstOrDefaultAsync(x => x.CommentId == id);
        if (comment == null)
        {
            throw new Exception(Errors.COMMENT_NOT_FOUND_ERROR);
        }
        try
        {
            await _commentRepository.Delete(comment);
        }
        catch (Exception e)
        {
            throw new Exception(Errors.CANT_DELETE_COMMENT_ERROR);
        }
        return ResponseService<bool>.Ok(true);
    }

    public async Task<ResponseService<Comment>> Update(UpdateCommentHttpPostModel vm)
    {
        Comment comment = await _commentRepository.GetAll()
            .FirstOrDefaultAsync(x => x.CommentId == vm.CommentId);
        if (comment == null)
        {
            return ResponseService<Comment>.Error(Errors.COMMENT_NOT_FOUND_ERROR);
        }
        
        comment.Text = vm.Text;
        
        try
        {
            await _commentRepository.Update(comment);
        }
        catch (Exception e)
        {
            return ResponseService<Comment>.Error(Errors.CANT_UPDATE_COMMENT_ERROR);
        }
        
        return ResponseService<Comment>.Ok(comment);
    }
}